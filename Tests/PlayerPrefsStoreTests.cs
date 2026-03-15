using NUnit.Framework;
using UnityEngine;
using Rossoforge.Utils.IO;

namespace Rossoforge.Utils.Tests
{
    public class PlayerPrefsStoreTests
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
            PlayerPrefsStore.SaveInt(IntKey, 42);

            int value = PlayerPrefsStore.LoadInt(IntKey);

            Assert.AreEqual(42, value);
        }

        [Test]
        public void LoadInt_ReturnsDefault_WhenKeyDoesNotExist()
        {
            int value = PlayerPrefsStore.LoadInt(IntKey, 99);

            Assert.AreEqual(99, value);
        }

        // --------- FLOAT ---------

        [Test]
        public void SaveFloat_LoadFloat_ReturnsSavedValue()
        {
            PlayerPrefsStore.SaveFloat(FloatKey, 3.14f);

            float value = PlayerPrefsStore.LoadFloat(FloatKey);

            Assert.AreEqual(3.14f, value);
        }

        [Test]
        public void LoadFloat_ReturnsDefault_WhenKeyDoesNotExist()
        {
            float value = PlayerPrefsStore.LoadFloat(FloatKey, 1.5f);

            Assert.AreEqual(1.5f, value);
        }

        // --------- STRING ---------

        [Test]
        public void SaveString_LoadString_ReturnsSavedValue()
        {
            PlayerPrefsStore.SaveString(StringKey, "hello");

            string value = PlayerPrefsStore.LoadString(StringKey);

            Assert.AreEqual("hello", value);
        }

        [Test]
        public void LoadString_ReturnsDefault_WhenKeyDoesNotExist()
        {
            string value = PlayerPrefsStore.LoadString(StringKey, "default");

            Assert.AreEqual("default", value);
        }

        // --------- BOOL ---------

        [Test]
        public void SaveBool_LoadBool_ReturnsTrue()
        {
            PlayerPrefsStore.SaveBool(BoolKey, true);

            bool value = PlayerPrefsStore.LoadBool(BoolKey);

            Assert.IsTrue(value);
        }

        [Test]
        public void SaveBool_LoadBool_ReturnsFalse()
        {
            PlayerPrefsStore.SaveBool(BoolKey, false);

            bool value = PlayerPrefsStore.LoadBool(BoolKey);

            Assert.IsFalse(value);
        }

        [Test]
        public void LoadBool_ReturnsDefault_WhenKeyDoesNotExist()
        {
            bool value = PlayerPrefsStore.LoadBool(BoolKey, true);

            Assert.IsTrue(value);
        }

        // --------- ENUM ---------

        [Test]
        public void SaveEnum_LoadEnum_ReturnsSavedValue()
        {
            PlayerPrefsStore.SaveEnum(EnumKey, TestEnum.C);

            TestEnum value = PlayerPrefsStore.LoadEnum(EnumKey, TestEnum.A);

            Assert.AreEqual(TestEnum.C, value);
        }

        [Test]
        public void LoadEnum_ReturnsDefault_WhenKeyDoesNotExist()
        {
            TestEnum value = PlayerPrefsStore.LoadEnum(EnumKey, TestEnum.B);

            Assert.AreEqual(TestEnum.B, value);
        }

        // --------- MANAGEMENT ---------

        [Test]
        public void HasKey_ReturnsTrue_WhenKeyExists()
        {
            PlayerPrefsStore.SaveInt(IntKey, 10);

            bool exists = PlayerPrefsStore.HasKey(IntKey);

            Assert.IsTrue(exists);
        }

        [Test]
        public void HasKey_ReturnsFalse_WhenKeyDoesNotExist()
        {
            bool exists = PlayerPrefsStore.HasKey(IntKey);

            Assert.IsFalse(exists);
        }

        [Test]
        public void DeleteKey_RemovesKey()
        {
            PlayerPrefsStore.SaveInt(IntKey, 5);

            PlayerPrefsStore.DeleteKey(IntKey);

            bool exists = PlayerPrefsStore.HasKey(IntKey);

            Assert.IsFalse(exists);
        }

        [Test]
        public void DeleteAll_RemovesAllKeys()
        {
            PlayerPrefsStore.SaveInt(IntKey, 1);
            PlayerPrefsStore.SaveFloat(FloatKey, 2f);

            PlayerPrefsStore.DeleteAll();

            Assert.IsFalse(PlayerPrefsStore.HasKey(IntKey));
            Assert.IsFalse(PlayerPrefsStore.HasKey(FloatKey));
        }

        [Test]
        public void Save_CanBeCalledWithoutError()
        {
            PlayerPrefsStore.SaveInt("dummy", 123);

            Assert.DoesNotThrow(() => PlayerPrefsStore.Save());
        }
    }
}
