using Autofac;
using Caliburn.Micro.Autofac;
using Enhance.Logic.Services;
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

        protected override void ConfigureContainer(Autofac.ContainerBuilder builder)
        {
            base.ConfigureContainer(builder);

            builder.RegisterType<ScannerService>().AsImplementedInterfaces();
        }
    }
}