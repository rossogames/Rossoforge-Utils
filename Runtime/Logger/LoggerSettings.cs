using UnityEngine;

namespace Rossoforge.Utils.Logger
{
    [CreateAssetMenu(fileName = "RossoLoggerSettings", menuName = "Rossoforge/Utils/Logger Settings")]
    public class LoggerSettings : ScriptableObject
    {
        public LogLevel LogLevel = LogLevel.Warning;
    }
}
