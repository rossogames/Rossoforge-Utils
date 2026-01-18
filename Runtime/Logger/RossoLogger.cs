using System;

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

        public static void Log(string message) => Current.Info(message);
        public static void Warning(string message) => Current.Warning(message);
        public static void Error(string message) => Current.Error(message);
        public static void Exception(Exception ex) => Current.Exception(ex);
    }
}
