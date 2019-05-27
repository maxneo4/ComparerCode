using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using ComparerCode.Base;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ComparerCode
{
    [TestClass]
    public class ContainsAlgorithms : PerformanceTestClass
    {        
        static double repetitiveCompare = 1E6;
        static double idsLength = 1e5;
        static int idsSampleLenght = 10000;
        static int idsPartialRandom = 5000;
        static int stringRandomLength = 50;

        static List<Guid> idsFull, idsPartial;
        static List<string> listTexts;
        static Regex regex = new Regex(@"\d");
          

    static ContainsAlgorithms()
        {            
            idsFull = GetIdsList(idsLength);
            idsPartial = GetRandomSample(idsFull, idsSampleLenght);
            idsPartial.AddRange(GetIdsList(idsPartialRandom));
            listTexts = GetStringList(repetitiveCompare, stringRandomLength);
            AddToLog(new { repetitiveCompare, idsLength, idsSampleLenght, idsPartialRandom, stringRandomLength });
        }

        #region add if contains

        [TestMethod]
        public void TestListContains()
        {
            int contains = idsFull.Where(id => idsPartial.Contains(id)).Count();            
        }

        [TestMethod]
        public void TestHashSetContains()
        {
            HashSet<Guid> hidsPartial = new HashSet<Guid>();
            foreach (var idPartial in idsPartial)
            {
                hidsPartial.Add(idPartial);
            }
            int contains = idsFull.Where(id => hidsPartial.Contains(id)).Count();
        }

        [TestMethod]
        public void TestHashSetAndForContains()
        {
            HashSet<Guid> hidsPartial = new HashSet<Guid>();
            foreach (var idPartial in idsPartial)
            {
                hidsPartial.Add(idPartial);
            }
            int contains = 0;
            foreach (var idFull in idsFull)
            {
                if (hidsPartial.Contains(idFull))
                    contains++;
            }
        }
        #endregion

        #region text contains

        string text = "Este texto esta aqui";

        [TestMethod]
        public void TestTextContains()
        {
            for (int i = 0; i < repetitiveCompare; i++)
            {
               bool r = text.Contains(listTexts[i]);
            }
        }

        [TestMethod]
        public void TestTextIndexOf()
        {
            for (int i = 0; i < repetitiveCompare; i++)
            {
               bool r = text.IndexOf(listTexts[i]) >0;
            }
        }

        [TestMethod]
        public void TestTextStartsWith()
        {
            for (int i = 0; i < repetitiveCompare; i++)
            {
                bool r = text.StartsWith(listTexts[i]);
            }
        }
      
        #endregion

        #region regex
        [TestMethod]
        public void TestTextContainsNumber_Regex()
        {
            for (int i = 0; i < repetitiveCompare; i++)
            {
                Match m = regex.Match(listTexts[i]);
                bool r = m.Success;
            }
        }

        [TestMethod]
        public void TestTextContainsNumberSingle_Regex()
        {
            for (int i = 0; i < repetitiveCompare; i++)
            {
                Match m = regex.Match("abc123");
                bool r = m.Success;
            }
        }
        #endregion
    }
}
