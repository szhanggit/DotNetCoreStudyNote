using Domain.Models;
using Google.Protobuf.WellKnownTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TXC.Proto.Product;

namespace Services.Utility.Helper
{
    public delegate Task<List<ProductRedeemTimeRestrictionSet>> ParseRestrictionTimeListDel(List<string> TimeRestrictions);
    public delegate Task<List<string>> SerializeRestrictionTimeListDel(IEnumerable<ProductRedeemTimeRestrictionSet> _productRedeemTimeRestrictionSets);
    public delegate Task<bool> IsTimeRestrictionModifiedDel(List<string> OriginalTimeRestrictions, List<string> RequestTimeRestrictions);
    public delegate Task<bool> IsDateRestrictionModifiedDel(List<DateTime> OriginalDateRestrictions, List<DateTime> RequestDateRestrictions);
    public delegate Task<bool> HasDuplicatedDateDel(List<DateTime> RequestDateRestrictions);
    public delegate Task<List<RedeemDateRestrictionItem>> ConvertToDateRestrictionItemListDel(IEnumerable<ProductRedeemDateRestrictionSet> _dateRestrictionList);
    public delegate Task<List<RedeemTimeRestrictionItem>> ConvertToTimeRestrictionItemListDel(IEnumerable<ProductRedeemTimeRestrictionSet> _timeRestrictionList, List<string> _timeRestrictionStringList);
    public delegate Task<bool> IsFormatCorrectDel(List<string> _hourList);
    public delegate Task<bool> IsRangeCorrectDel(List<string> _hourList);
    public delegate Task<bool> HasDuplicatedHoursDel(List<string> _hourList);
    public delegate Task<bool> IsWeekDayRepeatDel(List<int> _dayList);
    public delegate Task<bool> ValidWeekDayDel(List<int> _dayList);


    public interface IRestrictionHelper 
    {
        Task<List<ProductRedeemTimeRestrictionSet>> ParseRestrictionTimeList(List<string> TimeRestrictions);
        Task<List<string>> SerializeRestrictionTimeList(IEnumerable<ProductRedeemTimeRestrictionSet> _productRedeemTimeRestrictionSets);
        Task<bool> IsTimeRestrictionModified(List<string> OriginalTimeRestrictions, List<string> RequestTimeRestrictions);
        Task<bool> IsDateRestrictionModified(List<DateTime> OriginalDateRestrictions, List<DateTime> RequestDateRestrictions);
        Task<bool> HasDuplicatedDate(List<DateTime> RequestDateRestrictions);
        Task<List<RedeemDateRestrictionItem>> ConvertToDateRestrictionItemList(IEnumerable<ProductRedeemDateRestrictionSet> _dateRestrictionList);
        Task<string> SerializeRestrictionTime(ProductRedeemTimeRestrictionSet item);
        Task<List<RedeemTimeRestrictionItem>> ConvertToTimeRestrictionItemList(IEnumerable<ProductRedeemTimeRestrictionSet> _timeRestrictionList, List<string> _timeRestrictionStringList);
        Task<bool> IsFormatCorrect(List<string> _hourList);
        Task<bool> IsRangeCorrect(List<string> _hourList);
        Task<bool> HasDuplicatedHours(List<string> _hourList);
        Task<bool> IsWeekDayRepeat(List<int> _dayList);
        Task<bool> ValidWeekDay(List<int> _dayList);
    }
    public class RestrictionHelper : IRestrictionHelper
    {
        IStringHelper _stringHelper;
        public RestrictionHelper(IStringHelper stringHelper)
        {
            _stringHelper = stringHelper;
        }
        public async Task<List<ProductRedeemTimeRestrictionSet>> ParseRestrictionTimeList(List<string> TimeRestrictions)
        {
            List<ProductRedeemTimeRestrictionSet> _restrictionTimeSetList = new List<ProductRedeemTimeRestrictionSet>();
            if (TimeRestrictions == null || TimeRestrictions.Count == 0)
            {
                return _restrictionTimeSetList;
            }

            for (int i = 0; i < TimeRestrictions.Count; i++)
            {
                string timeRestrictions = TimeRestrictions[i];
                if (string.IsNullOrEmpty(timeRestrictions))
                {
                    continue;
                }

                string[] timeList = timeRestrictions.Split(';');
                if (timeList != null && timeList.Length > 1)
                {
                    int startTime = int.Parse(timeList[0]);
                    int nextPredit = int.Parse(timeList[0]) + 1;
                    for (int j = 1; j < timeList.Length - 1; j++)
                    {
                        if (int.Parse(timeList[j]) == nextPredit)
                        {
                            nextPredit++;
                        }
                        else
                        {
                            ProductRedeemTimeRestrictionSet timeSet = new ProductRedeemTimeRestrictionSet
                            {
                                DayOfTheWeek = (byte)i,
                                StartTime = new TimeSpan(startTime, 0, 0),
                                EndTime = new TimeSpan(nextPredit, 0, 0)
                            };

                            _restrictionTimeSetList.Add(timeSet);

                            startTime = int.Parse(timeList[j]);
                            nextPredit = startTime + 1;
                        }
                    }
                    ProductRedeemTimeRestrictionSet lastTimeSet = new ProductRedeemTimeRestrictionSet
                    {
                        //DayOfTheWeek = (byte)(((i + 1) != 7) ? (i + 1) : 0),
                        DayOfTheWeek = (byte)i,
                        StartTime = new TimeSpan(startTime, 0, 0),
                        EndTime = nextPredit != 24 ? new TimeSpan(nextPredit, 0, 0) : new TimeSpan(0, 23, 59, 59, 999)
                    };

                    _restrictionTimeSetList.Add(lastTimeSet);
                }
            }

            return _restrictionTimeSetList;
        }

