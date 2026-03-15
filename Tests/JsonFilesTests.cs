using NUnit.Framework;
using Rossoforge.Utils.IO;
using System.IO;
using UnityEngine;


namespace Rossoforge.Utils.Tests
{
    public class JsonFilesTests
    {
        private string tempFile;

        private class TestData
        {
            public string Name;
            public int Age;
        }

        [SetUp]
        public void Setup()
        {
            tempFile = Path.GetTempFileName();
        }

        [TearDown]
        public void TearDown()
        {
            if (File.Exists(tempFile))
                File.Delete(tempFile);
        }

        [Test]
        public void TryLoad_ValidJson_ReturnsTrue()
        {
            var data = new TestData { Name = "John", Age = 30 };
            File.WriteAllText(tempFile, JsonUtility.ToJson(data));

            bool success = JsonFiles.TryLoad(tempFile, out TestData loadedData);
            Assert.IsTrue(success);
            Assert.AreEqual("John", loadedData.Name);
            Assert.AreEqual(30, loadedData.Age);
        }

        [Test]
        public void TryLoad_InvalidJson_ReturnsFalse()
        {
            File.WriteAllText(tempFile, "Invalid Json");

            bool success = JsonFiles.TryLoad<TestData>(tempFile, out var loadedData);
            Assert.IsFalse(success);
            Assert.IsNull(loadedData);
        }

        [Test]
        public void Save_WritesJsonFile()
        {
            var data = new TestData { Name = "Alice", Age = 25 };
            JsonFiles.Save(tempFile, data);

            string json = File.ReadAllText(tempFile);
            Assert.IsTrue(json.Contains("Alice"));
            Assert.IsTrue(json.Contains("25"));
        }
    }
}