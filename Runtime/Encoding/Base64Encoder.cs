using System;

namespace Rossoforge.Utils.Encoding
{
    public static class Base64Encoder
    {
        private static string _key = "Rossoforge";

        public static void SetKey(string key)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentException("Encoder key cannot be null or empty");

            _key = key;
        }

        public static string Encode(string input)
        {
            var data = System.Text.Encoding.UTF8.GetBytes(input);
            var key = System.Text.Encoding.UTF8.GetBytes(_key);

            for (int i = 0; i < data.Length; i++)
                data[i] ^= key[i % key.Length];

            return System.Convert.ToBase64String(data);
        }

        public static bool TryDecode(string input, out string output)
        {
            output = null;

            try
            {
                var data = System.Convert.FromBase64String(input);
                var key = System.Text.Encoding.UTF8.GetBytes(_key);

                for (int i = 0; i < data.Length; i++)
                    data[i] ^= key[i % key.Length];

                output = System.Text.Encoding.UTF8.GetString(data);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
