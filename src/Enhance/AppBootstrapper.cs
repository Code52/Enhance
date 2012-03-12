using Caliburn.Micro.Autofac;
using Enhance.ViewModels;
using Enhance.ViewModels.Interfaces;

namespace Enhance
{
    public class AppBootstrapper : AutofacBootstrapper<ShellViewModel>
    {
        // Using http://buksbaum.us/2011/06/12/introducing-caliburn-micro-autofac/
        protected override void ConfigureBootstrapper()
        {
            base.ConfigureBootstrapper();

            EnforceNamespaceConvention = false;
            
            ViewModelBaseType = typeof (IShell);
        }
    }
}