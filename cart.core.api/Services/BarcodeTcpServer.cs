using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace cart.core.api
{
    public class BarcodeTcpServer
    {
        private readonly int _port;
        private readonly TcpListener _listener;
        private readonly Func<TcpClient, Task> _handler;

        public BarcodeTcpServer(int port, Func<TcpClient, Task> handler)
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
