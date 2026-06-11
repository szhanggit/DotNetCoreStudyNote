using Services.Utility.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TXC.Proto.Product;

namespace Services.Interface
{
    public interface IUpdateProductExternalPropertyService
    {
        Task<UpdateProductExternalPropertyResponse> UpdateProductExternalProperty(
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
            );        
    }
}
