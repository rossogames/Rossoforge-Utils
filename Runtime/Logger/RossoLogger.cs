using System;
using UnityEngine;

namespace Rossoforge.Utils.Logger
{
    public static class RossoLogger
    {
        public static ILogger Current { get; set; } = new UnityLogger();

        public static LogLevel Level
        {
            get => Current.Level;
            set => Current.Level = value;
        }

        static RossoLogger()
        {
            LoadSettings();
        }

        private static void LoadSettings()
        {
            var settings = Resources.Load<LoggerSettings>("RossoLoggerSettings");
            if (settings != null)
            {
                Current.Level = settings.LogLevel;
            }
        }

        public static void Verbose(string message)
        {
            if (Level >= LogLevel.Verbose)
                Current.Verbose(message);
        }

        public static void Info(string message)
        {
            if (Level >= LogLevel.Info)
                Current.Info(message);
        }

        public static void Warning(string message)
        {
            if (Level >= LogLevel.Warning)
                Current.Warning(message);
        }

        public static void Error(string message)
        {
            if (Level >= LogLevel.Error)
                Current.Error(message);
        }

        public static void Exception(Exception ex)
        {
            if (Level > LogLevel.None)
                Current.Exception(ex);
        }
    }
}
