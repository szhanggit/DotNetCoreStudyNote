using Google.Protobuf.WellKnownTypes;
using Services.Utility.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Unit.Test
{
    public class TestStringHelper
    {
        [Fact]
        public void Test_IsTheSame_Case0()
        {
            IStringHelper _helper = new StringHelper();
            String Item1 = "asdfsdf";
            String Item2 = null;
            bool result = _helper.IsTheSame(Item1, Item2);
            Assert.False(result);
        }

        [Fact]
        public void Test_IsTheSame_Case1()
        {
            IStringHelper _helper = new StringHelper();
            String Item1 = "asdfsdf";
            String Item2 = "sdfsd";
            bool result = _helper.IsTheSame(Item1, Item2);
            Assert.False(result);
        }

        [Fact]
        public void Test_IsTheSame_Case2()
        {
            IStringHelper _helper = new StringHelper();
            String Item1 = "asdfsdf";
            String Item2 = "asdfsdf";
            bool result = _helper.IsTheSame(Item1, Item2);
            Assert.True(result);
        }

        [Fact]
        public void Test_IsTheSame_Case3()
        {
            IStringHelper _helper = new StringHelper();
            String Item1 = null;
            String Item2 = null;
            bool result = _helper.IsTheSame(Item1, Item2);
            Assert.True(result);
        }

        [Fact]
        public void Test_RemoveLast_Case1()
        {
            IStringHelper _helper = new StringHelper();
            string key = "1;2;3;4;5;6;";
            string result = _helper.RemoveLast(key, ";");
        }
    }
}
