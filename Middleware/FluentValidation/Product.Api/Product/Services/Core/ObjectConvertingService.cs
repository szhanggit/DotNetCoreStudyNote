using AutoMapper;
using Domain.Models;
using Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TXC.Common.MessageContract.Product;
using TXC.Proto.Product;

namespace Services.Core
{
    public class ObjectConvertingService : IObjectConvertingService
    {
        private readonly IMapper _mapper;
        public ObjectConvertingService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public ProductCondition ConvertProductConditionResponseToProductCondition(ProductConditionResponse _productConditionResponse)
        {
            ProductCondition _result = new ProductCondition();
            if (_productConditionResponse == null)
            {
                return _result;
            }
            else
            {
                _result = _mapper.Map<ProductCondition>(_productConditionResponse);
                return _result;
            }
        }

        public ProductCondition ConvertProductUpdatingConditionRequestToProductCondition(ProductUpdatingConditionRequest _productUpdatingConditionRequest)
        {
            ProductCondition _result = new ProductCondition();
            if (_productUpdatingConditionRequest == null)
            {
                return _result;
            }
            else
            {
                _result = _mapper.Map<ProductCondition>(_productUpdatingConditionRequest);
                return _result;
            }
        }

        public UpdateProductConditionV1 ConvertProductUpdatingConditionRequestToUpdateProductConditionV1(ProductUpdatingConditionRequest _productUpdatingConditionRequest)
        {
            UpdateProductConditionV1 _result = new UpdateProductConditionV1();
            if (_productUpdatingConditionRequest == null)
            {
                return _result;
            }
            else
            {
                _result = _mapper.Map<UpdateProductConditionV1>(_productUpdatingConditionRequest);
                return _result;
            }
        }
    }
}
