using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace ComparerCode.Base
{
    [TestClass]
    public abstract class PerformanceTestClass
    {
        private static Random random = new Random();
        private static Stopwatch sw = new Stopwatch();
        private static string logPath;
        private static Dictionary<string, string> times;

        protected static void AddToLog(string text)
        {
            File.AppendAllText(logPath, text + "\r\n");
        }

        protected static void AddToLog(object data)
        {
            File.AppendAllText(logPath, Newtonsoft.Json.JsonConvert.SerializeObject(data, Newtonsoft.Json.Formatting.Indented, new NumberConverter()) +"\r\n");            
        }

        #region Generators
        public static List<Guid> GetIdsList(double length)
        {
            List<Guid> result = new List<Guid>();
            for (int i = 0; i < length; i++)
            {
                result.Add(Guid.NewGuid());
            }
            return result;
        }

        public static List<string> GetStringList(double length, int lengthStrings)
        {
            List<string> result = new List<string>();
            for (int i = 0; i < length; i++)
            {
                result.Add(GetRandomString(lengthStrings));
            }
            return result;
        }

        public static string GetRandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static List<T> GetRandomSample<T>(List<T> elements, int count)
        {
            List<T> result = new List<T>(count);
            for (int i = 0; i < count; i++)
            {
                result.Add(elements[random.Next(0, count - 1)]);
            }
            return result;
        }

        public static T GetRandomSample<T>(List<T> elements)
        {            
            return elements[random.Next(0, elements.Count - 1)];
        }

        public static List<T> GetObjects<T>(Func<T> builder, double length)
        {
            List<T> result = new List<T>();
            for (int i = 0; i < length; i++)
            {
                result.Add(builder.Invoke());
            }
            return result;
        }
        #endregion

        #region test configurations
        public TestContext TestContext
        {
            get;
            set;
        }

        [TestInitialize]
        public void Setup()
        {
            sw.Start();
        }

        [TestCleanup]
        public void End()
        {
            sw.Stop();
            times.Add($"{Format(TestContext.TestName)}", $"{(times.Count + 1).ToString("00")}=> [{sw.ElapsedMilliseconds.ToString("#,##0").Replace(".", " ")} ms]");
            sw.Reset();
        }

        private string Format(string testName)
        {
            StringBuilder builder = new StringBuilder(testName);
            while (builder.Length < 60)
                builder.Append(" ");
            return builder.ToString();
        }

        [AssemblyInitialize]
        public static void AssemblyInitialize(TestContext testContext)
        {
            times = new Dictionary<string, string>();
            logPath = $"{testContext.FullyQualifiedTestClassName}.txt";
        }

        [AssemblyCleanup()]
        public static void AssemblyCleanup()
        {
            AddToLog(times);
            AddToLog("-----****-----\r\n");
            Process.Start(logPath);
        }
        #endregion
    }
}