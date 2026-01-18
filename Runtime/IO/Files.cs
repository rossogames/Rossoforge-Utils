using Rossoforge.Utils.Logger;
using System;
using System.IO;

namespace Rossoforge.Utils.IO
{
    public static class Files
    {
        public static bool Exists(string path)
        {
            return File.Exists(path);
        }
        public static string ReadAllText(string path)
        {
            return File.ReadAllText(path);
        }

        public static void WriteAllText(string path, string text, System.Text.Encoding encoding = null)
        {
            EnsureDirectoryForFile(path);
            File.WriteAllText(path, text, encoding ?? System.Text.Encoding.UTF8);
        }

        public static bool TryLoad(
            string path,
            FileMode mode,
            FileAccess access,
            FileShare share,
            out FileStream stream)
        {
            try
            {
                stream = new FileStream(path, mode, access, share);
                return true;
            }
            catch (Exception e)
            {
                RossoLogger.Error($"[Files] Cannot open file: {path}\n{e}");
                stream = null;
                return false;
            }
        }

        private static void EnsureDirectoryForFile(string filePath)
        {
            var dir = Path.GetDirectoryName(filePath);
            if (!string.IsNullOrEmpty(dir) && !Directory.Exists(dir))
                Directory.CreateDirectory(dir);
        }
    }
}