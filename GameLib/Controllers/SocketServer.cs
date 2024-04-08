using GameLib.Models;
using System.Drawing;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace GameLib
{
    public class SocketServer : Socket
    {
        public SocketServer(IPEndPoint ipEndP)
            : base(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp)
        {
            this.Bind(ipEndP);
            this.Listen();
        }

        public void fillClients(in int SIZE, List<Socket?> clients)
        {
            int i = 0;

            while (i < SIZE)
            {
                if (getClient(out Socket? client))
                {
                    clients.Add(client);                    
                    i++;
                }
            }
        }

        private bool getClient(out Socket? socket)
        {
            try
            {
                socket = this.Accept();
                return true;
            }
            catch
            {
                socket = null;
                return false;
            }
        }
    }
}
