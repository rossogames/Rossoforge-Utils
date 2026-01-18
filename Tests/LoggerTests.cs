using NUnit.Framework;
using Rossoforge.Utils.Logger;
using System;
using System.Collections.Generic;
using ILogger = Rossoforge.Utils.Logger.ILogger;

namespace Rossoforge.Utils.Tests
{
    public class LoggerTests
    {
        private FakeLogger fake;

        [SetUp]
        public void Setup()
        {
            fake = new FakeLogger();

            RossoLogger.Current = fake;
            RossoLogger.Level = LogLevel.Verbose;
        }

        [Test]
        public void Log_ShouldBeForwardedToCurrentLogger()
        {
            RossoLogger.Info("test");

            Assert.AreEqual(1, fake.InfoMessages.Count);
            Assert.AreEqual("test", fake.InfoMessages[0]);
        }

        [Test]
        public void Warning_ShouldBeForwarded()
        {
            RossoLogger.Warning("warn");

            Assert.AreEqual(1, fake.WarningMessages.Count);
            Assert.AreEqual("warn", fake.WarningMessages[0]);
        }

        [Test]
        public void Error_ShouldBeForwarded()
        {
            RossoLogger.Error("error");

            Assert.AreEqual(1, fake.ErrorMessages.Count);
            Assert.AreEqual("error", fake.ErrorMessages[0]);
        }

        [Test]
        public void Exception_ShouldBeForwarded()
        {
            var ex = new Exception("boom");

            RossoLogger.Exception(ex);

            Assert.AreEqual(1, fake.Exceptions.Count);
            Assert.AreSame(ex, fake.Exceptions[0]);
        }

        [Test]
        public void Verbose_ShouldRespectLevel()
        {
            RossoLogger.Level = LogLevel.Info;

            RossoLogger.Verbose("hidden");

            Assert.AreEqual(0, fake.VerboseMessages.Count);
        }

        [Test]
        public void Log_ShouldRespectInfoLevel()
        {
            RossoLogger.Level = LogLevel.Warning;

            RossoLogger.Info("info message");

            Assert.AreEqual(0, fake.InfoMessages.Count);
        }

        [Test]
        public void Warning_ShouldAppearWhenLevelIsWarning()
        {
            RossoLogger.Level = LogLevel.Warning;

            RossoLogger.Warning("warn");

            Assert.AreEqual(1, fake.WarningMessages.Count);
        }

        [Test]
        public void Error_ShouldAlwaysAppearUnlessNone()
        {
            RossoLogger.Level = LogLevel.Error;

            RossoLogger.Error("error");

            Assert.AreEqual(1, fake.ErrorMessages.Count);
        }

        [Test]
        public void NoneLevel_ShouldBlockAllExceptExplicitExceptionRule()
        {
            RossoLogger.Level = LogLevel.None;

            RossoLogger.Info("a");
            RossoLogger.Warning("b");
            RossoLogger.Error("c");
            RossoLogger.Verbose("d");

            Assert.AreEqual(0, fake.InfoMessages.Count);
            Assert.AreEqual(0, fake.WarningMessages.Count);
            Assert.AreEqual(0, fake.ErrorMessages.Count);
            Assert.AreEqual(0, fake.VerboseMessages.Count);
        }
    }

    public class FakeLogger : ILogger
    {
        public LogLevel Level { get; set; } = LogLevel.Verbose;

        public List<string> VerboseMessages = new();
        public List<string> InfoMessages = new();
        public List<string> WarningMessages = new();
        public List<string> ErrorMessages = new();
        public List<Exception> Exceptions = new();

        public void Verbose(string message) => VerboseMessages.Add(message);
        public void Info(string message) => InfoMessages.Add(message);
        public void Warning(string message) => WarningMessages.Add(message);
        public void Error(string message) => ErrorMessages.Add(message);
        public void Exception(Exception ex) => Exceptions.Add(ex);
    }
}
