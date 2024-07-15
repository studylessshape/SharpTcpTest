using SimpleLogger;
using System.Net;
using System.Net.Sockets;
using System.Text;

var server = new TcpListener(IPAddress.Parse("127.0.0.1"), 20101);

server.Start();

Log.LogInfo($"Server start on {server.Server.LocalEndPoint}");

while (true)
{
    var client = server.AcceptTcpClient();
    var thread = new Thread(() =>
    {
        var stream = client.GetStream();
        while (client.Connected)
        {
            try
            {
                var key = Console.ReadKey(true);
                var message = "";
                if (key.Key == ConsoleKey.S)
                {
                    message = "Marking start";
                }
                else if (key.Key == ConsoleKey.F)
                {
                    message = "Marking finish";
                }
                else if (key.Key == ConsoleKey.R)
                {
                    message = "OK";
                }
                if (!string.IsNullOrEmpty(message))
                {
                    Log.LogInfo($"Send: {message}");
                    stream.Write(Encoding.UTF8.GetBytes(message));
                }
            }
            catch (Exception e)
            {
                Log.LogError($"Write error: {e.Message}");
            }
        }
    });
    thread.Start();
    try
    {
        var stream = client.GetStream();
        // 接收循环
        while (client.Connected)
        {
            var bytes = new byte[1024];
            stream.Read(bytes);

            Log.LogInfo($"Receive: {Encoding.UTF8.GetString(bytes)}");
        }
    }
    catch (Exception e)
    {
        Log.LogError(e.Message);
        break;
    }
    finally
    {
        client.Close();
    }
}