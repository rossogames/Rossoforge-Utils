using UnityEngine;

namespace Rossoforge.Utils.IO
{
    public static class PlayerPrefsStore
    {
        // --------- SAVE ---------
        public static void SaveInt(string key, int value)
        {
            PlayerPrefs.SetInt(key, value);
        }
        public static void SaveFloat(string key, float value)
        {
            PlayerPrefs.SetFloat(key, value);
        }
        public static void SaveString(string key, string value)
        {
            PlayerPrefs.SetString(key, value);
        }
        public static void SaveBool(string key, bool value)
        {
            PlayerPrefs.SetInt(key, value ? 1 : 0);
        }
        public static void SaveEnum<T>(string key, T value) where T : System.Enum
        {
            PlayerPrefs.SetInt(key, System.Convert.ToInt32(value));
        }
        public static void Save()
        {
            PlayerPrefs.Save();
        }

        // --------- LOAD ---------
        public static int LoadInt(string key, int defaultValue = 0)
        {
            return PlayerPrefs.GetInt(key, defaultValue);
        }
        public static float LoadFloat(string key, float defaultValue = 0f)
        {
            return PlayerPrefs.GetFloat(key, defaultValue);
        }
        public static string LoadString(string key, string defaultValue = "")
        {
            return PlayerPrefs.GetString(key, defaultValue);
        }
        public static bool LoadBool(string key, bool defaultValue = false)
        {
            int defaultInt = defaultValue ? 1 : 0;
            return PlayerPrefs.GetInt(key, defaultInt) == 1;
        }
        public static T LoadEnum<T>(string key, T defaultValue) where T : System.Enum
        {
            int defaultInt = System.Convert.ToInt32(defaultValue);
            int value = PlayerPrefs.GetInt(key, defaultInt);
            return (T)System.Enum.ToObject(typeof(T), value);
        }

        // --------- MANAGEMENT ---------
        public static void DeleteKey(string key)
        {
            PlayerPrefs.DeleteKey(key);
        }
        public static void DeleteAll()
        {
            PlayerPrefs.DeleteAll();
        }
        public static bool HasKey(string key)
        {
            return PlayerPrefs.HasKey(key);
        }
    }
}
