using System.IO;
using System.Net;
using System.Net.Sockets;

namespace Server
{
    class Program
    {
        static int size;
        static Socket socket;
        static EndPoint clientEndPoint;
        static IPEndPoint serverEndPoint;

        const int serverPort = 52000;
        const int clientPort = 54000;
        const string ip = "127.0.0.1";
        static byte[] data = new byte[1024];

        static void Main(string[] args)
        {
            Initialise();
            Listening();
        }

        static void Initialise()
        {
            serverEndPoint = new IPEndPoint(IPAddress.Parse(ip), serverPort);
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

            socket.Bind(serverEndPoint);
        }

        static void Listening()
        {
            clientEndPoint = new IPEndPoint(IPAddress.Parse(ip), clientPort);

            using (FileStream fstream = new FileStream("Image.png", FileMode.OpenOrCreate))
            {
                do
                {
                    size = socket.ReceiveFrom(data, ref clientEndPoint);
                    fstream.Write(data, 0, size);

                } while (socket.Available > 0);
            }

            socket.Shutdown(SocketShutdown.Both);
            socket.Close();
        }
    }
}
