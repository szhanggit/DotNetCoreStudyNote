using Services.Utility.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TXC.Proto.Product;

namespace Services.Interface
{
    public interface IUpdateProductRestrictionService
    {
        Task<UpdateRedeemRestrictionResponse> UpdateRedeemRestriction(
            UpdateRedeemRestrictionRequest request
            , CheckProductDel _checkProductDel
            , GetConfigDel getConfig
            , GetDBConnectionDel getDBConnection
            , InsertTimeRestrictionDel _insertTimeRestrictionDel
            , GetTimeRestrictionListDel _getTimeRestrictionListDel
            , InsertDateRestrictionDel _insertDateRestrictionDel
            , GetDateRestrictionListDel _getDateRestrictionListDel
            , DeleteDateRestrictionDel _deleteDateRestrictionDel
            , SendProductCreateRestrictionMessageDel _sendProductCreateRestrictionMessageDel
            , ParseRestrictionTimeListDel _parseRestrictionTimeListDel
            , SerializeRestrictionTimeListDel _serializeRestrictionTimeListDel
            , IsTimeRestrictionModifiedDel _isTimeRestrictionModifiedDel
            , IsDateRestrictionModifiedDel _isDateRestrictionModifiedDel
            , HasDuplicatedDateDel _hasDuplicatedDateDel
            , IsFormatCorrectDel _isFormatCorrectDel
            , IsRangeCorrectDel _isRangeCorrectDel
            , HasDuplicatedHoursDel _hasDuplicatedHoursDel
            , ValidWeekDayDel _validWeekDayDel
            , SendProductDeleteRestrictionMessageDel _sendProductDeleteRestrictionMessageDel
            , IsWeekDayRepeatDel _isWeekDayRepeatDel
            );
    }
}
