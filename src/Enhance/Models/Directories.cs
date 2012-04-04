using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Enhance.Storage;
using Phoenix;

namespace Enhance.Models
{
    public class Root: ViewModelBase
    {
        public Root()
        {
            Drives = new List<Drive>();
            
            var drives = FileSystem.GetDrives();

            foreach(var drive in drives.Where(x=>x.DriveType != System.IO.DriveType.Network))
            {
                Drives.Add(new Drive(drive));    
            }
        }
        public string Name { get { return "My Computer"; } }
        public List<Drive> Drives { get; set; }
    }

    public class Drive: ViewModelBase
    {
        public Drive(DriveInfo drive)
        {
            Name = drive.Name;
            Path = drive.RootDirectory.FullName;

            Directories = new List<Directory>();
            var directories = FileSystem.GetDirectories(drive.RootDirectory.FullName);

            if (directories == null) return;

            foreach(var directory in directories)
            {
                if(directory != null)
                    Directories.Add(new Directory(directory));
            }
        }

        public string Name { get; set; }
        public string Path { get; set; }

        public List<Directory> Directories { get; set; }
    }

    public class Directory: ViewModelBase
    {
        public Directory(DirectoryInfo directory)
        {
            Name = directory.Name;
            Path = directory.FullName;

            Directories = new List<Directory>();
            var directories = FileSystem.GetDirectories(directory.FullName);

            if (directories == null) return;

            foreach(var d in directories)
            {
                if(d != null)
                    Directories.Add(new Directory(d));
            }
        }

        public string Name { get; set; }
        public string Path { get; set; }

        public List<Directory> Directories { get; set; }
    }
}
