using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        private string ip;
        private int port;
        private Action newObject;
        private Action<string> guiRefresh;

        public Server(string ip, int port, Action newObject, Action<string> guiRefresh)
        {
            this.ip = ip;
            this.port = port;
            this.newObject = newObject;
            this.guiRefresh = guiRefresh;
        }
    }
}
