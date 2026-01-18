using System;
using UnityEngine;

namespace Rossoforge.Utils.Logger
{
    public class UnityLogger : ILogger
    {
        public LogLevel Level { get; set; } = LogLevel.Warning;

        public void Verbose(string message)
        {
            if (Level >= LogLevel.Verbose)
                Debug.Log(message);
        }

        public void Info(string message)
        {
            if (Level >= LogLevel.Info)
                Debug.Log(message);
        }

        public void Warning(string message)
        {
            if (Level >= LogLevel.Warning)
                Debug.LogWarning(message);
        }

        public void Error(string message)
        {
            if (Level >= LogLevel.Error)
                Debug.LogError(message);
        }

        public void Exception(Exception ex)
        {
            Debug.LogException(ex);
        }
    }
}
