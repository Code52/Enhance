using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using Enhance.Logic.Dialogs;
using Enhance.Logic.Models;
using Enhance.Logic.Services.Interfaces;
using WIA;

namespace Enhance.Logic.Services
{
    public class ScannerService : IScannerService
    {
        public IEnumerable<DeviceInfo> GetDevices()
        {
            return new DeviceManager().DeviceInfos.Cast<DeviceInfo>().ToList();
        }

        public IEnumerable<Scanner> GetScanners()
        {
            return
                GetDevices().Where(s => s.Type == WiaDeviceType.ScannerDeviceType).Select(
                    s => new Scanner
                             {
                                 Name = ((dynamic)(s.Properties["Description"])).Value,
                                 Device = s
                             });
        }

        const string wiaFormatBMP = "{B96B3CAB-0728-11D3-9D7B-0000F81EF32E}";
        const string wiaFormatPNG = "{B96B3CAF-0728-11D3-9D7B-0000F81EF32E}";
        const string wiaFormatGIF = "{B96B3CB0-0728-11D3-9D7B-0000F81EF32E}";
        const string wiaFormatJPEG = "{B96B3CAE-0728-11D3-9D7B-0000F81EF32E}";
        const string wiaFormatTIFF = "{B96B3CB1-0728-11D3-9D7B-0000F81EF32E}";

        public Image Scan(DeviceInfo device)
        {
            return Scan(device, PageSizes.A5, ColorDepths.Color, Resolutions.R72, Orientations.Landscape, true);
        }

        public Image Scan(DeviceInfo device, ColorDepth colorDepth, Resolution resolution)
        {
            return Scan(device, null, colorDepth, resolution, null, false);
        }

        public Image Scan(DeviceInfo device, PageSize pageSize, ColorDepth colorDepth, Resolution resolution, Orientation orientation, bool setSize = true)
        {
            if (device == null)
                throw new ArgumentException("Device must be specified");

            var scanner = device.Connect();

            var wiaCommonDialog = new WPFCommonDialog();
            var item = scanner.Items[1];

            SetupPageSize(item, pageSize, colorDepth, resolution, orientation, setSize);

            var image = (ImageFile)wiaCommonDialog.ShowTransfer(item, wiaFormatBMP, false);

            string fileName = Path.GetTempFileName();
            File.Delete(fileName);
            image.SaveFile(fileName);
            image = null;

            // add file to output list
            return Image.FromFile(fileName);
        }

        private void SetupPageSize(WIA.Item item, PageSize pageSize, ColorDepth colorDepth, Resolution resolution, Orientation orientation, bool setSize)
        {
            if (item == null) return;

            //Get Extents at 100 Res
            item.Properties["Horizontal Resolution"].set_Value(100);
            item.Properties["Vertical Resolution"].set_Value(100);
            double horizontalExtent = item.Properties["Horizontal Extent"].get_Value() / 100;
            double verticalExtent = item.Properties["Vertical Extent"].get_Value() / 100;


            item.Properties["Horizontal Resolution"].set_Value(resolution.Value);
            item.Properties["Vertical Resolution"].set_Value(resolution.Value);

            if (setSize)
            {
                if (orientation.Direction == 0)
                {
                    item.Properties["Horizontal Extent"].set_Value(resolution.Value * pageSize.Width);
                    item.Properties["Vertical Extent"].set_Value(resolution.Value * pageSize.Height);
                }
                else
                {
                    item.Properties["Horizontal Extent"].set_Value(resolution.Value * pageSize.Height);
                    item.Properties["Vertical Extent"].set_Value(resolution.Value * pageSize.Width);
                }
            }
            else
            {
      
                item.Properties["Horizontal Extent"].set_Value(resolution.Value * horizontalExtent);
                item.Properties["Vertical Extent"].set_Value(resolution.Value * verticalExtent);
            }

            item.Properties["Current Intent"].set_Value(colorDepth.Value);
            item.Properties["Bits Per Pixel"].set_Value(colorDepth.BitsPerPixel);
        }
    }
}
