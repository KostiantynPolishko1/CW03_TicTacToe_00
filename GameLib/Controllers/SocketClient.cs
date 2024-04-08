using GameLib;
using System.Net;
using System.Net.Sockets;

namespace GameLib
{
    public class SocketClient : Socket
    {
        public SocketClient(IPEndPoint ipEndP) 
            : base(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp) 
        {
            this.Connect(ipEndP);
        }
    }
}