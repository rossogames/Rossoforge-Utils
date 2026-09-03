using System;
using UnityEngine;

namespace Rossoforge.Utils.Logger
{
    public interface ILogger
    {
        LogLevel Level { get; set; }

        void Verbose(string message);
        void Info(string message);
        void Warning(string message);
        void Error(string message);
        void Exception(Exception ex);
        void OnLog(string condition, string stackTrace, LogType type);
    }
}
