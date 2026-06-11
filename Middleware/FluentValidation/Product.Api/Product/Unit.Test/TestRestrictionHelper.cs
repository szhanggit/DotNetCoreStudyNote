using Domain.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting.Internal;
using Moq;
using Product.Api;
using Services.Core;
using Services.Interface;
using Services.Utility.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TXC.Common.CacheManagement;
using TXC.Common.CacheManagement.Resolver;
using TXC.Common.Data;
using TXC.Common.Data.TenantDbConnection;
using TXC.Common.MessageContract;
using Xunit;

namespace Unit.Test
{

    public class TestRestrictionHelper
    {
        [Fact]
        public async Task Test_ParseRestrictionTimeList_Case1()
        {
            List<ProductRedeemTimeRestrictionSet> _restrictionTimeSetList = new List<ProductRedeemTimeRestrictionSet>();
            List<string> TimeRestrictions = new List<string> {
                "1;2;3;4;5;6;7;17;18;19;20;21;22;23;"
                , "8;9;10;11;12;13;14;15;16;17;"
                , "8;9;10;11;12;13;14;15;16;17;"
                , "8;9;10;11;12;13;14;15;16;17;"
                , "8;9;10;11;12;13;14;15;16;17;"
                , "8;9;10;11;12;13;14;15;16;17;"
                , "8;9;10;11;12;13;14;15;16;17;"
            };
            IStringHelper _stringHelper = new StringHelper();
            IRestrictionHelper _restrictionTimeHelper = new RestrictionHelper(_stringHelper);
            _restrictionTimeSetList = await _restrictionTimeHelper.ParseRestrictionTimeList(TimeRestrictions);

            Assert.Equal(8, _restrictionTimeSetList.Count);
            Assert.Equal(1, _restrictionTimeSetList[0].StartTime.Hours);
            Assert.Equal(0, _restrictionTimeSetList[0].StartTime.Minutes);
            Assert.Equal(0, _restrictionTimeSetList[0].StartTime.Seconds);
            Assert.Equal(8, _restrictionTimeSetList[0].EndTime.Hours);
            Assert.Equal(0, _restrictionTimeSetList[0].EndTime.Minutes);
            Assert.Equal(0, _restrictionTimeSetList[0].EndTime.Seconds);

            Assert.Equal(17, _restrictionTimeSetList[1].StartTime.Hours);
            Assert.Equal(0, _restrictionTimeSetList[1].StartTime.Minutes);
            Assert.Equal(0, _restrictionTimeSetList[1].StartTime.Seconds);
            Assert.Equal(23, _restrictionTimeSetList[1].EndTime.Hours);
            Assert.Equal(59, _restrictionTimeSetList[1].EndTime.Minutes);
            Assert.Equal(59, _restrictionTimeSetList[1].EndTime.Seconds);

            Assert.Equal(8, _restrictionTimeSetList[2].StartTime.Hours);
            Assert.Equal(0, _restrictionTimeSetList[2].StartTime.Minutes);
            Assert.Equal(0, _restrictionTimeSetList[2].StartTime.Seconds);
            Assert.Equal(18, _restrictionTimeSetList[2].EndTime.Hours);
            Assert.Equal(0, _restrictionTimeSetList[2].EndTime.Minutes);
            Assert.Equal(0, _restrictionTimeSetList[2].EndTime.Seconds);

            Assert.Equal(8, _restrictionTimeSetList[3].StartTime.Hours);
            Assert.Equal(0, _restrictionTimeSetList[3].StartTime.Minutes);
            Assert.Equal(0, _restrictionTimeSetList[3].StartTime.Seconds);
            Assert.Equal(18, _restrictionTimeSetList[3].EndTime.Hours);
            Assert.Equal(0, _restrictionTimeSetList[3].EndTime.Minutes);
            Assert.Equal(0, _restrictionTimeSetList[3].EndTime.Seconds);

            Assert.Equal(8, _restrictionTimeSetList[4].StartTime.Hours);
            Assert.Equal(0, _restrictionTimeSetList[4].StartTime.Minutes);
            Assert.Equal(0, _restrictionTimeSetList[4].StartTime.Seconds);
            Assert.Equal(18, _restrictionTimeSetList[4].EndTime.Hours);
            Assert.Equal(0, _restrictionTimeSetList[4].EndTime.Minutes);
            Assert.Equal(0, _restrictionTimeSetList[4].EndTime.Seconds);

            Assert.Equal(8, _restrictionTimeSetList[5].StartTime.Hours);
            Assert.Equal(0, _restrictionTimeSetList[5].StartTime.Minutes);
            Assert.Equal(0, _restrictionTimeSetList[5].StartTime.Seconds);
            Assert.Equal(18, _restrictionTimeSetList[5].EndTime.Hours);
            Assert.Equal(0, _restrictionTimeSetList[5].EndTime.Minutes);
            Assert.Equal(0, _restrictionTimeSetList[5].EndTime.Seconds);

            Assert.Equal(8, _restrictionTimeSetList[6].StartTime.Hours);
            Assert.Equal(0, _restrictionTimeSetList[6].StartTime.Minutes);
            Assert.Equal(0, _restrictionTimeSetList[6].StartTime.Seconds);
            Assert.Equal(18, _restrictionTimeSetList[6].EndTime.Hours);
            Assert.Equal(0, _restrictionTimeSetList[6].EndTime.Minutes);
            Assert.Equal(0, _restrictionTimeSetList[6].EndTime.Seconds);

            Assert.Equal(8, _restrictionTimeSetList[7].StartTime.Hours);
            Assert.Equal(0, _restrictionTimeSetList[7].StartTime.Minutes);
            Assert.Equal(0, _restrictionTimeSetList[7].StartTime.Seconds);
            Assert.Equal(18, _restrictionTimeSetList[7].EndTime.Hours);
            Assert.Equal(0, _restrictionTimeSetList[7].EndTime.Minutes);
            Assert.Equal(0, _restrictionTimeSetList[7].EndTime.Seconds);
        }

