using System;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Enhance.Logic.Models;
using Enhance.Logic.Services.Interfaces;
using Enhance.Models;
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

            FetchScanners();
            BackHomeCommand = new DelegateCommand(NavigateBack);
            ScanCommand = new DelegateCommand(Scan);
            PreviewCommand = new DelegateCommand(Preview);
            CopyCommand = new DelegateCommand(CopyImage);
            ManageCommand = new DelegateCommand(ManageImage);

            SelectedScanner = Scanners.FirstOrDefault();

            ColorDepthsList = new ObservableCollection<ColorDepth>(ColorDepths.List);
            ResolutionsList = new ObservableCollection<Resolution>(Resolutions.List);

            ColorDepth = ColorDepths.Color;
            Resolution = Resolutions.R300;
        }

        public ObservableCollection<Scanner> Scanners { get; set; }

        public Scanner SelectedScanner { get; set; }

        public EnhanceImage Image { get; set; }


        public ICommand BackHomeCommand { get; private set; }

        public ICommand ScanCommand { get; private set; }
        public ICommand PreviewCommand { get; private set; }
        public ICommand CopyCommand { get; private set; }
        public ICommand ManageCommand { get; private set; }

        public ColorDepth ColorDepth { get; set; }
        public ObservableCollection<ColorDepth> ColorDepthsList { get; set; } 

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
                var image = scannerService.Scan(SelectedScanner.Device, ColorDepth, Resolution);

                Image = new EnhanceImage { Bitmap = new Bitmap(image) };
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void Preview()
        {
            if (SelectedScanner == null) return;

            try
            {
                var image = scannerService.Scan(SelectedScanner.Device, ColorDepth, Resolutions.R50);

                Image = new EnhanceImage { Bitmap = new Bitmap(image) };
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void CopyImage()
        {
            if (Image == null) return;

            var bs = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
            Image.Bitmap.GetHbitmap(),
            IntPtr.Zero,
            Int32Rect.Empty,
            BitmapSizeOptions.FromEmptyOptions());

            Clipboard.SetImage(bs);
        }

        private void ManageImage()
        {
            if (Image == null) return;

            Controller<HomeController>().InvokeAction(c => c.ManageDocuments(Image));
        }

    }
}
