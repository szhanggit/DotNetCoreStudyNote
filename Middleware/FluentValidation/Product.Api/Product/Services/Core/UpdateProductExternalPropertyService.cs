using Domain.Models;
using Google.Protobuf.WellKnownTypes;
using Services.Interface;
using Services.Utility.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TXC.Common.Domain;
using TXC.Common.MessageContract.Product;
using TXC.Proto.Product;

namespace Services.Core
{
    public class UpdateProductExternalPropertyService : IUpdateProductExternalPropertyService
    {
        private IDbConnection _dbConnection;
        private readonly IExternalPropertyHelper _externalPropertyHelper;
        public UpdateProductExternalPropertyService(IExternalPropertyHelper externalPropertyHelper)
        {
            _externalPropertyHelper = externalPropertyHelper;
        }
        public async Task<UpdateProductExternalPropertyResponse> UpdateProductExternalProperty(
            UpdateProductExternalPropertyRequest request
            , CheckProductDel _checkProductDel
            , GetConfigDel getConfig
            , GetDBConnectionDel getDBConnection
            , GetExternalPropertyListDel _getExternalPropertyListDel
            , HasExternalPropertyModifiedDel _hasExternalPropertyModifiedDel
            , HasDuplicatePropertyNameDel _hasDuplicatePropertyNameDel
            , IsPropertyNameEmptyDel _isPropertyNameEmptyDel
            , DeleteExternalPropertyDel _deleteExternalPropertyDel
            , InsertExternalPropertyDel _insertExternalPropertyDel
            , SendProductCreateExternalPropertyMessageDel _sendProductCreateExternalPropertyMessageDel
            , SendProductDeleteExternalPropertyMessageDel _sendProductDeleteExternalPropertyMessageDel
            )
        {
            List<ExternalProperty> _externalPropertyList = new List<ExternalProperty>();
            CreateProductExternalPropertyV1 _createProductExternalPropertyV1 = new CreateProductExternalPropertyV1()
            {
                ProductId = request.ProductId,
                TenantId = request.TenantId,
                TX2UserName = request.TX2UserName
            };
            try
            {
                if (string.IsNullOrEmpty(request.TenantName))
                    return new UpdateProductExternalPropertyResponse() { Success = false, Message = "TenantName header required" };

                if (request.ProductId <= 0)
                    return new UpdateProductExternalPropertyResponse() { Success = false, Message = "Invalid request" };

                //check tx2 connector config
                TenantConfig queueNameConfig = await getConfig("TX2ConnectorQueueName", request.TenantId);

                // initialize db connection
                Response<IDbConnection> conn = await getDBConnection(request.TenantId);

                if (!conn.Success)
                    return new UpdateProductExternalPropertyResponse() { Success = false, Message = "Error in Tenant DB" };

                _dbConnection = conn.Data;

                Tuple<int, int> productChecker = await _checkProductDel(request.ProductId, _dbConnection);
                if (productChecker.Item1 == -1)
                {
                    return new UpdateProductExternalPropertyResponse() { Success = false, Message = "Product not found" };
                }

                // delete all of the external properties from the product.
                if (request.ExternalPropertyListItems == null || request.ExternalPropertyListItems.Count == 0)
                {
                    int _result = await _deleteExternalPropertyDel(request.ProductId, request.TX2UserName, _dbConnection);
                    if (_result == 1)
                    {
                        bool _send = await _sendProductDeleteExternalPropertyMessageDel(request.TenantId, queueNameConfig.Value, _createProductExternalPropertyV1);
                        if (_send)
                        {
                            return new UpdateProductExternalPropertyResponse
                            {
                                Success = true,
                                Message = "Success",
                                Data = request.ProductId
                            };
                        }
                        else
                        {
                            return new UpdateProductExternalPropertyResponse
                            {
                                Success = false,
                                Message = "Fail to be sent to service bus",
                                Data = request.ProductId
                            };
                        }
                    }
                    else
                    {
                        return new UpdateProductExternalPropertyResponse
                        {
                            Success = false,
                            Message = "Failed to delete external properties",
                            Data = request.ProductId
                        };
                    }
                }

                if (_isPropertyNameEmptyDel(request.ExternalPropertyListItems))
                {
                    return new UpdateProductExternalPropertyResponse
                    {
                        Success = false,
                        Message = "PropertyName is null or empty",
                        Data = request.ProductId
                    };
                }

                if (_hasDuplicatePropertyNameDel(request.ExternalPropertyListItems))
                {
                    return new UpdateProductExternalPropertyResponse
                    {
                        Success = false,
                        Message = "PropertyName already exists",
                        Data = request.ProductId
                    };
                }

                IEnumerable<ExternalPropertyItem> _propertyList = await _getExternalPropertyListDel(request.ProductId, _dbConnection);


                if (_hasExternalPropertyModifiedDel(_propertyList, request.ExternalPropertyListItems, _externalPropertyHelper.CompareExternalPropertyIsTheSame))
                {
                    List<ProductExternalPropertySet> _propertySetList = new List<ProductExternalPropertySet>();
                    foreach (var item in request.ExternalPropertyListItems)
                    {
                        ProductExternalPropertySet productExternalPropertySet = new ProductExternalPropertySet
                        {
                            PropertyName = item.PropertyName,
                            PropertyValue = item.PropertyValue,
                            Description = item.Description
                        };
                        _propertySetList.Add(productExternalPropertySet);
                        _externalPropertyList.Add(new ExternalProperty { PropertyName = item.PropertyName, PropertyValue = item.PropertyValue, Description = item.Description });
                    }
                    int _result = await _insertExternalPropertyDel(_propertySetList, request.ProductId, request.TX2UserName, _dbConnection);
                    if (_result == 1)
                    {                       
                        _createProductExternalPropertyV1.ExternalProperties = _externalPropertyList;
                        bool _successfulSent = await _sendProductCreateExternalPropertyMessageDel(request.TenantId, queueNameConfig.Value, _createProductExternalPropertyV1);
                        if (_successfulSent)
                        {
                            return new UpdateProductExternalPropertyResponse
                            {
                                Success = true,
                                Message = "Success",
                                Data = request.ProductId
                            };
                        }
                        else
                        {
                            return new UpdateProductExternalPropertyResponse
                            {
                                Success = false,
                                Message = "Failed to send to service bus",
                                Data = request.ProductId
                            };
                        }
                    }
                    else
                    {
                        return new UpdateProductExternalPropertyResponse
                        {
                            Success = false,
                            Message = "Failed",
                            Data = request.ProductId
                        };
                    }
                }
                else
                {
                    return new UpdateProductExternalPropertyResponse
                    {
                        Success = false,
                        Message = "No Change",
                        Data = request.ProductId
                    };
                }
            }
            catch (Exception ex)
            {
                return new UpdateProductExternalPropertyResponse() { Success = false, Message = ex.Message };
            }
        }
    }
}