        [Fact]
        public async Task Test_ParseRestrictionTimeList_Case2()
        {
            List<ProductRedeemTimeRestrictionSet> _restrictionTimeSetList = new List<ProductRedeemTimeRestrictionSet>();
            List<string> TimeRestrictions = new List<string> {
                "1;2;3;4;5;6;7;17;18;19;20;21;22;23;"
                , "0;1;21;22;23;"
                , "8;9;10;11;12;13;14;15;16;17;"
                , "8;9;10;11;12;13;14;15;16;17;"
                , "8;9;10;11;12;13;14;15;16;17;"
                , "8;9;10;11;12;13;14;15;16;17;"
                , "8;9;10;11;12;13;14;15;16;17;"
            };
            IStringHelper _stringHelper = new StringHelper();
            IRestrictionHelper _restrictionTimeHelper = new RestrictionHelper(_stringHelper);
            _restrictionTimeSetList = await _restrictionTimeHelper.ParseRestrictionTimeList(TimeRestrictions);

            Assert.Equal(9, _restrictionTimeSetList.Count);
            Assert.Equal(1, _restrictionTimeSetList[0].StartTime.Hours);
            Assert.Equal(0, _restrictionTimeSetList[0].StartTime.Minutes);
            Assert.Equal(0, _restrictionTimeSetList[0].StartTime.Seconds);
            Assert.Equal(8, _restrictionTimeSetList[0].EndTime.Hours);
            Assert.Equal(0, _restrictionTimeSetList[0].EndTime.Minutes);
            Assert.Equal(0, _restrictionTimeSetList[0].EndTime.Seconds);

            Assert.Equal(17, _restrictionTimeSetList[1].StartTime.Hours);
            Assert.Equal(0, _restrictionTimeSetList[1].StartTime.Minutes);
            Assert.Equal(0, _restrictionTimeSetList[1].StartTime.Seconds);
            Assert.Equal(23, _restrictionTimeSetList[1].EndTime.Hours);
            Assert.Equal(59, _restrictionTimeSetList[1].EndTime.Minutes);
            Assert.Equal(59, _restrictionTimeSetList[1].EndTime.Seconds);

            Assert.Equal(0, _restrictionTimeSetList[2].StartTime.Hours);
            Assert.Equal(0, _restrictionTimeSetList[2].StartTime.Minutes);
            Assert.Equal(0, _restrictionTimeSetList[2].StartTime.Seconds);
            Assert.Equal(2, _restrictionTimeSetList[2].EndTime.Hours);
            Assert.Equal(0, _restrictionTimeSetList[2].EndTime.Minutes);
            Assert.Equal(0, _restrictionTimeSetList[2].EndTime.Seconds);

            Assert.Equal(21, _restrictionTimeSetList[3].StartTime.Hours);
            Assert.Equal(0, _restrictionTimeSetList[3].StartTime.Minutes);
            Assert.Equal(0, _restrictionTimeSetList[3].StartTime.Seconds);
            Assert.Equal(23, _restrictionTimeSetList[3].EndTime.Hours);
            Assert.Equal(59, _restrictionTimeSetList[3].EndTime.Minutes);
            Assert.Equal(59, _restrictionTimeSetList[3].EndTime.Seconds);

            Assert.Equal(8, _restrictionTimeSetList[4].StartTime.Hours);
            Assert.Equal(0, _restrictionTimeSetList[4].StartTime.Minutes);
            Assert.Equal(0, _restrictionTimeSetList[4].StartTime.Seconds);
            Assert.Equal(18, _restrictionTimeSetList[4].EndTime.Hours);
            Assert.Equal(0, _restrictionTimeSetList[4].EndTime.Minutes);
            Assert.Equal(0, _restrictionTimeSetList[4].EndTime.Seconds);

            Assert.Equal(8, _restrictionTimeSetList[5].StartTime.Hours);
            Assert.Equal(0, _restrictionTimeSetList[5].StartTime.Minutes);
            Assert.Equal(0, _restrictionTimeSetList[5].StartTime.Seconds);
            Assert.Equal(18, _restrictionTimeSetList[5].EndTime.Hours);
            Assert.Equal(0, _restrictionTimeSetList[5].EndTime.Minutes);
            Assert.Equal(0, _restrictionTimeSetList[5].EndTime.Seconds);

            Assert.Equal(8, _restrictionTimeSetList[6].StartTime.Hours);
            Assert.Equal(0, _restrictionTimeSetList[6].StartTime.Minutes);
            Assert.Equal(0, _restrictionTimeSetList[6].StartTime.Seconds);
            Assert.Equal(18, _restrictionTimeSetList[6].EndTime.Hours);
            Assert.Equal(0, _restrictionTimeSetList[6].EndTime.Minutes);
            Assert.Equal(0, _restrictionTimeSetList[6].EndTime.Seconds);

            Assert.Equal(8, _restrictionTimeSetList[7].StartTime.Hours);
            Assert.Equal(0, _restrictionTimeSetList[7].StartTime.Minutes);
            Assert.Equal(0, _restrictionTimeSetList[7].StartTime.Seconds);
            Assert.Equal(18, _restrictionTimeSetList[7].EndTime.Hours);
            Assert.Equal(0, _restrictionTimeSetList[7].EndTime.Minutes);
            Assert.Equal(0, _restrictionTimeSetList[7].EndTime.Seconds);

            Assert.Equal(8, _restrictionTimeSetList[8].StartTime.Hours);
            Assert.Equal(0, _restrictionTimeSetList[8].StartTime.Minutes);
            Assert.Equal(0, _restrictionTimeSetList[8].StartTime.Seconds);
            Assert.Equal(18, _restrictionTimeSetList[8].EndTime.Hours);
            Assert.Equal(0, _restrictionTimeSetList[8].EndTime.Minutes);
            Assert.Equal(0, _restrictionTimeSetList[8].EndTime.Seconds);
        }

