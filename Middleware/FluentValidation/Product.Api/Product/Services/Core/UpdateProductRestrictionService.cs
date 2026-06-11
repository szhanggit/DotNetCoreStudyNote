using Domain.Models;
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
    public class UpdateProductRestrictionService : IUpdateProductRestrictionService
    {
        private IDbConnection _dbConnection;

        public UpdateProductRestrictionService()
        {

        }

        public async Task<UpdateRedeemRestrictionResponse> UpdateRedeemRestriction(
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
            )
        {
            List<DateTime> _originBlackDateList = new List<DateTime>();
            List<DateTime> _blackDateList = new List<DateTime>();
            List<ProductRedeemTimeRestrictionSet> _insertTimeList = new List<ProductRedeemTimeRestrictionSet>();
            CreateProductRestrictionV1 _createProductRestrictionV1 = new CreateProductRestrictionV1()
            {
                ProductId = request.ProductId,
                TenantId = request.TenantId,
                TX2UserName = request.TX2UserName
            };

            try
            {
                if (string.IsNullOrEmpty(request.TenantName))
                    return new UpdateRedeemRestrictionResponse() { Success = false, Message = "TenantName header required" };

                if (request.ProductId <= 0)
                    return new UpdateRedeemRestrictionResponse() { Success = false, Message = "Invalid request" };

                //check tx2 connector config
                TenantConfig queueNameConfig = await getConfig("TX2ConnectorQueueName", request.TenantId);

                // initialize db connection
                Response<IDbConnection> conn = await getDBConnection(request.TenantId);

                if (!conn.Success)
                    return new UpdateRedeemRestrictionResponse() { Success = false, Message = "Error in Tenant DB" };

                _dbConnection = conn.Data;

                Tuple<int, int> productChecker = await _checkProductDel(request.ProductId, _dbConnection);
                if (productChecker.Item1 == -1)
                {
                    return new UpdateRedeemRestrictionResponse() { Success = false, Message = "Product not found" };
                }

                if (request.RedeemDateRestrictionItems == null || request.RedeemDateRestrictionItems.Count == 0)
                {
                    int re = await _deleteDateRestrictionDel(request.ProductId, request.TX2UserName, _dbConnection);
                    if (re == 1)
                    {
                        var _sr = await _sendProductDeleteRestrictionMessageDel(request.TenantId, queueNameConfig.Value, _createProductRestrictionV1);
                        if (!_sr)
                        {
                            return new UpdateRedeemRestrictionResponse() { Success = false, Message = "Fail to be sent to service bus" };
                        }
                    }
                    else
                    {
                        return new UpdateRedeemRestrictionResponse() { Success = false, Message = "Fail to delete black date list" };
                    }
                }

                if (request.RedeemTimeRestrictionItems == null || request.RedeemTimeRestrictionItems.Count != 7)
                {
                    return new UpdateRedeemRestrictionResponse() { Success = false, Message = "Invalid redeem time restriction" };
                }

                foreach (var item in request.RedeemDateRestrictionItems)
                {
                    if (item.BlackDate == null)
                    {
                        continue;
                    }
                    else
                    {
                        try
                        {
                            _blackDateList.Add(item.BlackDate.ToDateTime());
                        }
                        catch (Exception)
                        {
                            return new UpdateRedeemRestrictionResponse() { Success = false, Message = "Invalid black date format" };
                        }
                    }                    
                }

                bool DoesHaveDuplicateDate = await _hasDuplicatedDateDel(_blackDateList);
                if (DoesHaveDuplicateDate)
                {
                    return new UpdateRedeemRestrictionResponse() { Success = false, Message = "Cannot have duplicated black date" };
                }

                List<string> _originHourList = new List<string>();
                List<string> _hourList = new List<string>();
                List<int> _dayInWeekList = new List<int>();
                foreach (var item in request.RedeemTimeRestrictionItems)
                {
                    _hourList.Add(item.Hours);
                    _dayInWeekList.Add((int)item.DayOfTheWeek);
                }
                bool _isWeekDayRepeated = await _isWeekDayRepeatDel(_dayInWeekList);
                if (_isWeekDayRepeated)
                {
                    return new UpdateRedeemRestrictionResponse() { Success = false, Message = "DayOfWeek is repeated" };
                }
                bool _isDayOfWeekCorrect = await _validWeekDayDel(_dayInWeekList);
                if (!_isDayOfWeekCorrect)
                {
                    return new UpdateRedeemRestrictionResponse() { Success = false, Message = "DayOfWeek is out of range (0~6)" };
                }
                bool _formatIsCorrect = await _isFormatCorrectDel(_hourList);
                if (!_formatIsCorrect)
                {
                    return new UpdateRedeemRestrictionResponse() { Success = false, Message = "Invalid time Restriction format" };
                }
                bool _rangeIsCorrect = await _isRangeCorrectDel(_hourList);
                if (!_rangeIsCorrect)
                {
                    return new UpdateRedeemRestrictionResponse() { Success = false, Message = "Hour is out of range (0~23)" };
                }
                bool _hasDuplicatedHour = await _hasDuplicatedHoursDel(_hourList);
                if (_hasDuplicatedHour)
                {
                    return new UpdateRedeemRestrictionResponse() { Success = false, Message = "Hour is repeated" };
                }


                IEnumerable<ProductRedeemTimeRestrictionSet> _timeList = await _getTimeRestrictionListDel(request.ProductId, _dbConnection);
                _originHourList = await _serializeRestrictionTimeListDel(_timeList);
                IEnumerable<ProductRedeemDateRestrictionSet> _dateList = await _getDateRestrictionListDel(request.ProductId, _dbConnection);
                foreach (ProductRedeemDateRestrictionSet date in _dateList)
                {
                    _originBlackDateList.Add(date.BlackDate);
                }

                bool _timeRestrictionModified = await _isTimeRestrictionModifiedDel(_originHourList, _hourList);
                if (_timeRestrictionModified)
                {
                    _insertTimeList = await _parseRestrictionTimeListDel(_hourList);
                    int _updateTimeRestrictionList = await _insertTimeRestrictionDel(_insertTimeList, request.ProductId, request.TX2UserName, _dbConnection);
                    if (_updateTimeRestrictionList != 1)
                    {
                        return new UpdateRedeemRestrictionResponse() { Success = false, Message = "Update time restriction list failed" };
                    }
                }

                bool _blackDateModified = await _isDateRestrictionModifiedDel(_originBlackDateList, _blackDateList);
                if (_blackDateModified)
                {
                    var _updateBlackList = await _insertDateRestrictionDel(_blackDateList, request.ProductId, request.TX2UserName, _dbConnection);
                    if (_updateBlackList != 1)
                    {
                        return new UpdateRedeemRestrictionResponse() { Success = false, Message = "Update black date list failed" };
                    }
                }

                if (!_timeRestrictionModified && !_blackDateModified)
                {
                    return new UpdateRedeemRestrictionResponse() { Success = false, Message = "No change" };
                }

                List<TimeRestriction> _timeRestrictionList = new List<TimeRestriction>();
                foreach (var time in _insertTimeList)
                {
                    _timeRestrictionList.Add(new TimeRestriction
                    {
                        DayOfWeek = (DayOfWeek)time.DayOfTheWeek,
                        StartTime = time.StartTime,
                        EndTime = time.EndTime
                    });
                }
                _createProductRestrictionV1.TimeRestrictionList = _timeRestrictionList;
                _createProductRestrictionV1.BlackDateList = _blackDateList;
                var _sendResult = await _sendProductCreateRestrictionMessageDel(request.TenantId, queueNameConfig.Value, _createProductRestrictionV1);

                if (_sendResult)
                {
                    return new UpdateRedeemRestrictionResponse
                    {
                        Success = true,
                        Message = "Success",
                        Data = request.ProductId
                    };
                }
                else
                {
                    return new UpdateRedeemRestrictionResponse() { Success = false, Message = "Fail to be sent to service bus" };
                }
            }
            catch (Exception ex)
            {
                return new UpdateRedeemRestrictionResponse() { Success = false, Message = ex.Message };
            }
        }
    }
}
