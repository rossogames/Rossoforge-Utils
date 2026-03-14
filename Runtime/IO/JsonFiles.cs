using UnityEngine;

namespace Rossoforge.Utils.IO
{
    public static class JsonFiles
    {
        public static bool TryLoad<T>(string path, out T data)
        {
            data = default;

            if (!Files.Exists(path))
                return false;

            try
            {
                var json = Files.ReadAllText(path);
                data = Deserialize<T>(json);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static void Save<T>(string path, T data)
        {
            var json = Serialize(data);
            Files.WriteAllText(path, json);
        }

        public static string Serialize<T>(T data)
        {
            return JsonUtility.ToJson(data, true);
        }

        public static T Deserialize<T>(string json)
        {
            return JsonUtility.FromJson<T>(json);
        }
    }
}
