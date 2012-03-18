using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Input;
using Enhance.Models;
using Enhance.Storage;
using Phoenix;
using Phoenix.Commands;

namespace Enhance.Features
{
    public class ManageDocumentsViewModel : ViewModelBase
    {
        public ManageDocumentsViewModel(EnhanceImage image)
        {
            BackHomeCommand = new DelegateCommand(NavigateBack);

            Directories = new Root();

            //foreach(var drive in drives)
            //{
            //    Directories.Add(new FileInfo(drive.RootDirectory.FullName));
            //}
        }

        public ICommand BackHomeCommand { get; private set; }

        public Root Directories { get; set; }
        //public ObservableCollection<FileSystemInfo> Directories { get; set; }

        public ObservableCollection<FileInfo> Files { get; set; }
        public FileInfo SelectedFile { get; set; }

        public EnhanceImage Image { get; set; }
    }
}