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
    public class TestChangesTrackerHelper
    {
        [Fact]
        public void Test_AddItems_Case0()
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
            IChangesTrackerHelper<ExternalPropertyItem, ExternalPropertyItem> _tHelper = new ChangesTrackerHelper<ExternalPropertyItem, ExternalPropertyItem>(
                list1, list2, _helper.CompareExternalPropertyIsTheSame
                );
            int? count = _tHelper.AddedItems?.ToList().Count;
            Assert.Equal(0, count);
        }

        [Fact]
        public void Test_AddItems_Case1()
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
            IChangesTrackerHelper<ExternalPropertyItem, ExternalPropertyItem> _tHelper = new ChangesTrackerHelper<ExternalPropertyItem, ExternalPropertyItem>(
                list1, list2, _helper.CompareExternalPropertyIsTheSame
                );
            int? count = _tHelper.AddedItems?.ToList().Count;
            Assert.Equal(1, count);
        }

        [Fact]
        public void Test_AddItems_Case2()
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
            };
            IChangesTrackerHelper<ExternalPropertyItem, ExternalPropertyItem> _tHelper = new ChangesTrackerHelper<ExternalPropertyItem, ExternalPropertyItem>(
                list1, list2, _helper.CompareExternalPropertyIsTheSame
                );
            int? count = _tHelper.AddedItems?.ToList().Count;
            Assert.Equal(0, count);
        }

        [Fact]
        public void Test_RemoveItems_Case0()
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
            IChangesTrackerHelper<ExternalPropertyItem, ExternalPropertyItem> _tHelper = new ChangesTrackerHelper<ExternalPropertyItem, ExternalPropertyItem>(
                list1, list2, _helper.CompareExternalPropertyIsTheSame
                );
            int? count = _tHelper.RemovedItems?.ToList().Count;
            Assert.Equal(0, count);
        }

        [Fact]
        public void Test_RemoveItems_Case1()
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
            IChangesTrackerHelper<ExternalPropertyItem, ExternalPropertyItem> _tHelper = new ChangesTrackerHelper<ExternalPropertyItem, ExternalPropertyItem>(
                list1, list2, _helper.CompareExternalPropertyIsTheSame
                );
            int? count = _tHelper.RemovedItems?.ToList().Count;
            Assert.Equal(0, count);
        }

        [Fact]
        public void Test_RemoveItems_Case2()
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
            };
            IChangesTrackerHelper<ExternalPropertyItem, ExternalPropertyItem> _tHelper = new ChangesTrackerHelper<ExternalPropertyItem, ExternalPropertyItem>(
                list1, list2, _helper.CompareExternalPropertyIsTheSame
                );
            int? count = _tHelper.RemovedItems?.ToList().Count;
            Assert.Equal(1, count);
        }
    }
}
