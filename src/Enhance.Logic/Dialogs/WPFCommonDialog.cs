using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WIA;

namespace Enhance.Logic.Dialogs
{
    public class WPFCommonDialog: WIA.ICommonDialog
    {
        public object ShowAcquisitionWizard(Device Device)
        {
            throw new NotImplementedException();
        }

        public ImageFile ShowAcquireImage(WiaDeviceType DeviceType = WiaDeviceType.UnspecifiedDeviceType, WiaImageIntent Intent = WiaImageIntent.UnspecifiedIntent, WiaImageBias Bias = WiaImageBias.MaximizeQuality, string FormatID = "{00000000-0000-0000-0000-000000000000}", bool AlwaysSelectDevice = false, bool UseCommonUI = true, bool CancelError = false)
        {
            throw new NotImplementedException();
        }

        public Device ShowSelectDevice(WiaDeviceType DeviceType = WiaDeviceType.UnspecifiedDeviceType, bool AlwaysSelectDevice = false, bool CancelError = false)
        {
            throw new NotImplementedException();
        }

        public Items ShowSelectItems(Device Device, WiaImageIntent Intent = WiaImageIntent.UnspecifiedIntent, WiaImageBias Bias = WiaImageBias.MaximizeQuality, bool SingleSelect = true, bool UseCommonUI = true, bool CancelError = false)
        {
            throw new NotImplementedException();
        }

        public void ShowDeviceProperties(Device Device, bool CancelError = false)
        {
            throw new NotImplementedException();
        }

        public void ShowItemProperties(Item Item, bool CancelError = false)
        {
            throw new NotImplementedException();
        }

        public object ShowTransfer(Item Item, string FormatID = "{00000000-0000-0000-0000-000000000000}", bool CancelError = false)
        {
            return Item.Transfer(FormatID);
        }

        public void ShowPhotoPrintingWizard(ref object Files)
        {
            throw new NotImplementedException();
        }
    }
}
