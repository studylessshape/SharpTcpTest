using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Windows;
using Tcp.Client.Wpf.Views;

namespace Tcp.Client.Wpf
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        internal IServiceProvider _serviceProvider { get; set; }
        public IHost _Host { get; private set; }

        private void BuildHost()
        {
            _Host = Host.CreateDefaultBuilder()
                .ConfigureServices(Hostup.ConfigureServices)
                .Build();
            // 服务注入初始化
            _serviceProvider = _Host.Services;
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            BuildHost();

            var mainPage = _serviceProvider.GetRequiredService<MainWindow>();
            mainPage.Show();

            var thread = new Thread(async () =>
            {
                try
                {
                    await _Host.RunAsync();
                }
                catch (Exception)
                {
                }
            });
            thread.Start();
        }
    }

}
