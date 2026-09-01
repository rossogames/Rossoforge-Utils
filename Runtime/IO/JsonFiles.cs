using Newtonsoft.Json;

namespace Rossoforge.Utils.IO
{
    public static class JsonFiles
    {
        private static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            Formatting = Formatting.Indented,
            TypeNameHandling = TypeNameHandling.Auto
        };

        public static bool TryLoad<T>(string path, out T data)
        {
            data = default;

            if (!Files.ExistsFile(path))
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
            return JsonConvert.SerializeObject(data, Settings);
        }

        public static T Deserialize<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json, Settings);
        }
    }
}
