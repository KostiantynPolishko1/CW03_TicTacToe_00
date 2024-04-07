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

        public void checkField(GameField gameField, Player? player)
        {

        }
    }
}
