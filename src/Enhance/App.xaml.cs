using System.Windows;
using Autofac;
using Enhance.Logic.Services;
using Phoenix;
using Phoenix.Frames;
using Phoenix.Extensions.Autofac;

namespace Enhance
{
    public partial class App
    {
        protected override void ConfigurePhoenixHostBuilder(IPhoenixHostBuilder hostBuilder)
        {
            hostBuilder.SetNavigationFrameFactory(() => new TransitionNavigationFrame());
        }

        protected override void ConfigureContainer(ContainerBuilder containerBuilder)
        {
            GetType().Assembly.RegisterControllers(containerBuilder);
            containerBuilder.RegisterType<ScannerService>().AsImplementedInterfaces();
        }

        protected override Window CreateShell()
        {
            return new Shell();
        }
    }
}
