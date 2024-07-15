using System.Windows;
using Tcp.Client.Wpf.ViewModels;

namespace Tcp.Client.Wpf.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow(MainWindowModel model)
        {
            this.DataContext = model;
            InitializeComponent();
        }
    }
}