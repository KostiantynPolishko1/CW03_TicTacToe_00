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

        public (char, string?) getPlayerData()
        {
            Console.Write("enter player name: ");
            string? userName = Console.ReadLine();

            Console.Write("select type X | O: ");
            char type = Console.ReadKey().KeyChar;

            return (type, userName);
        }
    }
}