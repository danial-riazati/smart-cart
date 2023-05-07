using System.Net;
using System.Net.Sockets;
using System.Text;

class Program
{
    static void Main(string[] args)
    {
        try
        {
            TcpListener server = new TcpListener(System.Net.IPAddress.Any, 1302);
            server.Start();
            string[] recieveData = new string[1024];
            int dataSize = 0;

            while (true)
            {
                Console.WriteLine("Server listening on port 1302...");
                TcpClient client = server.AcceptTcpClient();
                Console.WriteLine("Client connected!");

                NetworkStream stream = client.GetStream();

                while (client.Connected)
                {
                   
                    byte[] data = new byte[1024];
                    int count = stream.Read(data, 0, data.Length);
                 
                    if (count > 0)
                    {
                        string received = Encoding.ASCII.GetString(data, 0, count);
                        recieveData[dataSize] = Encoding.ASCII.GetString(data, 0, count);
                        dataSize++;
                        Console.WriteLine("Received: {0}", received);
                    }
                    if(count==0)
                    {
                        try
                        {
                            Console.WriteLine("Reconnecting...");
                            client = new TcpClient("localhost", 1302);
                            stream = client.GetStream();
                            Console.WriteLine("Client reconnected!");
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("Error: {0}", e.Message);
                            Thread.Sleep(5000);
                        }

                    }
                }

                Console.WriteLine("Client disconnected.");
                //stream.Close();
                //client.Close();
                // Try to reconnect the client
                while (!client.Connected)
                {
                    try
                    {
                        Console.WriteLine("Reconnecting...");
                        client = new TcpClient("localhost", 1302);
                        stream = client.GetStream();
                        Console.WriteLine("Client reconnected!");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Error: {0}", e.Message);
                        Thread.Sleep(5000);
                    }
                }
            }
        }catch(Exception ex)
        {
            Console.WriteLine("exception : ",ex.ToString());
           
        }
    }
}
