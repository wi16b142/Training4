using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Training4.Model
{
    public class ClientHandler
    {
        public Socket clientSocket;
        private Action<Socket, string> requestBroadcast;
        Thread receivingThread;
        private const string endMsg = "@quit";
        byte[] buffer = new byte[512];

        public ClientHandler(Socket socket, Action<Socket, string> broadcast)
        {
            clientSocket = socket;
            requestBroadcast = broadcast;

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
            string msg = "";
            while (receivingThread.IsAlive)
            {  
                while (!msg.Equals(endMsg))
                {
                    int length = clientSocket.Receive(buffer);
                    msg = Encoding.UTF8.GetString(buffer, 0, length);
                    requestBroadcast(clientSocket, msg);
                }
                StopReceiving();
            }
        }

        public void StopReceiving()
        {
            Send(endMsg);
            clientSocket.Close(1);
            receivingThread.Abort();
        }

        public void Send(string outgoing)
        {
            if(clientSocket.Connected) clientSocket.Send(Encoding.UTF8.GetBytes(outgoing));
        }



    }
}
