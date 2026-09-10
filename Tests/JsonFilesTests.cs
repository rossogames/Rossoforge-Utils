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

        private class UnityTypesData
        {
            public Vector2 Position2D;
            public Vector3 Position3D;
            public Vector4 ShaderVector;
            public Vector2Int GridPos2D;
            public Vector3Int GridPos3D;
            public Quaternion Rotation;
            public Color TintColor;
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
            File.WriteAllText(tempFile, JsonFiles.Serialize(data));

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

        [Test]
        public void SaveAndLoad_UnityTypes_SerializesAndDeserializesCorrectly()
        {
            var originalData = new UnityTypesData
            {
                Position2D = new Vector2(1.5f, 2.5f),
                Position3D = new Vector3(10.0f, 20.5f, -30.0f),
                ShaderVector = new Vector4(1.0f, 0.5f, 0.0f, 1.0f),
                GridPos2D = new Vector2Int(5, 10),
                GridPos3D = new Vector3Int(1, -2, 3),
                Rotation = new Quaternion(0.1f, 0.2f, 0.3f, 0.4f),
                TintColor = new Color(0.2f, 0.4f, 0.6f, 0.8f)
            };

            JsonFiles.Save(tempFile, originalData);

            bool success = JsonFiles.TryLoad(tempFile, out UnityTypesData loadedData);

            Assert.IsTrue(success);
            Assert.NotNull(loadedData);

            // Verificación de tolerancia de flotantes para tipos de Unity
            Assert.AreEqual(originalData.Position2D.x, loadedData.Position2D.x, 0.0001f);
            Assert.AreEqual(originalData.Position2D.y, loadedData.Position2D.y, 0.0001f);

            Assert.AreEqual(originalData.Position3D.x, loadedData.Position3D.x, 0.0001f);
            Assert.AreEqual(originalData.Position3D.y, loadedData.Position3D.y, 0.0001f);
            Assert.AreEqual(originalData.Position3D.z, loadedData.Position3D.z, 0.0001f);

            Assert.AreEqual(originalData.ShaderVector.w, loadedData.ShaderVector.w, 0.0001f);

            Assert.AreEqual(originalData.GridPos2D, loadedData.GridPos2D);
            Assert.AreEqual(originalData.GridPos3D, loadedData.GridPos3D);

            Assert.AreEqual(originalData.Rotation.x, loadedData.Rotation.x, 0.0001f);
            Assert.AreEqual(originalData.Rotation.w, loadedData.Rotation.w, 0.0001f);

            Assert.AreEqual(originalData.TintColor.r, loadedData.TintColor.r, 0.0001f);
            Assert.AreEqual(originalData.TintColor.a, loadedData.TintColor.a, 0.0001f);
        }
    }
}