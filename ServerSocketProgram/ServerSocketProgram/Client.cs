using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

namespace ServerSocketProgram
{
    class Client
    {
        public static int dataBufferSize = 4096;

        private int id;

        public TCP tcp;
        public Client(int clientId)
        {
            id = clientId;
            tcp = new TCP(id);
        }

        public class TCP
        {
            public TcpClient socket;
            private readonly int id;
            NetworkStream stream;
            byte[] receivedBuffer;
        public TCP(int _id)
        {
            id = _id;
        }
        public void Connect(TcpClient socket)
        {
                socket.ReceiveBufferSize = dataBufferSize;
                socket.SendBufferSize = dataBufferSize;
                stream = socket.GetStream();
                receivedBuffer = new byte[dataBufferSize];

                stream.BeginRead(receivedBuffer, 0, dataBufferSize, ReceiveCallback, null);
        }

        private void ReceiveCallback(IAsyncResult ar)
        {
            try
                {
                    int byteLength = stream.EndRead(ar);
                    if(byteLength<=0)
                    {
                        return;
                    }
                    byte[] dataReceived = new byte[byteLength];
                    Array.Copy(receivedBuffer, dataReceived, byteLength);
                    stream.BeginRead(receivedBuffer, 0, dataBufferSize, ReceiveCallback, null);
                }
            catch(Exception exp)
                {
                    Console.WriteLine($"Error in receiving data {exp}");
                }
        }
        }

    }
}