        [Fact]
        public async Task Test_ParseRestrictionTimeList_Case3()
        {
            List<ProductRedeemTimeRestrictionSet> _restrictionTimeSetList = new List<ProductRedeemTimeRestrictionSet>();
            List<string> TimeRestrictions = new List<string> {
                "1;2;3;4;5;6;7;17;18;19;20;21;22;23;"
                , "17;18;19;20;21;22;23;"
                , "8;9;10;11;12;13;14;15;16;17;"
                , "8;9;10;11;12;13;14;15;16;17;"
                , "8;9;10;11;12;13;14;15;16;17;"
                , "8;9;10;11;12;13;14;15;16;17;"
                , "8;9;10;11;12;13;14;15;16;17;"
            };
            IStringHelper _stringHelper = new StringHelper();
            IRestrictionHelper _restrictionTimeHelper = new RestrictionHelper(_stringHelper);
            _restrictionTimeSetList = await _restrictionTimeHelper.ParseRestrictionTimeList(TimeRestrictions);

            Assert.Equal(9, _restrictionTimeSetList.Count);
            Assert.Equal(1, _restrictionTimeSetList[0].StartTime.Hours);
            Assert.Equal(0, _restrictionTimeSetList[0].StartTime.Minutes);
            Assert.Equal(0, _restrictionTimeSetList[0].StartTime.Seconds);
            Assert.Equal(8, _restrictionTimeSetList[0].EndTime.Hours);
            Assert.Equal(0, _restrictionTimeSetList[0].EndTime.Minutes);
            Assert.Equal(0, _restrictionTimeSetList[0].EndTime.Seconds);

            Assert.Equal(17, _restrictionTimeSetList[1].StartTime.Hours);
            Assert.Equal(0, _restrictionTimeSetList[1].StartTime.Minutes);
            Assert.Equal(0, _restrictionTimeSetList[1].StartTime.Seconds);
            Assert.Equal(23, _restrictionTimeSetList[1].EndTime.Hours);
            Assert.Equal(59, _restrictionTimeSetList[1].EndTime.Minutes);
            Assert.Equal(59, _restrictionTimeSetList[1].EndTime.Seconds);

            Assert.Equal(0, _restrictionTimeSetList[2].StartTime.Hours);
            Assert.Equal(0, _restrictionTimeSetList[2].StartTime.Minutes);
            Assert.Equal(0, _restrictionTimeSetList[2].StartTime.Seconds);
            Assert.Equal(2, _restrictionTimeSetList[2].EndTime.Hours);
            Assert.Equal(0, _restrictionTimeSetList[2].EndTime.Minutes);
            Assert.Equal(0, _restrictionTimeSetList[2].EndTime.Seconds);

            Assert.Equal(21, _restrictionTimeSetList[3].StartTime.Hours);
            Assert.Equal(0, _restrictionTimeSetList[3].StartTime.Minutes);
            Assert.Equal(0, _restrictionTimeSetList[3].StartTime.Seconds);
            Assert.Equal(23, _restrictionTimeSetList[3].EndTime.Hours);
            Assert.Equal(59, _restrictionTimeSetList[3].EndTime.Minutes);
            Assert.Equal(59, _restrictionTimeSetList[3].EndTime.Seconds);

            Assert.Equal(8, _restrictionTimeSetList[4].StartTime.Hours);
            Assert.Equal(0, _restrictionTimeSetList[4].StartTime.Minutes);
            Assert.Equal(0, _restrictionTimeSetList[4].StartTime.Seconds);
            Assert.Equal(18, _restrictionTimeSetList[4].EndTime.Hours);
            Assert.Equal(0, _restrictionTimeSetList[4].EndTime.Minutes);
            Assert.Equal(0, _restrictionTimeSetList[4].EndTime.Seconds);

            Assert.Equal(8, _restrictionTimeSetList[5].StartTime.Hours);
            Assert.Equal(0, _restrictionTimeSetList[5].StartTime.Minutes);
            Assert.Equal(0, _restrictionTimeSetList[5].StartTime.Seconds);
            Assert.Equal(18, _restrictionTimeSetList[5].EndTime.Hours);
            Assert.Equal(0, _restrictionTimeSetList[5].EndTime.Minutes);
            Assert.Equal(0, _restrictionTimeSetList[5].EndTime.Seconds);

            Assert.Equal(8, _restrictionTimeSetList[6].StartTime.Hours);
            Assert.Equal(0, _restrictionTimeSetList[6].StartTime.Minutes);
            Assert.Equal(0, _restrictionTimeSetList[6].StartTime.Seconds);
            Assert.Equal(18, _restrictionTimeSetList[6].EndTime.Hours);
            Assert.Equal(0, _restrictionTimeSetList[6].EndTime.Minutes);
            Assert.Equal(0, _restrictionTimeSetList[6].EndTime.Seconds);

            Assert.Equal(8, _restrictionTimeSetList[7].StartTime.Hours);
            Assert.Equal(0, _restrictionTimeSetList[7].StartTime.Minutes);
            Assert.Equal(0, _restrictionTimeSetList[7].StartTime.Seconds);
            Assert.Equal(18, _restrictionTimeSetList[7].EndTime.Hours);
            Assert.Equal(0, _restrictionTimeSetList[7].EndTime.Minutes);
            Assert.Equal(0, _restrictionTimeSetList[7].EndTime.Seconds);

            Assert.Equal(8, _restrictionTimeSetList[8].StartTime.Hours);
            Assert.Equal(0, _restrictionTimeSetList[8].StartTime.Minutes);
            Assert.Equal(0, _restrictionTimeSetList[8].StartTime.Seconds);
            Assert.Equal(18, _restrictionTimeSetList[8].EndTime.Hours);
            Assert.Equal(0, _restrictionTimeSetList[8].EndTime.Minutes);
            Assert.Equal(0, _restrictionTimeSetList[8].EndTime.Seconds);
        }

