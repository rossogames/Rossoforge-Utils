using System.Collections.Generic;
using Newtonsoft.Json;
using Rossoforge.Utils.JsonConverters;

namespace Rossoforge.Utils.IO
{
    public static class JsonFiles
    {
        private static readonly List<JsonConverter> DefaultConverters = new List<JsonConverter>
        {
            new Vector2Converter(),
            new Vector3Converter(),
            new Vector4Converter(),
            new Vector2IntConverter(),
            new Vector3IntConverter(),
            new QuaternionConverter(),
            new ColorConverter()
        };

        private static readonly JsonSerializerSettings DefaultSettings = new JsonSerializerSettings
        {
            Formatting = Formatting.Indented,
            TypeNameHandling = TypeNameHandling.Auto,
            Converters = DefaultConverters
        };

        public static bool TryLoad<T>(string path, out T data, IList<JsonConverter> customConverters = null)
        {
            data = default;

            if (!Files.ExistsFile(path))
                return false;

            try
            {
                var json = Files.ReadAllText(path);
                data = Deserialize<T>(json, customConverters);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool TryLoad<T>(string path, out T data, JsonSerializerSettings settings)
        {
            data = default;

            if (!Files.ExistsFile(path))
                return false;

            try
            {
                var json = Files.ReadAllText(path);
                data = Deserialize<T>(json, settings);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static void Save<T>(string path, T data, IList<JsonConverter> customConverters = null)
        {
            var json = Serialize(data, customConverters);
            Files.WriteAllText(path, json);
        }

        public static void Save<T>(string path, T data, JsonSerializerSettings settings)
        {
            var json = Serialize(data, settings);
            Files.WriteAllText(path, json);
        }

        public static string Serialize<T>(T data, IList<JsonConverter> customConverters = null)
        {
            var settings = CreateSettingsWithCustomConverters(customConverters);
            return JsonConvert.SerializeObject(data, settings);
        }

        public static string Serialize<T>(T data, JsonSerializerSettings settings)
        {
            return JsonConvert.SerializeObject(data, settings ?? DefaultSettings);
        }

        public static T Deserialize<T>(string json, IList<JsonConverter> customConverters = null)
        {
            var settings = CreateSettingsWithCustomConverters(customConverters);
            return JsonConvert.DeserializeObject<T>(json, settings);
        }

        public static T Deserialize<T>(string json, JsonSerializerSettings settings)
        {
            return JsonConvert.DeserializeObject<T>(json, settings ?? DefaultSettings);
        }

        private static JsonSerializerSettings CreateSettingsWithCustomConverters(IList<JsonConverter> customConverters)
        {
            if (customConverters == null || customConverters.Count == 0)
                return DefaultSettings;

            var settings = new JsonSerializerSettings
            {
                Formatting = DefaultSettings.Formatting,
                TypeNameHandling = DefaultSettings.TypeNameHandling,
                Converters = new List<JsonConverter>(DefaultConverters)
            };

            foreach (var converter in customConverters)
            {
                if (!settings.Converters.Contains(converter))
                {
                    settings.Converters.Add(converter);
                }
            }

            return settings;
        }
    }
}