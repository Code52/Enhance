using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;

namespace Enhance.Storage
{
    public static class FileSystem
    {
        public static DriveInfo[] GetDrives()
        {
            return DriveInfo.GetDrives();
        }

        public static DirectoryInfo[] GetDirectories(string path)
        {
            return new DirectoryInfo(path).GetDirectories();
        }

        public static FileInfo[] GetFiles(string path)
        {
            return new DirectoryInfo(path).GetFiles();
        }

        public static void Save(Bitmap image, string filename)
        {
            if (File.Exists(filename)) File.Delete(filename);

            image.Save(filename);
        }
    }
}
