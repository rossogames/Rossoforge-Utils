using System;
using UnityEngine;

namespace Rossoforge.Utils.Logger
{
    public class UnityLogger : ILogger
    {
        public LogLevel Level { get; set; } = LogLevel.Warning;

        public void Verbose(string message) => Debug.Log(message);
        public void Info(string message) => Debug.Log(message);
        public void Warning(string message) => Debug.LogWarning(message);
        public void Error(string message) => Debug.LogError(message);
        public void Exception(Exception ex) => Debug.LogException(ex);
    }
}
