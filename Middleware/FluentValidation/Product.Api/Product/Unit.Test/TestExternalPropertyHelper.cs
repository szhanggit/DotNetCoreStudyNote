using Domain.Models;
using Services.Utility.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TXC.Proto.Product;
using Xunit;

namespace Unit.Test
{
    public class TestExternalPropertyHelper
    {
        [Fact]
        public void Test_CompareExternalPropertyIsTheSame_Case0()
        {
            IStringHelper _stringHelper = new StringHelper();
            IExternalPropertyHelper _helper = new ExternalPropertyHelper(_stringHelper);
            ExternalPropertyItem item1 = new ExternalPropertyItem
            { 
                PropertyName = "name1", 
                PropertyValue = "value1", 
                Description = "des1"
            };
            ExternalPropertyItem item2 = new ExternalPropertyItem
            {
                PropertyName = "name1",
                PropertyValue = "value1",
                Description = "des1"
            };
            bool result = _helper.CompareExternalPropertyIsTheSame(item1, item2);
            Assert.True(result);
        }

        [Fact]
        public void Test_CompareExternalPropertyIsTheSame_Case1()
        {
            IStringHelper _stringHelper = new StringHelper();
            IExternalPropertyHelper _helper = new ExternalPropertyHelper(_stringHelper);
            ExternalPropertyItem item1 = new ExternalPropertyItem
            {
                PropertyName = "name1",
                PropertyValue = "value1",
                Description = null
            };
            ExternalPropertyItem item2 = new ExternalPropertyItem
            {
                PropertyName = "name1",
                PropertyValue = "value1",
                Description = "des1"
            };
            bool result = _helper.CompareExternalPropertyIsTheSame(item1, item2);
            Assert.False(result);
        }

        [Fact]
        public void Test_CompareExternalPropertyIsTheSame_Case2()
        {
            IStringHelper _stringHelper = new StringHelper();
            IExternalPropertyHelper _helper = new ExternalPropertyHelper(_stringHelper);
            ExternalPropertyItem item1 = null;
            ExternalPropertyItem item2 = new ExternalPropertyItem
            {
                PropertyName = "name1",
                PropertyValue = "value1",
                Description = "des1"
            };
            bool result = _helper.CompareExternalPropertyIsTheSame(item1, item2);
            Assert.False(result);
        }

        [Fact]
        public void Test_CompareExternalPropertyIsTheSame_Case3()
        {
            IStringHelper _stringHelper = new StringHelper();
            IExternalPropertyHelper _helper = new ExternalPropertyHelper(_stringHelper);
            ExternalPropertyItem item1 = new ExternalPropertyItem
            {
                PropertyName = "name1",
                PropertyValue = "value1",
                Description = "des1"
            };
            ExternalPropertyItem item2 = new ExternalPropertyItem
            {
                PropertyName = "name1",
                PropertyValue = "value1",
                Description = "des2"
            };
            bool result = _helper.CompareExternalPropertyIsTheSame(item1, item2);
            Assert.False(result);
        }

        [Fact]
        public void Test_HasExternalPropertyModified_Case0()
        {
            IStringHelper _stringHelper = new StringHelper();
            IExternalPropertyHelper _helper = new ExternalPropertyHelper(_stringHelper);
            List<ExternalPropertyItem> list1 = new List<ExternalPropertyItem> {
                new ExternalPropertyItem { PropertyName = "A", PropertyValue = "A", Description = "A"},
                new ExternalPropertyItem { PropertyName = "B", PropertyValue = "B", Description = "B"},
                new ExternalPropertyItem { PropertyName = "C", PropertyValue = "C", Description = "C"},
            };
            List<ExternalPropertyItem> list2 = new List<ExternalPropertyItem> {
                new ExternalPropertyItem { PropertyName = "A", PropertyValue = "A", Description = "A"},
                new ExternalPropertyItem { PropertyName = "B", PropertyValue = "B", Description = "B"},
                new ExternalPropertyItem { PropertyName = "C", PropertyValue = "C", Description = "C"},
            };
            bool result = _helper.HasExternalPropertyModified(list1, list2, _helper.CompareExternalPropertyIsTheSame);
            Assert.False(result) ;
        }

        [Fact]
        public void Test_HasExternalPropertyModified_Case1()
        {
            IStringHelper _stringHelper = new StringHelper();
            IExternalPropertyHelper _helper = new ExternalPropertyHelper(_stringHelper);
            List<ExternalPropertyItem> list1 = null;
            List<ExternalPropertyItem> list2 = new List<ExternalPropertyItem> {
                new ExternalPropertyItem { PropertyName = "A", PropertyValue = "A", Description = "A"},
                new ExternalPropertyItem { PropertyName = "B", PropertyValue = "B", Description = "B"},
                new ExternalPropertyItem { PropertyName = "C", PropertyValue = "C", Description = "C"},
            };
            bool result = _helper.HasExternalPropertyModified(list1, list2, _helper.CompareExternalPropertyIsTheSame);
            Assert.True(result);
        }

        [Fact]
        public void Test_HasExternalPropertyModified_Case2()
        {
            IStringHelper _stringHelper = new StringHelper();
            IExternalPropertyHelper _helper = new ExternalPropertyHelper(_stringHelper);
            List<ExternalPropertyItem> list1 = new List<ExternalPropertyItem> {
                new ExternalPropertyItem { PropertyName = "A", PropertyValue = "A", Description = "A"},
                new ExternalPropertyItem { PropertyName = "B", PropertyValue = "B", Description = "B"},
            };
            List<ExternalPropertyItem> list2 = new List<ExternalPropertyItem> {
                new ExternalPropertyItem { PropertyName = "A", PropertyValue = "A", Description = "A"},
                new ExternalPropertyItem { PropertyName = "B", PropertyValue = "B", Description = "B"},
                new ExternalPropertyItem { PropertyName = "C", PropertyValue = "C", Description = "C"},
            };
            bool result = _helper.HasExternalPropertyModified(list1, list2, _helper.CompareExternalPropertyIsTheSame);
            Assert.True(result);
        }

        [Fact]
        public void Test_HasExternalPropertyModified_Case3()
        {
            IStringHelper _stringHelper = new StringHelper();
            IExternalPropertyHelper _helper = new ExternalPropertyHelper(_stringHelper);
            List<ExternalPropertyItem> list1 = new List<ExternalPropertyItem> {
                new ExternalPropertyItem { PropertyName = "A", PropertyValue = "A", Description = "A"},
                new ExternalPropertyItem { PropertyName = "B", PropertyValue = "B", Description = "B"},
                new ExternalPropertyItem { PropertyName = "C", PropertyValue = "C", Description = "C"},
            };
            List<ExternalPropertyItem> list2 = new List<ExternalPropertyItem> {
                new ExternalPropertyItem { PropertyName = "A", PropertyValue = "A", Description = "A"},
                new ExternalPropertyItem { PropertyName = "B", PropertyValue = "B", Description = "B"},
                new ExternalPropertyItem { PropertyName = "C", PropertyValue = "C", Description = null},
            };
            bool result = _helper.HasExternalPropertyModified(list1, list2, _helper.CompareExternalPropertyIsTheSame);
            Assert.True(result);
        }
    }
}