        [Fact]
        public async Task Test_ParseRestrictionTimeList_Case4()
        {
            List<ProductRedeemTimeRestrictionSet> _restrictionTimeSetList = new List<ProductRedeemTimeRestrictionSet>();
            List<string> TimeRestrictions = new List<string> {
                "0;1;2;3;4;5;6;7;8;9;10;11;12;15;16;17;18;19;20;21;22;23;"
                , ""
                , "0;1;2;3;4;5;6;7;8;9;10;11;12;13;14;15;16;17;18;19;20;21;22;23;"
                , "0;1;2;3;4;5;6;7;8;9;10;11;12;13;14;15;16;17;18;19;20;21;22;23;"
                , "0;1;2;3;4;5;6;7;8;9;10;15;16;17;18;19;20;21;22;23;"
                , "0;1;2;3;4;5;6;7;8;9;10;11;12;13;14;15;16;17;18;19;20;21;22;23;"
                , "0;1;2;3;4;5;6;7;8;9;10;11;12;15;16;17;18;19;20;21;22;23;"
            };
            IStringHelper _stringHelper = new StringHelper();
            IRestrictionHelper _restrictionTimeHelper = new RestrictionHelper(_stringHelper);
            _restrictionTimeSetList = await _restrictionTimeHelper.ParseRestrictionTimeList(TimeRestrictions);

            Assert.Equal(9, _restrictionTimeSetList.Count);
        }

