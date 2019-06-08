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
        //good value 100e3
        private static double list_objects_size = 100e3;
        //good value 200
        private static int cycle_linq = 200;
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
            }; }, list_objects_size);
            AddToLog(new { list_objects_size, cycle_linq });
        }

        [TestMethod]
        public void Test01_GetTypesAndContentLinQ()
        {
            IEnumerable<MetadataObject> forms = objects.Where(o => o.Type == "Form");
            IEnumerable<MetadataObject> rules = objects.Where(o => o.Type == "Rule");
            IEnumerable<MetadataObject> userPs = objects.Where(o => o.Type == "UserProperty");
            IEnumerable<MetadataObject> tasks = objects.Where(o => o.Type == "Task");
            IEnumerable<MetadataObject> contexts = objects.Where(o => o.Type == "Context");

            for (int i = 0; i < cycle_linq; i++)
            {
                List<MetadataObject> formsWS = forms.Where(f => f.Content.Contains("01")).ToList();
                List<MetadataObject> rulesWS = rules.Where(f => f.Content.Contains("01")).ToList();
                List<MetadataObject> userPsWS = userPs.Where(f => f.Content.Contains("01")).ToList();
                List<MetadataObject> tasksWS = tasks.Where(f => f.Content.Contains("01")).ToList();
                List<MetadataObject> contextsWS = contexts.Where(f => f.Content.Contains("01")).ToList();
            }
        }

        [TestMethod]
        public void Test02_GetTypesAndContentLinQ_ToList()
        {
            List<MetadataObject> forms = objects.Where(o => o.Type == "Form").ToList();
            List<MetadataObject> rules = objects.Where(o => o.Type == "Rule").ToList();
            List<MetadataObject> userPs = objects.Where(o => o.Type == "UserProperty").ToList();
            List<MetadataObject> tasks = objects.Where(o => o.Type == "Task").ToList();
            List<MetadataObject> contexts = objects.Where(o => o.Type == "Context").ToList();

            for (int i = 0; i < cycle_linq; i++)
            {
                List<MetadataObject> formsWS = forms.Where(f => f.Content.Contains("01")).ToList();
                List<MetadataObject> rulesWS = rules.Where(f => f.Content.Contains("01")).ToList();
                List<MetadataObject> userPsWS = userPs.Where(f => f.Content.Contains("01")).ToList();
                List<MetadataObject> tasksWS = tasks.Where(f => f.Content.Contains("01")).ToList();
                List<MetadataObject> contextsWS = contexts.Where(f => f.Content.Contains("01")).ToList();
            }
        }

        [TestMethod]
        public void Test03_GetTypesAndContentLinQ_ToList_optimizeCycle()
        {
            List<MetadataObject> forms = objects.Where(o => o.Type == "Form").ToList();
            List<MetadataObject> rules = objects.Where(o => o.Type == "Rule").ToList();
            List<MetadataObject> userPs = objects.Where(o => o.Type == "UserProperty").ToList();
            List<MetadataObject> tasks = objects.Where(o => o.Type == "Task").ToList();
            List<MetadataObject> contexts = objects.Where(o => o.Type == "Context").ToList();

            for (int i = 0; i < cycle_linq; i++)
            {
                List<MetadataObject> objectsWith01 = objects.Where(o => o.Content.Contains("01")).ToList();
                List<MetadataObject> formsWs = objectsWith01.Where(o => o.Type == "Form").ToList();
                List<MetadataObject> rulesWs = objectsWith01.Where(o => o.Type == "Rule").ToList();
                List<MetadataObject> userPsWs = objectsWith01.Where(o => o.Type == "UserProperty").ToList();
                List<MetadataObject> tasksWs = objectsWith01.Where(o => o.Type == "Task").ToList();
                List<MetadataObject> contextsWs = objectsWith01.Where(o => o.Type == "Context").ToList();
            }
        }

        [TestMethod]
        public void Test03_GetTypesAndContentLinQ_ToList_optimizeCycle_OUT()
        {
            List<MetadataObject> forms = objects.Where(o => o.Type == "Form").ToList();
            List<MetadataObject> rules = objects.Where(o => o.Type == "Rule").ToList();
            List<MetadataObject> userPs = objects.Where(o => o.Type == "UserProperty").ToList();
            List<MetadataObject> tasks = objects.Where(o => o.Type == "Task").ToList();
            List<MetadataObject> contexts = objects.Where(o => o.Type == "Context").ToList();

            List<MetadataObject> objectsWith01 = objects.Where(o => o.Content.Contains("01")).ToList();
            for (int i = 0; i < cycle_linq; i++)
            {
                List<MetadataObject> formsWs = objectsWith01.Where(o => o.Type == "Form").ToList();
                List<MetadataObject> rulesWs = objectsWith01.Where(o => o.Type == "Rule").ToList();
                List<MetadataObject> userPsWs = objectsWith01.Where(o => o.Type == "UserProperty").ToList();
                List<MetadataObject> tasksWs = objectsWith01.Where(o => o.Type == "Task").ToList();
                List<MetadataObject> contextsWs = objectsWith01.Where(o => o.Type == "Context").ToList();
            }
        }

        [TestMethod]
        public void Test04_GetTypesLinQ()
        {
            List<MetadataObject> forms = objects.Where(o => o.Type == "Form").ToList();
            List<MetadataObject> rules = objects.Where(o => o.Type == "Rule").ToList();
            List<MetadataObject> userPs = objects.Where(o => o.Type == "UserProperty").ToList();
            List<MetadataObject> tasks = objects.Where(o => o.Type == "Task").ToList();
            List<MetadataObject> contexts = objects.Where(o => o.Type == "Context").ToList();
        }

        [TestMethod]
        public void Test05_GetTypes_For()
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
        public void Test06_GetTypesLinQ_Magic()
        {
            IEnumerable<MetadataObject> forms = objects.Where(o => o.Type == "Form");
            IEnumerable<MetadataObject> rules = objects.Where(o => o.Type == "Rule");
            IEnumerable<MetadataObject> userPs = objects.Where(o => o.Type == "UserProperty");
            IEnumerable<MetadataObject> tasks = objects.Where(o => o.Type == "Task");
            IEnumerable<MetadataObject> contexts = objects.Where(o => o.Type == "Context");
        }


        //***********Linq ienumarable without search, llinq where.where

        // CapitalizeFirstLetter "hello" => "Hello"
    }
}
