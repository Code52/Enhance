using System;
using System.Collections.Generic;
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

            OrientationsList = new ObservableCollection<Orientation>(Orientations.List);
            ColorDepthsList = new ObservableCollection<ColorDepth>(ColorDepths.List);
            PageSizesList = new ObservableCollection<PageSize>(PageSizes.List);
            ResolutionsList = new ObservableCollection<Resolution>(Resolutions.List);

            Orientation = Orientations.Portrait;
            ColorDepth = ColorDepths.Color;
            Resolution = Resolutions.R72;
            PageSize = PageSizes.A5;
        }

        public ObservableCollection<Scanner> Scanners { get; set; }

        public Scanner SelectedScanner { get; set; }

        public BitmapImage Image { get; set; }

        public Visibility IsProgressVisible { get; set; }

        public ICommand BackHomeCommand { get; private set; }

        public ICommand ScanCommand { get; private set; }

        public Orientation Orientation { get; set; }
        public ObservableCollection<Orientation> OrientationsList { get; set; } 
        
        public ColorDepth ColorDepth { get; set; }
        public ObservableCollection<ColorDepth> ColorDepthsList { get; set; } 

        public PageSize PageSize { get; set; }
        public ObservableCollection<PageSize> PageSizesList { get; set; }
        
        public Resolution Resolution { get; set; }
        public ObservableCollection<Resolution> ResolutionsList { get; set; } 

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

                var image = scannerService.Scan(SelectedScanner.Device, PageSize, ColorDepth, Resolution, Orientation);
              
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
            string fileName = Path.GetTempFileName();
            File.Delete(fileName);
            
            bitmap.Save(fileName, ImageFormat.Bmp);

            var bitmapImage = new BitmapImage();

            bitmapImage.BeginInit();
            bitmapImage.StreamSource = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            bitmapImage.EndInit();

            return bitmapImage;
        }
    }
}
