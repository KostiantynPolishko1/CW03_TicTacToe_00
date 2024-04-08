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

                UserPlayer? player = Extensions.getPlayer(client);
                player?.setUserName(player.type);
                Extensions.sentPlayer(client, player);

                GameField? gameField = Extensions.getGameField(client);
                gameField?.showField();

                Console.WriteLine($"Your name: {player?.userName} | You play {player?.type}");

                while (true)
                {
                    Extensions.sentPlayer(client, player);
                    gameField = Extensions.getGameField(client);

                    gameField?.showField();
                    Console.Read();
                }               
            }
        }
    }
}