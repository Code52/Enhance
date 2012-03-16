using System;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Enhance.Logic.Models;
using Enhance.Logic.Services.Interfaces;
using Phoenix;
using Phoenix.Commands;

namespace Enhance.Features
{
    public class ScanDocumentViewModel : ViewModelBase
    {
        readonly IScannerService scannerService;

        public ScanDocumentViewModel(IScannerService scannerService)
        {
            this.scannerService = scannerService;
            IsProgressVisible = Visibility.Hidden;

            FetchScanners();
            BackHomeCommand = new DelegateCommand(NavigateBack);
            ScanCommand = new DelegateCommand(Scan);
        }

        public ObservableCollection<Scanner> Scanners { get; set; }

        public Scanner SelectedScanner { get; set; }

        public BitmapImage Image { get; set; }

        public Visibility IsProgressVisible { get; set; }

        public ICommand BackHomeCommand { get; private set; }

        public ICommand ScanCommand { get; private set; }

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
