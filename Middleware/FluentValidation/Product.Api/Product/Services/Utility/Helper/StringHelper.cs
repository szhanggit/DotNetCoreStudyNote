using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Utility.Helper
{
    public interface IStringHelper
    {
        string Append(params object[] objs);
        bool IsTheSame(string item1, string item2);
        string RemoveLast(string text, string character);
    }
    public class StringHelper : IStringHelper
    {
        public string Append(params object[] objs)
        {
            var stringBuilder = new StringBuilder();

            foreach (var obj in objs)
            {
                stringBuilder.Append(obj);
            }

            return stringBuilder.ToString();
        }

        public bool IsTheSame(string item1, string item2)
        {
            if (item1 == null && item2 == null)
            {
                return true;
            }
            else if (item1 != null && item2 == null)
            {
                return false;
            }
            else if (item1 == null && item2 != null)
            {
                return false;
            }
            else
            {
                if (item1.Equals(item2))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public string RemoveLast(string text, string character)
        {
            if (text.Length < 1) return text;
            return text.Remove(text.ToString().LastIndexOf(character), character.Length);
        }
    }
}
