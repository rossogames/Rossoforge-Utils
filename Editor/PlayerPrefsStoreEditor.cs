using Rossoforge.Utils.IO;
using Rossoforge.Utils.Logger;
using UnityEditor;

namespace Rossoforge.Utils.Editor
{
    public class PlayerPrefsStoreEditor
    {
        [MenuItem("Rossoforge/Registry/Delete")]
        public static void DeleteAllRegistry()
        {
            PlayerPrefsStore.DeleteAll();
            PlayerPrefsStore.Save();
            RossoLogger.Info("Registry cleared successfully");
            
            EditorUtility.DisplayDialog("Registry Clean", "Registry cleared successfully", "OK");
        }
    }
}