        [Fact]
        public async Task Test_SerializeRestrictionTimeList_Case1()
        {
            List<string> list = new List<string>();
            List<ProductRedeemTimeRestrictionSet> _restrictionTimeSetList = new List<ProductRedeemTimeRestrictionSet> { 
                new ProductRedeemTimeRestrictionSet{ DayOfTheWeek = 0, StartTime = new TimeSpan(1,0,0), EndTime = new TimeSpan(8,0,0) },
                new ProductRedeemTimeRestrictionSet{ DayOfTheWeek = 0, StartTime = new TimeSpan(17,0,0), EndTime = new TimeSpan(23,59,59) },
                new ProductRedeemTimeRestrictionSet{ DayOfTheWeek = 1, StartTime = new TimeSpan(0,0,0), EndTime = new TimeSpan(2,0,0) },
                new ProductRedeemTimeRestrictionSet{ DayOfTheWeek = 1, StartTime = new TimeSpan(21,0,0), EndTime = new TimeSpan(23,59,59) },
                new ProductRedeemTimeRestrictionSet{ DayOfTheWeek = 2, StartTime = new TimeSpan(8,0,0), EndTime = new TimeSpan(18,0,0) },
                new ProductRedeemTimeRestrictionSet{ DayOfTheWeek = 3, StartTime = new TimeSpan(8,0,0), EndTime = new TimeSpan(18,0,0) },
                new ProductRedeemTimeRestrictionSet{ DayOfTheWeek = 4, StartTime = new TimeSpan(8,0,0), EndTime = new TimeSpan(18,0,0) },
                new ProductRedeemTimeRestrictionSet{ DayOfTheWeek = 5, StartTime = new TimeSpan(8,0,0), EndTime = new TimeSpan(18,0,0) },
                new ProductRedeemTimeRestrictionSet{ DayOfTheWeek = 6, StartTime = new TimeSpan(8,0,0), EndTime = new TimeSpan(18,0,0) },
            };

            IStringHelper _stringHelper = new StringHelper();
            IRestrictionHelper _restrictionTimeHelper = new RestrictionHelper(_stringHelper);
            list = await _restrictionTimeHelper.SerializeRestrictionTimeList(_restrictionTimeSetList);

            Assert.Equal("1;2;3;4;5;6;7;17;18;19;20;21;22;23;", list[0]);
            Assert.Equal("0;1;21;22;23;", list[1]);
            Assert.Equal("8;9;10;11;12;13;14;15;16;17;", list[2]);
            Assert.Equal("8;9;10;11;12;13;14;15;16;17;", list[3]);
            Assert.Equal("8;9;10;11;12;13;14;15;16;17;", list[4]);
            Assert.Equal("8;9;10;11;12;13;14;15;16;17;", list[5]);
            Assert.Equal("8;9;10;11;12;13;14;15;16;17;", list[6]);
        }

