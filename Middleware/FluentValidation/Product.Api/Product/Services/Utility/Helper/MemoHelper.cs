using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Utility.Helper
{
    public interface IMemoHelper
    {
        bool CompareMemoIsTheSame(ProductMemo original, ProductMemo request);
    }
    public class MemoHelper : IMemoHelper
    {
        private readonly IStringHelper _stringHelper = null;
        public MemoHelper(IStringHelper stringHelper)
        {
            _stringHelper = stringHelper;
        }

        public bool CompareMemoIsTheSame(ProductMemo original, ProductMemo request)
        {
            if (original == null && request != null)
            {
                return false;
            }
            else if (original != null && request == null)
            {
                return false;
            }
            else if (original == null && request == null)
            {
                return true;
            }
            else
            {
                if (_stringHelper.IsTheSame(original.OperationNote, request.OperationNote)
                    && _stringHelper.IsTheSame(original.SalesNote, request.SalesNote)
                    && _stringHelper.IsTheSame(original.CustomerServiceNote, request.CustomerServiceNote))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}
