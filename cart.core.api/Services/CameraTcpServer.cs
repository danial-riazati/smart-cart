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
        string[] recieveData = new string[1024];
        int dataSize = 0;
        public CameraTcpServer(int port, Func<TcpClient, Task> handler)
        {
            _port = port;
            _handler = handler;
            _listener = new TcpListener(IPAddress.Any, port);
        }

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
                await _handler(client);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex);
              
            }
            finally
            {
                client.Close();
            }
        }
    }
}
