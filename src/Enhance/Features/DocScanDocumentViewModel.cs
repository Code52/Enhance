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
    public class DocScanDocumentViewModel : ViewModelBase
    {
        readonly IScannerService scannerService;

        public DocScanDocumentViewModel(IScannerService scannerService)
        {
            this.scannerService = scannerService;

            FetchScanners();
            BackHomeCommand = new DelegateCommand(NavigateBack);
            ScanCommand = new DelegateCommand(Scan);
            PreviewCommand = new DelegateCommand(Preview);
            ManageCommand = new DelegateCommand(ManageImage);

            SelectedScanner = Scanners.FirstOrDefault();

            ResolutionsList = new ObservableCollection<Resolution>(Resolutions.List);

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
                var image = scannerService.Scan(SelectedScanner.Device, ColorDepths.BlackAndWhite, Resolution);

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
                var image = scannerService.Scan(SelectedScanner.Device, ColorDepths.BlackAndWhite, Resolutions.R50);

                Image = new EnhanceImage { Bitmap = new Bitmap(image) };
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ManageImage()
        {
            if (Image == null) return;

            Controller<HomeController>().InvokeAction(c => c.ManageDocuments(Image));
        }

    }
}
