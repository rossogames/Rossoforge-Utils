using UnityEngine;

namespace Rossoforge.Utils.Logger
{
    [CreateAssetMenu(fileName = "RossoLoggerSettings", menuName = "Rossoforge/Data Config/Utils/Logger")]
    public class LoggerDataConfig : ScriptableObject
    {
        public LogLevel LogLevel = LogLevel.Warning;
    }
}
