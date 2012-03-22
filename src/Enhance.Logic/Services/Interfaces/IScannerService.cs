using System.Collections.Generic;
using System.Drawing;
using Enhance.Logic.Models;
using WIA;

namespace Enhance.Logic.Services.Interfaces
{
    public interface IScannerService
    {
        IEnumerable<DeviceInfo> GetDevices();
        IEnumerable<Scanner> GetScanners();

        Image Scan(DeviceInfo device);
        Image Scan(DeviceInfo device, ColorDepth colorDepth, Resolution resolution);
        Image Scan(DeviceInfo device, PageSize pageSize, ColorDepth colorDepth, Resolution resolution, Orientation orientation, bool scanSize = true);
    }
}