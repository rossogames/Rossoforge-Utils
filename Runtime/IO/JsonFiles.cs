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
                data = JsonUtility.FromJson<T>(json);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static void Save<T>(string path, T data)
        {
            var json = JsonUtility.ToJson(data, true);
            Files.WriteAllText(path, json);
        }
    }
}
