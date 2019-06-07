using ComparerCode.Base;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Text;

namespace ComparerCode
{

    [TestClass]
    public class BuildStrings : PerformanceTestClass
    {
        static List<string> toAppend;
        //good value 30e3
        static double string_list_size = 45e3;

        static BuildStrings()
        {
            toAppend = GetStringList(string_list_size, 10);
            AddToLog(new { string_list_size });
        }
       
        [TestMethod]
        public void Test01_AppendWithPlus()
        {
            string result = string.Empty;
            for (int i = 0; i < string_list_size; i++)
            {
                result += toAppend[i];
            }
        }

        [TestMethod]
        public void Test02_AppendWithConcat()
        {
            string result = string.Empty;
            for (int i = 0; i < string_list_size; i++)
            {
                result = string.Concat(result, toAppend[i]);
            }
        }

        [TestMethod]
        public void Test03_AppendWithOneConcat()
        {
            string result = string.Concat(toAppend);
        }

        [TestMethod]
        public void Test04_AppendWithSB()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < string_list_size; i++)
            {
                sb.Append(toAppend[i]);
            }
            string result = sb.ToString();
        }

        [TestMethod]
        public void TestConstant01_interpolationAndBreakLineSupport()
        {
            int param = 2;           
                string query =
$@"select count(guidPointer) from BACATALOGREFERENCE 
group by guidPointer
having count(guidPointer)>{param}";
            
        }

        [TestMethod]
        public void TestConstant02_stringByParts()
        {
            int param = 2;
                string oldQuery = "select count(guidPointer) from BACATALOGREFERENCE \r\n" +
                "group by guidPointer\r\n" +
                "having count(guidPointer) >" + param;
            
        }
    }
}
