using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text.Json;
using GameLib;
using GameLib.Models;

namespace Client
{
    internal class ProgramClient
    {
        static void Main(string[] args)
        {
            using (SocketClient client = new SocketClient(new ConnectIPEndP().getIpeP()))
            {
                Console.WriteLine($"client connect to server {client.RemoteEndPoint}");

                (char type, string? userName) = client.getPlayerData();
                UserPlayer player = new UserPlayer(type, userName) {};
                GameField? gameField = null;

                while (true)
                {
                    Extensions.sentPlayer(client, player);
                    gameField = Extensions.getGameField(client);

                    gameField.showField();
                    Console.Read();
                }               
            }
        }
    }
}