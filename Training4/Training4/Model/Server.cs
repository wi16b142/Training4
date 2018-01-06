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
    public class Server
    {
        //serversock
        //list ch
        //acceptingthread
        //broadcast
        //guiupdate
        private Socket serverSocket;
        private string ip;
        private int port;
        private Action newObject;
        private Action<string> guiRefresh;
        private List<ClientHandler> clients;
        Thread acceptingThread;

        public Server(string ip, int port, Action<string> guiRefresh)
        {
            this.ip = ip;
            this.port = port;
            this.guiRefresh = guiRefresh;
            clients = new List<ClientHandler>();

            serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            serverSocket.Bind(new IPEndPoint(IPAddress.Parse(ip), port));
            serverSocket.Listen(10);
            StartAccepting();
        }

        private void StartAccepting()
        {
            acceptingThread = new Thread(Acceept);
            acceptingThread.IsBackground = true;
            acceptingThread.Start();
        }

        private void Acceept()
        {
            while (acceptingThread.IsAlive)
            {
                clients.Add(new ClientHandler(serverSocket.Accept(), Broadcast));
            }
        }

        private void StopAccepting()
        {
            serverSocket.Close(1);
            acceptingThread.Abort();
        }

        public void Broadcast(Socket sender, string msg)
        {
            string senderName = "server";
            
            foreach(var client in clients)
            {
                if (sender != null)
                {
                    senderName = client.clientSocket.ToString();
                    if (client.clientSocket.Equals(sender))
                    {
                        senderName = "you";
                    }
                }
                client.Send(senderName + msg);      
            }

            guiRefresh(senderName + msg);
        }
    }
}
