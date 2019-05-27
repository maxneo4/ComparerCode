using ComparerCode.Base;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ComparerCode
{
    [TestClass]
    public class OptimizeSearch : PerformanceTestClass
    {
        class MetadataObject
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public string Type { get; set; }
            public string Content { get; set; }
        }

        private static List<MetadataObject> objects;
        private static double objectsCount = 1e4;
        private static int cycleWhereLinQ = 100;
        private static List<string> types;

        static OptimizeSearch()
        {
           types = new List<string>() { "Rule", "Form", "UserProperty", "Task", "Context", "Ohers" };
            objects = GetObjects(()=> { return new MetadataObject()
            {
                Id = Guid.NewGuid(),
                Content = GetRandomString(100),
                Name = GetRandomString(20),
                Type  = GetRandomSample(types)
            }; }, objectsCount);
            AddToLog(new { objectsCount, cycleWhereLinQ });
        }
       
        [TestMethod]
        public void Test_GetDifferentTypesLinQ()
        {
            List<MetadataObject> forms = objects.Where(o => o.Type == "Form").ToList();
            List<MetadataObject> rules = objects.Where(o => o.Type == "Rule").ToList();
            List<MetadataObject> userPs = objects.Where(o => o.Type == "UserProperty").ToList();
            List<MetadataObject> tasks = objects.Where(o => o.Type == "Task").ToList();
            List<MetadataObject> contexts = objects.Where(o => o.Type == "Context").ToList();
        }

        [TestMethod]
        public void Test_GetDifferentTypes_For()
        {
            List<MetadataObject> forms = new List<MetadataObject>();
            List<MetadataObject> rules = new List<MetadataObject>();
            List<MetadataObject> userPs = new List<MetadataObject>();
            List<MetadataObject> tasks = new List<MetadataObject>();
            List<MetadataObject> contexts = new List<MetadataObject>();

            foreach (var obj in objects)
            {
                if (obj.Type == "Form")
                    forms.Add(obj);
                if (obj.Type == "Rule")
                    rules.Add(obj);
                if (obj.Type == "UserProperty")
                    userPs.Add(obj);
                if (obj.Type == "Task")
                    tasks.Add(obj);
                if (obj.Type == "Context")
                    contexts.Add(obj);
            }
        }

        [TestMethod]
        public void Test_GetDifferentTypesLinQ_Magic()
        {
            IEnumerable<MetadataObject> forms = objects.Where(o => o.Type == "Form");
            IEnumerable<MetadataObject> rules = objects.Where(o => o.Type == "Rule");
            IEnumerable<MetadataObject> userPs = objects.Where(o => o.Type == "UserProperty");
            IEnumerable<MetadataObject> tasks = objects.Where(o => o.Type == "Task");
            IEnumerable<MetadataObject> contexts = objects.Where(o => o.Type == "Context");
        }


        [TestMethod]
        public void Test_GetDifferentTypesAndContentLinQ()
        {
            IEnumerable<MetadataObject> forms = objects.Where(o => o.Type == "Form");
            IEnumerable<MetadataObject> rules = objects.Where(o => o.Type == "Rule");
            IEnumerable<MetadataObject> userPs = objects.Where(o => o.Type == "UserProperty");
            IEnumerable<MetadataObject> tasks = objects.Where(o => o.Type == "Task");
            IEnumerable<MetadataObject> contexts = objects.Where(o => o.Type == "Context");

            for (int i = 0; i < cycleWhereLinQ; i++)
            {
                List<MetadataObject> formsWS = forms.Where(f => f.Content.Contains("01")).ToList();
                List<MetadataObject> rulesWS = rules.Where(f => f.Content.Contains("01")).ToList();
                List<MetadataObject> userPsWS = userPs.Where(f => f.Content.Contains("01")).ToList();
                List<MetadataObject> tasksWS = tasks.Where(f => f.Content.Contains("01")).ToList();
                List<MetadataObject> contextsWS = contexts.Where(f => f.Content.Contains("01")).ToList();
            }            
        }

        [TestMethod]
        public void Test_GetDifferentTypesAndContentLinQ_ToList()
        {
            List<MetadataObject> forms = objects.Where(o => o.Type == "Form").ToList();
            List<MetadataObject> rules = objects.Where(o => o.Type == "Rule").ToList();
            List<MetadataObject> userPs = objects.Where(o => o.Type == "UserProperty").ToList();
            List<MetadataObject> tasks = objects.Where(o => o.Type == "Task").ToList();
            List<MetadataObject> contexts = objects.Where(o => o.Type == "Context").ToList();

            for (int i = 0; i < cycleWhereLinQ; i++)
            {
                List<MetadataObject> formsWS = forms.Where(f => f.Content.Contains("01")).ToList();
                List<MetadataObject> rulesWS = rules.Where(f => f.Content.Contains("01")).ToList();
                List<MetadataObject> userPsWS = userPs.Where(f => f.Content.Contains("01")).ToList();
                List<MetadataObject> tasksWS = tasks.Where(f => f.Content.Contains("01")).ToList();
                List<MetadataObject> contextsWS = contexts.Where(f => f.Content.Contains("01")).ToList();
            }
        }

        //***********Linq ienumarable without search, llinq where.where

        // CapitalizeFirstLetter "hello" => "Hello"
    }
}
