using System;
using System.IO;

namespace SimpleCM.Tools
{
    class PathUtils
    {
        public static string GetLogPath()
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData, Environment.SpecialFolderOption.Create);
            //string path = Process.GetCurrentProcess().MainModule.FileName;
            path += "\\simpleCM\\Log";
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            return path;
        }

        public static string GetConfigPath()
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            path = path + "\\simpleCM\\";

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            return path;
        }
    }
}
