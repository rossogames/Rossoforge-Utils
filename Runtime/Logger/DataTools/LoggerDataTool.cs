using UnityEngine;

namespace Rossoforge.Utils.Logger
{
    [CreateAssetMenu(fileName = nameof(LoggerDataTool), menuName = "Rossoforge/Data Tools/Utils/Logger")]
    public class LoggerDataTool : ScriptableObject
    {
        public LogLevel LogLevel = LogLevel.Warning;
    }
}
