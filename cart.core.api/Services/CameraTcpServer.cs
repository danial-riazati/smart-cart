using cart.core.api.Dtos;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace cart.core.api.Services
{
    public class CameraTcpServer
    {
        private readonly int _port;
        private readonly TcpListener _listener;
        private readonly Func<TcpClient, Task> _handler;
        public Queue<string> itmeQueue = new Queue<string>();
        string[] recieveData = new string[1024];
        int dataSize = 0;
        public CameraTcpServer(int port, Func<TcpClient, Task> handler)
        {
            _port = port;
            _handler = handler;
            _listener = new TcpListener(IPAddress.Any, port);
        }
        public CameraTcpServer() { }

        public async Task Start()
        {
            _listener.Start();

            while (true)
            {
                var client = await _listener.AcceptTcpClientAsync();
                _ = HandleClientAsync(client);
            }
        }

        public void Stop()
        {
            _listener.Stop();
        }

        private async Task HandleClientAsync(TcpClient client)
        {
            try
            {
                NetworkStream stream = client.GetStream();
                byte[] buffer = new byte[1024];
                int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
                string data = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                itmeQueue.Enqueue(data);
                await SendResponseAsync(stream, "Data received.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error handling client: {ex.Message}");
            }
         
        }
        private async Task SendResponseAsync(NetworkStream stream, string message)
        {
            byte[] buffer = Encoding.UTF8.GetBytes(message);
            await stream.WriteAsync(buffer, 0, buffer.Length);
        }
        public string GetReceivedData()
        {
            string data;
            if (itmeQueue.TryDequeue(out data))
            {
                return data;
            }
            else
            {
                return "No data received yet.";
            }
        }
        public string GetNextEvent()
        {
            return itmeQueue.Dequeue();
        }
    }
}
