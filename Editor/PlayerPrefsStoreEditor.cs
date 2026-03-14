using Rossoforge.Utils.Logger;
using UnityEditor;
using UnityEngine;

namespace Rossoforge.Utils.Editor
{
    public class PlayerPrefsStoreEditor
    {
        [MenuItem("Rossoforge/Save/Delete Registry")]
        public static void DeleteAllRegistry()
        {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.Save();
            RossoLogger.Info("Registry cleared successfully");
            
            EditorUtility.DisplayDialog("Registry Clean", "Registry cleared successfully", "OK");
        }
    }
}