        [Fact]
        public async Task Test_SerializeRestrictionTimeList_Case2()
        {
            List<string> list = new List<string>();
            List<ProductRedeemTimeRestrictionSet> _restrictionTimeSetList = new List<ProductRedeemTimeRestrictionSet> {
                new ProductRedeemTimeRestrictionSet{ DayOfTheWeek = 0, StartTime = new TimeSpan(1,0,0), EndTime = new TimeSpan(8,0,0) },
            };

            IStringHelper _stringHelper = new StringHelper();
            IRestrictionHelper _restrictionTimeHelper = new RestrictionHelper(_stringHelper);
            list = await _restrictionTimeHelper.SerializeRestrictionTimeList(_restrictionTimeSetList);

            Assert.Equal("1;2;3;4;5;6;7;", list[0]);
            Assert.Equal(string.Empty, list[1]);
            Assert.Equal(string.Empty, list[2]);
            Assert.Equal(string.Empty, list[3]);
            Assert.Equal(string.Empty, list[4]);
            Assert.Equal(string.Empty, list[5]);
            Assert.Equal(string.Empty, list[6]);
        }

        [Fact]
        public async Task Test_SerializeRestrictionTimeList_Case3()
        {
            List<string> list = new List<string>();
            List<ProductRedeemTimeRestrictionSet> _restrictionTimeSetList = new List<ProductRedeemTimeRestrictionSet> {
                new ProductRedeemTimeRestrictionSet{ DayOfTheWeek = 4, StartTime = new TimeSpan(1,0,0), EndTime = new TimeSpan(8,0,0) },
            };

            IStringHelper _stringHelper = new StringHelper();
            IRestrictionHelper _restrictionTimeHelper = new RestrictionHelper(_stringHelper);
            list = await _restrictionTimeHelper.SerializeRestrictionTimeList(_restrictionTimeSetList);

            Assert.Equal(string.Empty, list[0]);
            Assert.Equal(string.Empty, list[1]);
            Assert.Equal(string.Empty, list[2]);
            Assert.Equal(string.Empty, list[3]);
            Assert.Equal("1;2;3;4;5;6;7;", list[4]);
            Assert.Equal(string.Empty, list[5]);
            Assert.Equal(string.Empty, list[6]);
        }

        [Fact]
        public async Task Test_SerializeRestrictionTime_Case1()
        {
            IStringHelper _stringHelper = new StringHelper();
            IRestrictionHelper _restrictionTimeHelper = new RestrictionHelper(_stringHelper);
            ProductRedeemTimeRestrictionSet _restrictionTimeSet0 = new ProductRedeemTimeRestrictionSet { DayOfTheWeek = 0, StartTime = new TimeSpan(1, 0, 0), EndTime = new TimeSpan(8, 0, 0) };
            string set0 = await _restrictionTimeHelper.SerializeRestrictionTime(_restrictionTimeSet0);
        }

        [Fact]
        public async Task Test_IsTimeRestrictionModified_case1()
        {
            List<string> OriginalTimeRestrictions = new List<string> {
                "1;2;3;4;5;6;7;17;18;19;20;21;22;23;",
                "0;1;21;22;23;",
                "8;9;10;11;12;13;14;15;16;17;",
                "8;9;10;11;12;13;14;15;16;17;",
                "8;9;10;11;12;13;14;15;16;17;",
                "8;9;10;11;12;13;14;15;16;17;",
                "8;9;10;11;12;13;14;15;16;17;",
            };
            List<string> RequestTimeRestrictions = new List<string> {
                "1;2;3;4;5;6;7;17;18;19;20;21;22;23;",
                "0;1;21;22;23;",
                "8;9;10;11;12;13;14;15;16;17;",
                "8;9;10;11;12;13;14;15;16;17;",
                "8;9;10;11;12;13;14;15;16;17;",
                "8;9;10;11;12;13;14;15;16;17;",
                "8;9;10;11;12;13;14;15;16;17;",
            };
            IStringHelper _stringHelper = new StringHelper();
            IRestrictionHelper _restrictionTimeHelper = new RestrictionHelper(_stringHelper);
            bool flag = await _restrictionTimeHelper.IsTimeRestrictionModified(OriginalTimeRestrictions, RequestTimeRestrictions);
            Assert.False(flag);
        }

