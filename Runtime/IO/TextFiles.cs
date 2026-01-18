using System.IO;

namespace Rossoforge.Utils.IO
{
    public static class TextFiles
    {
        public static string Load(string path)
        {
            if (!Files.Exists(path))
                throw new FileNotFoundException(path);

            return Files.ReadAllText(path);
        }

        public static bool TryLoad(string path, out string text)
        {
            if (!Files.Exists(path))
            {
                text = null;
                return false;
            }

            text = Files.ReadAllText(path);
            return true;
        }

        public static void Save(string path, string text, System.Text.Encoding encoding = null)
        {
            Files.WriteAllText(path, text, encoding ?? System.Text.Encoding.UTF8);
        }
    }
}
