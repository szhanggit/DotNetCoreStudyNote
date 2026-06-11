using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringTry
{
    class Program
    {
        static void Main(string[] args)
        {
            String input = "Hello";
            PersonInfo personInfo = new PersonInfo { FirstName="William", LastName="Black" };
            StringService ss = new StringService();
            ss.modifyString(input);
            ss.modifyPersonInfo(personInfo);
            Console.WriteLine(input);
            Console.WriteLine(personInfo.FirstName);
            Console.ReadKey();
        }
    }
}