        [Fact]
        public async Task Test_IsTimeRestrictionModified_case2()
        {
            List<string> OriginalTimeRestrictions = new List<string> {
                "1;2;3;4;5;6;7;17;18;19;20;21;22;23;",
                null,
                "8;9;10;11;12;13;14;15;16;17;",
                "8;9;10;11;12;13;14;15;16;17;",
                "8;9;10;11;12;13;14;15;16;17;",
                "8;9;10;11;12;13;14;15;16;17;",
                "8;9;10;11;12;13;14;15;16;",
            };
            List<string> RequestTimeRestrictions = new List<string> {
                "1;2;3;4;5;6;7;17;18;19;20;21;22;23;",
                "",
                "8;9;10;11;12;13;14;15;16;17;",
                "8;9;10;11;12;13;14;15;16;17;",
                "8;9;10;11;12;13;14;15;16;17;",
                "8;9;10;11;12;13;14;15;16;17;",
                "8;9;10;11;12;13;14;15;16;",
            };
            IStringHelper _stringHelper = new StringHelper();
            IRestrictionHelper _restrictionTimeHelper = new RestrictionHelper(_stringHelper);
            bool flag = await _restrictionTimeHelper.IsTimeRestrictionModified(OriginalTimeRestrictions, RequestTimeRestrictions);
            Assert.False(flag);
        }

        [Fact]
        public async Task Test_IsDateRestrictionModified_Case0()
        {
            List<DateTime> OriginalDateRestrictions = new List<DateTime> { 
                new DateTime(2022,1,24),
                new DateTime(2022,1,25),
            };
            List<DateTime> RequestDateRestrictions = new List<DateTime> {
                new DateTime(2022,1,24),
                new DateTime(2022,1,25),
            };
            IStringHelper _stringHelper = new StringHelper();
            IRestrictionHelper _restrictionTimeHelper = new RestrictionHelper(_stringHelper);
            bool flag = await _restrictionTimeHelper.IsDateRestrictionModified(OriginalDateRestrictions, RequestDateRestrictions);
            Assert.False(flag);
        }

        [Fact]
        public async Task Test_IsDateRestrictionModified_Case1()
        {
            List<DateTime> OriginalDateRestrictions = new List<DateTime> {
                new DateTime(2022,1,24),
                new DateTime(2022,1,26),
            };
            List<DateTime> RequestDateRestrictions = new List<DateTime> {
                new DateTime(2022,1,24),
                new DateTime(2022,1,25),
            };
            IStringHelper _stringHelper = new StringHelper();
            IRestrictionHelper _restrictionTimeHelper = new RestrictionHelper(_stringHelper);
            bool flag = await _restrictionTimeHelper.IsDateRestrictionModified(OriginalDateRestrictions, RequestDateRestrictions);
            Assert.True(flag);
        }

        [Fact]
        public async Task Test_IsDateRestrictionModified_Case2()
        {
            List<DateTime> OriginalDateRestrictions = new List<DateTime> {
                new DateTime(2022,1,24),
            };
            List<DateTime> RequestDateRestrictions = new List<DateTime> {
                new DateTime(2022,1,24),
                new DateTime(2022,1,25),
            };
            IStringHelper _stringHelper = new StringHelper();
            IRestrictionHelper _restrictionTimeHelper = new RestrictionHelper(_stringHelper);
            bool flag = await _restrictionTimeHelper.IsDateRestrictionModified(OriginalDateRestrictions, RequestDateRestrictions);
            Assert.True(flag);
        }

        [Fact]
        public async Task Test_HasDuplicatedDate_Case0()
        {
            List<DateTime> RequestDateRestrictions = new List<DateTime> {
                new DateTime(2022,1,24),
                new DateTime(2022,1,24)
            };
            IStringHelper _stringHelper = new StringHelper();
            IRestrictionHelper _restrictionTimeHelper = new RestrictionHelper(_stringHelper);
            bool flag = await _restrictionTimeHelper.HasDuplicatedDate(RequestDateRestrictions);
            Assert.True(flag);
        }

        [Fact]
        public async Task Test_HasDuplicatedDate_Case1()
        {
            List<DateTime> RequestDateRestrictions = new List<DateTime> {
                new DateTime(2022,1,24),
                new DateTime(2022,1,25)
            };
            IStringHelper _stringHelper = new StringHelper();
            IRestrictionHelper _restrictionTimeHelper = new RestrictionHelper(_stringHelper);
            bool flag = await _restrictionTimeHelper.HasDuplicatedDate(RequestDateRestrictions);
            Assert.False(flag);
        }

        [Fact]
        public async Task Test_IsFormatCorrect_Case1()
        {
            IStringHelper _stringHelper = new StringHelper();
            IRestrictionHelper _restrictionTimeHelper = new RestrictionHelper(_stringHelper);
            bool result = await _restrictionTimeHelper.IsFormatCorrect(new List<string> { "1;2;3;", "4;5;6;", "7;8;9;" });
            Assert.True(result);
        }

