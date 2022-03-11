using System;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            var data = new byte[1024];
            const int serverPort = 52000;
            const int clientPort = 54000;
            const string ip = "127.0.0.1";

            Console.Write("Введите имя файла, который хотите сохранить на сервере: ");
            string name = Console.ReadLine();

            var clientEndPoint = new IPEndPoint(IPAddress.Parse(ip), clientPort);
            EndPoint serverEndPoint = new IPEndPoint(IPAddress.Parse(ip), serverPort);
            var socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

            socket.Bind(clientEndPoint);

            using (var fstream = new FileStream(name, FileMode.Open))
            {
                while (fstream.Read(data, 0, data.Length) > 0)
                {
                    socket.SendTo(data, serverEndPoint);
                }
            }

            socket.Shutdown(SocketShutdown.Both);
            socket.Close();
        }
    }
}
