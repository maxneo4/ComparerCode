using ComparerCode.Base;
using ComparerCode.Extention;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ComparerCode
{
    [TestClass]
    public class OptimizeSearchIndexation : PerformanceTestClass
    {
        class MetadataObject
        {
            public Guid Id { get; set; }
            public Guid Parent { get; set; }
            public string Name { get; set; }
            public string Type { get; set; }
            public string Content { get; set; }
        }

        private static List<MetadataObject> objects;
        //good value 15e3, 5e3 badHS
        private static double list_objects_size = 5e3;     
        private static List<string> types;
        private static List<Guid> set_ids;

        static OptimizeSearchIndexation()
        {
           types = new List<string>() { "Rule", "Form", "UserProperty", "Task", "Context", "Ohers" };
            set_ids = GetIdsList(list_objects_size);
            objects = GetObjects(()=> { return new MetadataObject()
            {
                Id = GetRandomSample(set_ids),
                Content = GetRandomString(100),
                Name = GetRandomString(20),
                Type = GetRandomSample(types),
                Parent = GetRandomSample(set_ids)
            }; }, list_objects_size);
            AddToLog(new { list_objects_size });
        }

        [TestMethod]
        public void Test01_setIdsAndParent_Not()
        {
            //get set_ids not in objects
            //get objects with parents not present in objects
            List<Guid> set_ids_not = new List<Guid>();
            foreach (var id in set_ids)
            {
                if (!objects.Select(o => o.Id).Contains(id))
                    set_ids_not.Add(id);
            }

            List<MetadataObject> object_parent_not = new List<MetadataObject>();
            foreach (var obj in objects)
            {
                if (!set_ids_not.Contains(obj.Parent))
                    object_parent_not.Add(obj);
            }
            LogResult(set_ids_not, object_parent_not);
        }

        [TestMethod]
        public void Test01_setIdsAndParent_Not_HS()
        {
            //get set_ids not in objects
            //get objects with parents not present in objects
            List<Guid> set_ids_not = new List<Guid>();
            var hsObjects = objects.ToHashSet(o => o.Id);
            foreach (var id in set_ids)
            {
                if (!hsObjects.Contains(id))
                    set_ids_not.Add(id);
            }

            List<MetadataObject> object_parent_not = new List<MetadataObject>();
            var hsSet_ids_not = set_ids_not.ToHashSet();
            foreach (var obj in objects)
            {
                if (!hsSet_ids_not.Contains(obj.Parent))
                    object_parent_not.Add(obj);
            }
            LogResult(set_ids_not, object_parent_not);
        }

        [TestMethod]
        public void Test02_setIdsAndParent_Not()
        {
            //get set_ids not in objects
            //get objects with parents not present in objects
            List<Guid> set_ids_not = set_ids.Where(id => !objects.Select(o => o.Id).Contains(id)).ToList();
            List<MetadataObject> object_parent_not = objects.Where(obj => !set_ids_not.Contains(obj.Parent)).ToList();           
            LogResult(set_ids_not, object_parent_not);
        }

        [Ignore]//55 segundos 15e3
        [TestMethod]
        public void Test02_setIdsAndParent_Not_BadHS()
        {
            //get set_ids not in objects
            //get objects with parents not present in objects
            List<Guid> set_ids_not = set_ids.Where(id => !objects.ToHashSet(o => o.Id).Contains(id)).ToList();
            List<MetadataObject> object_parent_not = objects.Where(obj => !set_ids_not.ToHashSet().Contains(obj.Parent)).ToList();
            LogResult(set_ids_not, object_parent_not);
        }
                
        [TestMethod]
        public void Test02_setIdsAndParent_Not_GoodHS()
        {
            //get set_ids not in objects
            //get objects with parents not present in objects
            var hsObjects = objects.ToHashSet(o => o.Id);            
            List<Guid> set_ids_not = set_ids.Where(id => !hsObjects.Contains(id)).ToList();

            var hsSet_ids_not = set_ids_not.ToHashSet();
            List<MetadataObject> object_parent_not = objects.Where(obj => !hsSet_ids_not.Contains(obj.Parent)).ToList();

            LogResult(set_ids_not, object_parent_not);
        }

        [TestMethod]
        public void Test02_setIdsAndParent_Not_GoodHS_Extention()
        {
            //get set_ids not in objects
            //get objects with parents not present in objects           
            List<Guid> set_ids_not = set_ids.WhereNotIn(objects, o => o.Id);
            List<MetadataObject> object_parent_not = objects.WherePropertyNotIn(o => o.Parent, set_ids_not);

            LogResult(set_ids_not, object_parent_not);
        }

        //add extention notContains with indexation to begin
        private void LogResult(List<Guid> set_ids_not, List<MetadataObject> object_parent_not)
        {
            var not_ids = set_ids_not.Count;
            var not_parents = object_parent_not.Count;
            TestContext.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(new { not_ids, not_parents }));
        }
    }
}