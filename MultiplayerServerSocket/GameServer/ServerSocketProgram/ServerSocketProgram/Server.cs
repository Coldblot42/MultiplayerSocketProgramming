 using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ServerSocketProgram
{
    class Server
    {
        public static int MaxPlayers { get; private set; }
        public static int Port { get; private set; }

        private static TcpListener tcpListener;

        public static Dictionary<int, Client> clients = new Dictionary<int, Client>();
        public static void Start(int _maxPlayers,int _port)
        {
            MaxPlayers = _maxPlayers;
            Port = _port;
            Console.WriteLine("Starting the Server...");

            tcpListener = new TcpListener(IPAddress.Any, Port);

            tcpListener.Start();

            tcpListener.BeginAcceptTcpClient(new AsyncCallback(TCPConnectCallBack),null);
            Console.WriteLine("Server Started on Port {0}",Port);
        }

        private static void TCPConnectCallBack(IAsyncResult ar)
        {
            TcpClient client = tcpListener.EndAcceptTcpClient(ar);
            tcpListener.BeginAcceptTcpClient(new AsyncCallback(TCPConnectCallBack), null);
            Console.WriteLine($"Incomming connection from remote point {client.Client.RemoteEndPoint}");
            for (int i = 1; i < MaxPlayers; i++)
            {
                if (clients[i].tcp.socket == null)
                {
                    clients[i].tcp.Connect(client);
                    return;
                }
            }

            Console.WriteLine($"");
        }
        private static void InitiliazeServerData()
        {
            for (int i = 1; i < MaxPlayers; i++)
            {
                clients.Add(i, new Client(i));
            }
        }
    }
}