        public async Task<List<string>> SerializeRestrictionTimeList(IEnumerable<ProductRedeemTimeRestrictionSet> _productRedeemTimeRestrictionSets)
        {
            if (_productRedeemTimeRestrictionSets.Count() == 0)
            {
                return new List<string>();
            }

            StringBuilder[] sblist = new StringBuilder[7];
            for (int i = 0; i < sblist.Length; i++)
            {
                sblist[i] = new StringBuilder();
            }

            foreach (var item in _productRedeemTimeRestrictionSets)
            {
                var end = item.EndTime.Hours < 23 ? item.EndTime.Hours : item.EndTime.Hours + 1;

                for (int i = item.StartTime.Hours; i < end; i++)
                {
                    sblist[item.DayOfTheWeek].Append(i + ";");
                }
            }

            return sblist.Select(p => p.ToString()).ToList();
        }

        public async Task<string> SerializeRestrictionTime(ProductRedeemTimeRestrictionSet item)
        {
            StringBuilder sb = new StringBuilder();
            var end = item.EndTime.Hours < 23 ? item.EndTime.Hours : item.EndTime.Hours + 1;

            for (int i = item.StartTime.Hours; i < end; i++)
            {
                sb.Append(i + ";");
            }

            return sb.ToString();
        }

        public async Task<bool> IsTimeRestrictionModified(List<string> OriginalTimeRestrictions, List<string> RequestTimeRestrictions)
        {
            bool flag = false;
            if (OriginalTimeRestrictions.Count != RequestTimeRestrictions.Count)
            {
                return true;
            }

            for (int i = 0; i < RequestTimeRestrictions.Count; i++)
            {
                if (!string.IsNullOrEmpty(OriginalTimeRestrictions[i]) && !string.IsNullOrEmpty(RequestTimeRestrictions[i]) && !OriginalTimeRestrictions[i].Equals(RequestTimeRestrictions[i]))
                {
                    flag = true;
                    break;
                }
                else if ((!string.IsNullOrEmpty(OriginalTimeRestrictions[i]) && string.IsNullOrEmpty(RequestTimeRestrictions[i])) || (string.IsNullOrEmpty(OriginalTimeRestrictions[i]) && !string.IsNullOrEmpty(RequestTimeRestrictions[i])))
                {
                    flag = true;
                    break;
                }
            }

            return flag;
        }

        public async Task<bool> IsDateRestrictionModified(List<DateTime> OriginalDateRestrictions, List<DateTime> RequestDateRestrictions)
        {
            if (OriginalDateRestrictions.Count != RequestDateRestrictions.Count)
            {
                return true;
            }

            foreach (var date in RequestDateRestrictions)
            {
                if (!OriginalDateRestrictions.Contains(date))
                {
                    return true;
                }
            }

            return false;
        }

        public async Task<bool> HasDuplicatedDate(List<DateTime> RequestDateRestrictions)
        {
            bool anyDuplicate = RequestDateRestrictions.GroupBy(x => x.Date).Any(g => g.Count() > 1);
            return anyDuplicate;
        }

        public async Task<List<RedeemDateRestrictionItem>> ConvertToDateRestrictionItemList(IEnumerable<ProductRedeemDateRestrictionSet> _dateRestrictionList)
        {
            List<RedeemDateRestrictionItem> _dateRestrictionItemList = new List<RedeemDateRestrictionItem>();
            foreach (ProductRedeemDateRestrictionSet item in _dateRestrictionList)
            {
                _dateRestrictionItemList.Add(new RedeemDateRestrictionItem
                {
                    BlackDate = Timestamp.FromDateTime(DateTime.SpecifyKind(item.BlackDate, DateTimeKind.Utc)),
                    ProductRedeemDateBlacklistId = item.ProductRedeemDateBlacklistId,
                    ProductRedeemDateBlacklistSetId = item.ProductRedeemDateBlacklistSetId
                });
            }

            return _dateRestrictionItemList;
        }

