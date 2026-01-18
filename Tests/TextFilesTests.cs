using NUnit.Framework;
using Rossoforge.Utils.IO;
using System.IO;

namespace Rossoforge.Utils.Tests
{
    public class TextFilesTests
    {
        private string tempFile;

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
        public void Load_FileExists_ReturnsContent()
        {
            File.WriteAllText(tempFile, "Hello World");
            string result = TextFiles.Load(tempFile);
            Assert.AreEqual("Hello World", result);
        }

        [Test]
        public void Load_FileNotExists_ThrowsException()
        {
            File.Delete(tempFile);
            Assert.Throws<FileNotFoundException>(() => TextFiles.Load(tempFile));
        }

        [Test]
        public void TryLoad_FileExists_ReturnsTrueAndContent()
        {
            File.WriteAllText(tempFile, "Test");
            bool success = TextFiles.TryLoad(tempFile, out string content);
            Assert.IsTrue(success);
            Assert.AreEqual("Test", content);
        }

        [Test]
        public void TryLoad_FileNotExists_ReturnsFalseAndNull()
        {
            File.Delete(tempFile);
            bool success = TextFiles.TryLoad(tempFile, out string content);
            Assert.IsFalse(success);
            Assert.IsNull(content);
        }

        [Test]
        public void Save_WritesContentToFile()
        {
            TextFiles.Save(tempFile, "SavedText");
            string content = File.ReadAllText(tempFile);
            Assert.AreEqual("SavedText", content);
        }
    }

}
