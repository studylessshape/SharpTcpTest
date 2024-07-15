using System.Net.Sockets;
using System.Text;

namespace Tcp.Client.Wpf.Services
{
    public class ClientService
    {
        private TcpClient client;
        private CancellationToken token;
        public event Action<string> ReceiveCallback;
        public ClientService()
        {
            client = new TcpClient();
            token = new CancellationToken();
            _ = ExecuteAsync(token);
        }

        private Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var thread = new Thread(async () =>
            {
                await RunAsync(stoppingToken);
            });
            thread.Start();
            return Task.CompletedTask;
        }

        private async Task RunAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                if (!client.Connected)
                {
                    try
                    {
                        client.Connect("127.0.0.1", 20101);
                    }
                    catch (Exception e)
                    {
                        ReceiveCallback($"Error: {e.Message}");
                        await Task.Delay(1000, stoppingToken);
                        continue;
                    }
                }

                try
                {
                    var stream = client.GetStream();
                    var bytes = new byte[1024];
                    await stream.ReadAsync(bytes, stoppingToken);

                    ReceiveCallback($"Receive: {Encoding.UTF8.GetString(bytes)}");
                }
                catch (Exception e)
                {
                    ReceiveCallback($"Error: {e.Message}");
                }
            }
        }

        public async Task SendMessageAsync(string message)
        {
            if (!string.IsNullOrEmpty(message))
            {
                try
                {
                    var stream = client.GetStream();
                    await stream.WriteAsync(Encoding.UTF8.GetBytes(message));
                }
                catch (Exception e)
                {
                    ReceiveCallback($"Error: {e.Message}");
                }
            }
        }
    }
}
