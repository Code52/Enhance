using System;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;
using Caliburn.Micro;
using Enhance.Logic.Models;
using Enhance.Logic.Services.Interfaces;
using Enhance.ViewModels.Interfaces;

namespace Enhance.ViewModels
{

    public class ShellViewModel : Screen, IShell
    {
        IWindowManager wm { get; set; }
        IScannerService scannerService { get; set; }

        public ShellViewModel(IWindowManager wm, IScannerService scannerService)
        {
            this.scannerService = scannerService;
            IsProgressVisible = Visibility.Hidden;

            FetchScanners();
        }

        public ObservableCollection<Scanner> Scanners { get; set; }

        public Scanner SelectedScanner { get; set; }

        public BitmapImage Image { get; set; }

        public Visibility IsProgressVisible { get; set; }

        private void FetchScanners()
        {
            Scanners = new ObservableCollection<Scanner>(scannerService.GetScanners());
        }

        public void Scan()
        {
            if (SelectedScanner == null) return;

            try
            {
                IsProgressVisible = Visibility.Visible;

                var image = scannerService.Scan(SelectedScanner.Device);

                Image = ConvertImageToBitmapImage(image);

                IsProgressVisible = Visibility.Hidden;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }

        private BitmapImage ConvertImageToBitmapImage(Image bitmap)
        {
            var memoryStream = new MemoryStream();

            bitmap.Save(memoryStream, ImageFormat.Bmp);

            var bitmapImage = new BitmapImage();

            bitmapImage.BeginInit();
            bitmapImage.StreamSource = new MemoryStream(memoryStream.ToArray());
            bitmapImage.EndInit();

            return bitmapImage;
        }
    }
}
