using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Tcp.Client.Wpf.Services;

namespace Tcp.Client.Wpf.ViewModels
{
    public partial class MainWindowModel : ObservableObject
    {
        private readonly ClientService client;

        [ObservableProperty]
        private string receiveMessage = "";

        [ObservableProperty]
        private string sendMessage = "";

        public MainWindowModel(ClientService service)
        {
            service.ReceiveCallback += InsertMessage;
            this.client = service;
        }

        public void InsertMessage(string message)
        {
            ReceiveMessage = $"{ReceiveMessage}\n[{DateTime.Now:HH:mm:dd}] {message}";
        }

        [RelayCommand]
        public async Task SendAsync()
        {
            InsertMessage($"Send: {SendMessage}");
            await client.SendMessageAsync(SendMessage);
            SendMessage = "";
        }
    }
}
