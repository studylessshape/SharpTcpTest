using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Tcp.Client.Wpf.Services;
using Tcp.Client.Wpf.ViewModels;
using Tcp.Client.Wpf.Views;

namespace Tcp.Client.Wpf
{
    public static class Hostup
    {
        public static void ConfigureServices(HostBuilderContext context, IServiceCollection services)
        {
            var configuration = context.Configuration;
            RegistViewsAndModels(services);
        }

        public static IServiceCollection RegistViewsAndModels(IServiceCollection services)
        {
            #region views
            services.AddSingleton<MainWindow>();
            #endregion

            #region models
            services.AddSingleton<MainWindowModel>();
            #endregion

            services.AddSingleton<ClientService>();
            return services;
        }
    }
}
