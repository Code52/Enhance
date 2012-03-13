using System.ComponentModel;
using WIA;

namespace Enhance.Logic.Models
{
    public class Scanner: INotifyPropertyChanged
    {
        public string Name { get; set; }
        public DeviceInfo Device { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
