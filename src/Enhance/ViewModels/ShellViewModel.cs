using Caliburn.Micro;
using Enhance.ViewModels.Interfaces;

namespace Enhance.ViewModels {

    public class ShellViewModel : IShell
    {
        IWindowManager wm { get; set; }

        public ShellViewModel(IWindowManager wm)
        {
        }
    }
}
