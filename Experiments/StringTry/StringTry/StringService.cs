using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringTry
{
    public class StringService
    {
        public StringService()
        {

        }

        public void modifyString(string input) {
            input = $"This is the modified {input}.";        
        }

        public void modifyPersonInfo(PersonInfo pi) {
            pi.FirstName = $"This is the modified {pi.FirstName}.";
        }
    }
}
