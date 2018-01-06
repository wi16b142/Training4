using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Training4.Model
{
    public class Client
    {
        //receive
        //guiupdate
        //sendmsg
        private string ip;
        private int port;
        private Action newObject;
        private Action<string> guiRefresh;
        Socket clientSocket;
        TcpClient client;
        Thread receivingThread;
        const string endMsg = "@quit";
        byte[] buffer = new byte[512];

        public Client(string ip, int port, Action<string> guiRefresh)
        {
            this.ip = ip;
            this.port = port;
            this.guiRefresh = guiRefresh;

            client = new TcpClient();
            client.Connect(IPAddress.Parse(ip), port);
            clientSocket = client.Client;

            StartReceiving();
        }

        private void StartReceiving()
        {
            receivingThread = new Thread(Receive);
            receivingThread.IsBackground = true;
            receivingThread.Start();
        }

        private void Receive()
        {
            while (receivingThread.IsAlive)
            {
                string msg = "";
                while (!msg.Equals(endMsg))
                {
                    int length = clientSocket.Receive(buffer);
                    msg = Encoding.UTF8.GetString(buffer, 0, length);
                    guiRefresh(msg);
                }
                StopReceiving();
            }
        }

        private void StopReceiving()
        {
            clientSocket.Close();
            receivingThread.Abort();
        }

        public void Send(string outgoing)
        {
            if (clientSocket.Connected) clientSocket.Send(Encoding.UTF8.GetBytes(outgoing));
        }
    }
}