        public async Task<List<RedeemTimeRestrictionItem>> ConvertToTimeRestrictionItemList(IEnumerable<ProductRedeemTimeRestrictionSet> _timeRestrictionList, List<string> _timeRestrictionStringList)
        {
            List<RedeemTimeRestrictionItem> _timeRestrictionItemList = new List<RedeemTimeRestrictionItem>();

            if (_timeRestrictionList.Count() == 0)
            {
                return _timeRestrictionItemList;
            }

            int ProductRedeemTimeRestrictionId = _timeRestrictionList.First().ProductRedeemTimeRestrictionId;
            for (int i = 0; i < 7; i++)
            {
                if (!string.IsNullOrEmpty(_timeRestrictionStringList[i]))
                {
                    int ProductRedeemTimeRestrictionSetId = _timeRestrictionList.Where(_ => _.DayOfTheWeek == i).FirstOrDefault().ProductRedeemTimeRestrictionSetId;
                    _timeRestrictionItemList.Add(new RedeemTimeRestrictionItem
                    {
                        DayOfTheWeek = i,
                        Hours = _timeRestrictionStringList[i],
                        ProductRedeemTimeRestrictionId = ProductRedeemTimeRestrictionId,
                        ProductRedeemTimeRestrictionSetId = ProductRedeemTimeRestrictionSetId
                    });
                }
                else
                {
                    _timeRestrictionItemList.Add(new RedeemTimeRestrictionItem
                    {
                        DayOfTheWeek = i,
                        Hours = string.Empty
                    });
                }
            }
           
            return _timeRestrictionItemList;
        }

        public async Task<bool> IsFormatCorrect(List<string> _hourList)
        {
            bool _canPass = false;
            foreach (string hours in _hourList)
            {
                if (string.IsNullOrEmpty(hours) || string.IsNullOrWhiteSpace(hours))
                {
                    continue;
                }
                else
                {
                    _canPass = true;
                    break;
                }
            }

            if (!_canPass)
            {
                return false;
            }

            foreach (string hours in _hourList)
            {
                if (!string.IsNullOrEmpty(hours))
                {
                    string tempHours = string.Empty;
                    if (!hours.Contains(";"))
                    {
                        return false;
                    }
                    if (!hours.EndsWith(";"))
                    {
                        return false;
                    }
                    else
                    {
                        tempHours = _stringHelper.RemoveLast(hours, ";");
                    }
                    string[] arr = tempHours.Split(";");
                    foreach (string hour in arr)
                    {
                        int result;
                        if (!Int32.TryParse(hour, out result))
                        {
                            return false;
                        }
                    }
                }
            }

            return true;
        }

        public async Task<bool> IsRangeCorrect(List<string> _hourList)
        {
            foreach (string hours in _hourList)
            {
                if (!string.IsNullOrEmpty(hours))
                {
                    string tempHours = string.Empty;
                    tempHours = _stringHelper.RemoveLast(hours, ";");
                    string[] arr = tempHours.Split(";");
                    foreach (string hour in arr)
                    {
                        int result;
                        Int32.TryParse(hour, out result);
                        if (result < 0 || result > 23)
                        {
                            return false;
                        }
                    }
                }
            }

            return true;
        }

        public async Task<bool> HasDuplicatedHours(List<string> _hourList)
        {
            foreach (string hours in _hourList)
            {
                if (!string.IsNullOrEmpty(hours))
                {
                    string tempHours = string.Empty;
                    tempHours = _stringHelper.RemoveLast(hours, ";");
                    string[] arr = tempHours.Split(";");
                    bool anyDuplicate = arr.GroupBy(x => x).Any(g => g.Count() > 1);
                    if (anyDuplicate)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public async Task<bool> IsWeekDayRepeat(List<int> _dayList)
        {
            if (_dayList.GroupBy(x => x).Any(g => g.Count() > 1))
            {
                return true;
            }

            return false;
        }

        public async Task<bool> ValidWeekDay(List<int> _dayList)
        {
            if (_dayList.Count != 7)
            {
                return false;
            }

            if (!_dayList.Contains((int)DayOfWeek.Sunday))
            {
                return false;
            }

            if (!_dayList.Contains((int)DayOfWeek.Monday))
            {
                return false;
            }

            if (!_dayList.Contains((int)DayOfWeek.Tuesday))
            {
                return false;
            }

            if (!_dayList.Contains((int)DayOfWeek.Wednesday))
            {
                return false;
            }

            if (!_dayList.Contains((int)DayOfWeek.Thursday))
            {
                return false;
            }

            if (!_dayList.Contains((int)DayOfWeek.Friday))
            {
                return false;
            }

            if (!_dayList.Contains((int)DayOfWeek.Saturday))
            {
                return false;
            }

            return true;
        }
    }
}
