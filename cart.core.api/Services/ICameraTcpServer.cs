using System.Net.Sockets;

namespace cart.core.api.Services
{
    public interface ICameraTcpServer
    {
        Task Start();
        void Stop();
        Task HandleClientAsync(TcpClient client);
        Task SendResponseAsync(NetworkStream stream, string message);

        string GetNextEvent();
    }
}
