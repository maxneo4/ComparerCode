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
        static double repetitiveLoops = 30e3;

        static BuildStrings()
        {
            toAppend = GetStringList(repetitiveLoops, 10);
            AddToLog(new { repetitiveLoops });
        }
       
        [TestMethod]
        public void TestAppendWithPlus()
        {
            string result = string.Empty;
            for (int i = 0; i < repetitiveLoops; i++)
            {
                result += toAppend[i];
            }
        }

        [TestMethod]
        public void TestAppendWithConcat()
        {
            string result = string.Empty;
            for (int i = 0; i < repetitiveLoops; i++)
            {
                result = string.Concat(result, toAppend[i]);
            }
        }

        [TestMethod]
        public void TestAppendWithOneConcat()
        {
            string result = string.Concat(toAppend);
        }

        [TestMethod]
        public void TestAppendWithSB()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < repetitiveLoops; i++)
            {
                sb.Append(toAppend[i]);
            }
            string result = sb.ToString();
        }
    }
}
