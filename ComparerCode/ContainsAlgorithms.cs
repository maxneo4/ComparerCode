using System;
using System.Collections.Generic;
using System.Linq;
using ComparerCode.Base;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ComparerCode
{
    [TestClass]
    public class ContainsAlgorithms : PerformanceTestClass
    {
        //good value 100e3
        static double ids_list_fullSize = 1e6;
        static int ids_list_sample_size = 5000;
        static int ids_list_shortSize = 5000;        

        static List<Guid> idsFull, idsPartial;     

        static ContainsAlgorithms()
        {
            idsFull = GetIdsList(ids_list_fullSize);
            idsPartial = GetRandomSample(idsFull, ids_list_sample_size);
            idsPartial.AddRange(GetIdsList(ids_list_shortSize));
            int ids_list_partial_size = ids_list_shortSize + ids_list_sample_size;
           
            AddToLog(new { ids_list_fullSize, ids_list_sample_size, ids_list_shortSize,  ids_list_partial_size});
        }

        #region add if contains

        [TestMethod]
        public void Test01_ListContains()
        {
            int contains = idsFull.Where(id => idsPartial.Contains(id)).Count();
        }

        [TestMethod]
        public void Test02_HashSetContains()
        {
            HashSet<Guid> hidsPartial = new HashSet<Guid>();
            foreach (var idPartial in idsPartial)
            {
                hidsPartial.Add(idPartial);
            }
            int contains = idsFull.Where(id => hidsPartial.Contains(id)).Count();
        }

        [TestMethod]
        public void Test03_HashSetAndForContains()
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
    }
}