        [Fact]
        public async Task Test_IsFormatCorrect_Case2()
        {
            IStringHelper _stringHelper = new StringHelper();
            IRestrictionHelper _restrictionTimeHelper = new RestrictionHelper(_stringHelper);
            bool result = await _restrictionTimeHelper.IsFormatCorrect(new List<string> { "1;2;3", "4;5;6;", "7;8;9;" });
            Assert.False(result);
        }

        [Fact]
        public async Task Test_IsFormatCorrect_Case3()
        {
            IStringHelper _stringHelper = new StringHelper();
            IRestrictionHelper _restrictionTimeHelper = new RestrictionHelper(_stringHelper);
            bool result = await _restrictionTimeHelper.IsFormatCorrect(new List<string> { "1;2;s;", "4;5;6;", "7;8;9;" });
            Assert.False(result);
        }

        [Fact]
        public async Task Test_IsFormatCorrect_Case4()
        {
            IStringHelper _stringHelper = new StringHelper();
            IRestrictionHelper _restrictionTimeHelper = new RestrictionHelper(_stringHelper);
            bool result = await _restrictionTimeHelper.IsFormatCorrect(new List<string> { "lkjbjlkjlj", "4;5;6;", "7;8;9;" });
            Assert.False(result);
        }

        [Fact]
        public async Task Test_IsRangeCorrect_Case1()
        {
            IStringHelper _stringHelper = new StringHelper();
            IRestrictionHelper _restrictionTimeHelper = new RestrictionHelper(_stringHelper);
            bool result = await _restrictionTimeHelper.IsRangeCorrect(new List<string> { "1;2;3;", "4;5;6;"});
            Assert.True(result);
        }

        [Fact]
        public async Task Test_IsRangeCorrect_Case2()
        {
            IStringHelper _stringHelper = new StringHelper();
            IRestrictionHelper _restrictionTimeHelper = new RestrictionHelper(_stringHelper);
            bool result = await _restrictionTimeHelper.IsRangeCorrect(new List<string> { "1;2;33;", "4;5;66;" });
            Assert.False(result);
        }

        [Fact]
        public async Task Test_HasDuplicatedHours_Case1()
        {
            IStringHelper _stringHelper = new StringHelper();
            IRestrictionHelper _restrictionTimeHelper = new RestrictionHelper(_stringHelper);
            bool result = await _restrictionTimeHelper.HasDuplicatedHours(new List<string> { "1;2;2;", "4;5;5;" });
            Assert.True(result);
        }

        [Fact]
        public async Task Test_HasDuplicatedHours_Case2()
        {
            IStringHelper _stringHelper = new StringHelper();
            IRestrictionHelper _restrictionTimeHelper = new RestrictionHelper(_stringHelper);
            bool result = await _restrictionTimeHelper.HasDuplicatedHours(new List<string> { "1;2;3;", "4;5;6;" });
            Assert.False(result);
        }

        [Fact]
        public async Task Test_ValidWeekDay_Case1()
        {
            IStringHelper _stringHelper = new StringHelper();
            IRestrictionHelper _restrictionTimeHelper = new RestrictionHelper(_stringHelper);
            bool result = await _restrictionTimeHelper.ValidWeekDay(new List<int> { 0,1,2,3,4,5,6 });
            Assert.True(result);
        }

        [Fact]
        public async Task Test_ValidWeekDay_Case2()
        {
            IStringHelper _stringHelper = new StringHelper();
            IRestrictionHelper _restrictionTimeHelper = new RestrictionHelper(_stringHelper);
            bool result = await _restrictionTimeHelper.ValidWeekDay(new List<int> { 0, 1, 1, 3, 4, 5, 6 });
            Assert.False(result);
        }

        [Fact]
        public async Task Test_ValidWeekDay_Case3()
        {
            IStringHelper _stringHelper = new StringHelper();
            IRestrictionHelper _restrictionTimeHelper = new RestrictionHelper(_stringHelper);
            bool result = await _restrictionTimeHelper.ValidWeekDay(new List<int> { 0, 1, 2, 3, 4, 5, 6, 7 });
            Assert.False(result);
        }

        [Fact]
        public async Task Test_ValidWeekDay_Case4()
        {
            IStringHelper _stringHelper = new StringHelper();
            IRestrictionHelper _restrictionTimeHelper = new RestrictionHelper(_stringHelper);
            bool result = await _restrictionTimeHelper.ValidWeekDay(new List<int> { 0, 1, 2, 3, 4, 5, 60 });
            Assert.False(result);
        }
    }
}