using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TXC.Proto.Product;

namespace Services.Utility.Helper
{
    public delegate bool CompareExternalPropertyIsTheSameDel(ExternalPropertyItem item1, ExternalPropertyItem item2);
    public delegate bool HasExternalPropertyModifiedDel(IEnumerable<ExternalPropertyItem> list1, IEnumerable<ExternalPropertyItem> list2, Func<ExternalPropertyItem, ExternalPropertyItem, bool> areEqual);
    public delegate bool HasDuplicatePropertyNameDel(IEnumerable<ExternalPropertyItem> request);
    public delegate bool IsPropertyNameEmptyDel(IEnumerable<ExternalPropertyItem> request);
    public interface IExternalPropertyHelper
    {
        bool CompareExternalPropertyIsTheSame(ExternalPropertyItem item1, ExternalPropertyItem item2);
        bool HasExternalPropertyModified(IEnumerable<ExternalPropertyItem> list1, IEnumerable<ExternalPropertyItem> list2, Func<ExternalPropertyItem, ExternalPropertyItem, bool> areEqual);
        bool HasDuplicatePropertyName(IEnumerable<ExternalPropertyItem> request);
        bool IsPropertyNameEmpty(IEnumerable<ExternalPropertyItem> request);
    }
    public class ExternalPropertyHelper : IExternalPropertyHelper
    {
        private readonly IStringHelper _stringHelper = null;
        public ExternalPropertyHelper(IStringHelper stringHelper)
        {
            _stringHelper = stringHelper;
        }
        public bool CompareExternalPropertyIsTheSame(ExternalPropertyItem item1, ExternalPropertyItem item2)
        {
            if (item1 == null && item2 != null)
            {
                return false;
            }
            else if (item1 != null && item2 == null)
            {
                return false;
            }
            else if (item1 == null && item2 == null)
            {
                return true;
            }
            else
            {
                if (_stringHelper.IsTheSame(item1.PropertyName, item2.PropertyName) 
                    && _stringHelper.IsTheSame(item1.PropertyValue, item2.PropertyValue) 
                    && _stringHelper.IsTheSame(item1.Description, item2.Description))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool HasExternalPropertyModified(IEnumerable<ExternalPropertyItem> list1, IEnumerable<ExternalPropertyItem> list2, Func<ExternalPropertyItem, ExternalPropertyItem, bool> areEqual)
        {
            if (list1 == null && list2 != null)
            {
                return true;
            }
            else if (list1 != null && list2 == null)
            {
                return true;
            }
            else if (list1 == null && list2 == null)
            {
                return false;
            }
            else
            {
                if (list1.Count() != list2.Count())
                {
                    return true;
                }
                else
                {
                    foreach (ExternalPropertyItem item in list1)
                    {
                        if (!list2.Any(o => areEqual(item, o)))
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        public bool HasDuplicatePropertyName(IEnumerable<ExternalPropertyItem> request)
        {
            if (request == null)
            {
                return false;
            }
            bool anyDuplicate = request.GroupBy(x => x.PropertyName).Any(g => g.Count() > 1);
            return anyDuplicate;
        }

        public bool IsPropertyNameEmpty(IEnumerable<ExternalPropertyItem> request)
        {
            if (request == null)
            {
                return false;
            }
            foreach (ExternalPropertyItem item in request)
            {
                if (string.IsNullOrEmpty(item.PropertyName) || string.IsNullOrWhiteSpace(item.PropertyName))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
