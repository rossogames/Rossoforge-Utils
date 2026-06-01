using NUnit.Framework;
using UnityEngine;
using Rossoforge.Utils.IO;

namespace Rossoforge.Utils.Tests
{
    public class PlayerPrefsStorageTests
    {
        private const string IntKey = "test_int";
        private const string FloatKey = "test_float";
        private const string StringKey = "test_string";
        private const string BoolKey = "test_bool";
        private const string EnumKey = "test_enum";

        private enum TestEnum
        {
            A,
            B,
            C
        }

        [SetUp]
        public void Setup()
        {
            PlayerPrefs.DeleteAll();
        }

        [TearDown]
        public void TearDown()
        {
            PlayerPrefs.DeleteAll();
        }

        // --------- INT ---------

        [Test]
        public void SaveInt_LoadInt_ReturnsSavedValue()
        {
            PlayerPrefsStorage.SaveInt(IntKey, 42);

            int value = PlayerPrefsStorage.LoadInt(IntKey);

            Assert.AreEqual(42, value);
        }

        [Test]
        public void LoadInt_ReturnsDefault_WhenKeyDoesNotExist()
        {
            int value = PlayerPrefsStorage.LoadInt(IntKey, 99);

            Assert.AreEqual(99, value);
        }

        // --------- FLOAT ---------

        [Test]
        public void SaveFloat_LoadFloat_ReturnsSavedValue()
        {
            PlayerPrefsStorage.SaveFloat(FloatKey, 3.14f);

            float value = PlayerPrefsStorage.LoadFloat(FloatKey);

            Assert.AreEqual(3.14f, value);
        }

        [Test]
        public void LoadFloat_ReturnsDefault_WhenKeyDoesNotExist()
        {
            float value = PlayerPrefsStorage.LoadFloat(FloatKey, 1.5f);

            Assert.AreEqual(1.5f, value);
        }

        // --------- STRING ---------

        [Test]
        public void SaveString_LoadString_ReturnsSavedValue()
        {
            PlayerPrefsStorage.SaveString(StringKey, "hello");

            string value = PlayerPrefsStorage.LoadString(StringKey);

            Assert.AreEqual("hello", value);
        }

        [Test]
        public void LoadString_ReturnsDefault_WhenKeyDoesNotExist()
        {
            string value = PlayerPrefsStorage.LoadString(StringKey, "default");

            Assert.AreEqual("default", value);
        }

        // --------- BOOL ---------

        [Test]
        public void SaveBool_LoadBool_ReturnsTrue()
        {
            PlayerPrefsStorage.SaveBool(BoolKey, true);

            bool value = PlayerPrefsStorage.LoadBool(BoolKey);

            Assert.IsTrue(value);
        }

        [Test]
        public void SaveBool_LoadBool_ReturnsFalse()
        {
            PlayerPrefsStorage.SaveBool(BoolKey, false);

            bool value = PlayerPrefsStorage.LoadBool(BoolKey);

            Assert.IsFalse(value);
        }

        [Test]
        public void LoadBool_ReturnsDefault_WhenKeyDoesNotExist()
        {
            bool value = PlayerPrefsStorage.LoadBool(BoolKey, true);

            Assert.IsTrue(value);
        }

        // --------- ENUM ---------

        [Test]
        public void SaveEnum_LoadEnum_ReturnsSavedValue()
        {
            PlayerPrefsStorage.SaveEnum(EnumKey, TestEnum.C);

            TestEnum value = PlayerPrefsStorage.LoadEnum(EnumKey, TestEnum.A);

            Assert.AreEqual(TestEnum.C, value);
        }

        [Test]
        public void LoadEnum_ReturnsDefault_WhenKeyDoesNotExist()
        {
            TestEnum value = PlayerPrefsStorage.LoadEnum(EnumKey, TestEnum.B);

            Assert.AreEqual(TestEnum.B, value);
        }

        // --------- MANAGEMENT ---------

        [Test]
        public void HasKey_ReturnsTrue_WhenKeyExists()
        {
            PlayerPrefsStorage.SaveInt(IntKey, 10);

            bool exists = PlayerPrefsStorage.HasKey(IntKey);

            Assert.IsTrue(exists);
        }

        [Test]
        public void HasKey_ReturnsFalse_WhenKeyDoesNotExist()
        {
            bool exists = PlayerPrefsStorage.HasKey(IntKey);

            Assert.IsFalse(exists);
        }

        [Test]
        public void DeleteKey_RemovesKey()
        {
            PlayerPrefsStorage.SaveInt(IntKey, 5);

            PlayerPrefsStorage.DeleteKey(IntKey);

            bool exists = PlayerPrefsStorage.HasKey(IntKey);

            Assert.IsFalse(exists);
        }

        [Test]
        public void DeleteAll_RemovesAllKeys()
        {
            PlayerPrefsStorage.SaveInt(IntKey, 1);
            PlayerPrefsStorage.SaveFloat(FloatKey, 2f);

            PlayerPrefsStorage.DeleteAll();

            Assert.IsFalse(PlayerPrefsStorage.HasKey(IntKey));
            Assert.IsFalse(PlayerPrefsStorage.HasKey(FloatKey));
        }

        [Test]
        public void Save_CanBeCalledWithoutError()
        {
            PlayerPrefsStorage.SaveInt("dummy", 123);

            Assert.DoesNotThrow(() => PlayerPrefsStorage.Save());
        }
    }
}
