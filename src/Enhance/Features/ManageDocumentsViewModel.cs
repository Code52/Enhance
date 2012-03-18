using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
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

            Drives = new ObservableCollection<DriveInfo>(FileSystem.GetDrives());

            SelectedDriveChangedCommand = new DelegateCommand(SelectedDriveChanged);
            SelectedDirectoryChangedCommand = new DelegateCommand(SelectedDirectoryChanged);
            SelectedDirectoryOpenedCommand = new DelegateCommand(SelectedDirectoryOpened);
            UpDirectoryCommand = new DelegateCommand(UpDirectory);
            MyPicturesCommand = new DelegateCommand(MyPictures);

            if(image != null)
            {
                Image = image;
                if(!string.IsNullOrWhiteSpace(image.Filename))
                {
                    SetSelectedItemsToFile(image.Filename);
                }
            }
        }

        public ICommand BackHomeCommand { get; private set; }

        public ObservableCollection<DriveInfo> Drives { get; set; }
        public DriveInfo SelectedDrive { get; set; }

        public ObservableCollection<DirectoryInfo> Directories { get; set; }
        public DirectoryInfo SelectedDirectory { get; set; }
        private DirectoryInfo CurrentDirectory { get; set; }

        public ObservableCollection<FileInfo> Files { get; set; }
        public FileInfo SelectedFile { get; set; }

        public ICommand SelectedDriveChangedCommand { get; private set; }
        public ICommand SelectedDirectoryChangedCommand { get; private set; }
        public ICommand SelectedDirectoryOpenedCommand { get; private set; }
        public ICommand UpDirectoryCommand { get; private set; }
        public ICommand MyPicturesCommand { get; private set; }

        public EnhanceImage Image { get; set; }

        public void SelectedDriveChanged()
        {
            if (SelectedDrive == null)
            {
                Directories = null;
                return;
            }

            CurrentDirectory = SelectedDrive.RootDirectory;
            Directories = new ObservableCollection<DirectoryInfo>(FileSystem.GetDirectories(SelectedDrive.RootDirectory.FullName));
        }

        public void SelectedDirectoryChanged()
        {
            if (SelectedDirectory == null)
            {
                Files = null;
                return;
            }

            Files = new ObservableCollection<FileInfo>(FileSystem.GetFiles(SelectedDirectory.FullName));
        }

        public void SelectedDirectoryOpened()
        {
            CurrentDirectory = SelectedDirectory;
            var directory = SelectedDirectory.FullName;

            Directories = new ObservableCollection<DirectoryInfo>(FileSystem.GetDirectories(directory));
        }

        public void UpDirectory()
        {
            if (SelectedDrive == null) return;
            if (CurrentDirectory.FullName == SelectedDrive.RootDirectory.FullName) return;

            SelectedDirectory = CurrentDirectory.Parent;

            SelectedDirectoryOpened();
        }

        public void MyPictures()
        {
            SetSelectedItemsToDirectory(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures));
        }

        private void SetSelectedItemsToDirectory(string directory)
        {
            SelectedDrive = Drives.FirstOrDefault(d => d.RootDirectory.FullName == new DriveInfo(directory).RootDirectory.FullName);
            CurrentDirectory = new DirectoryInfo(directory);
            Directories = new ObservableCollection<DirectoryInfo>(FileSystem.GetDirectories(CurrentDirectory.FullName));
            Files = new ObservableCollection<FileInfo>(FileSystem.GetFiles(CurrentDirectory.FullName));
        }

        private void SetSelectedItemsToFile(string filename)
        {
            var file = new FileInfo(filename);
            SetSelectedItemsToDirectory(file.Directory.FullName);
        }

    }
}