using ComparerCode.Base;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace ComparerCode
{
    [TestClass]
    public class StringContains : PerformanceTestClass
    {
        //good value 3E6
        static double string_list_size = 3E6;        
        static List<string> listTexts;
        static string text = "Este texto esta aqui";
        static int string_random_size = 2;//text.Length;
        static Regex regex = new Regex(@"\d");

        static StringContains()
        {
            listTexts = GetStringList(string_list_size, string_random_size);
            AddToLog(new { string_list_size, string_random_size, text });
        }

        [TestMethod]
        public void Test01_TextContains()
        {
            for (int i = 0; i < string_list_size; i++)
            {
                bool r = text.Contains(listTexts[i]);
            }
        }

        [TestMethod]
        public void Test03_TextIndexOf()
        {
            for (int i = 0; i < string_list_size; i++)
            {
                bool r = text.IndexOf(listTexts[i]) > 0;
            }
        }

        [TestMethod]
        public void Test02_TextStartsWith()
        {
            for (int i = 0; i < string_list_size; i++)
            {
                bool r = text.StartsWith(listTexts[i]);
            }
        }

        [TestMethod]
        public void Test03_TextIndexOf_IfSize()
        {
            for (int i = 0; i < string_list_size; i++)
            {
                bool r = text.Length> listTexts[i].Length? text.StartsWith(listTexts[i]) : false;
            }
        }

        #region regex
        [TestMethod]
        public void TestRegex02_TextContainsNumber()
        {
            for (int i = 0; i < listTexts.Count; i++)
            {
                Match m = regex.Match(listTexts[i]);
                bool r = m.Success;
            }
        }

        [TestMethod]
        public void TestRegex01_TextContains_abc123()
        {
            for (int i = 0; i < listTexts.Count; i++)
            {
                Match m = regex.Match("abc123");
                bool r = m.Success;
            }
        }
        #endregion

    }
}
