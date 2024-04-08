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
                UserPlayer? player = null;
                GameField? gameField = null;

                try
                {
                    player = Extensions.getPlayer(client);

                    Console.Write($"player {player?.type} enter your name: ");
                    player?.setUserName();

                    Console.WriteLine($"Your name: {player?.userName} | You play {player?.type}");

                    while (true)
                    {
                        gameField = Extensions.getGameField(client);
                        Console.Clear();
                        gameField?.showField();

                        while (true)
                        {
                            player?.setPosPlayer();
                            Console.Clear();

                            Extensions.sentPlayer(client, player);
                            gameField = Extensions.getGameField(client);

                            if(gameField?.status == "error")
                            {
                                Console.WriteLine($"{player?.pointX}-{player?.pointY} is occupied");
                                gameField.showField();
                                continue;
                            }
                            break;
                        }

                        gameField = Extensions.getGameField(client);
                        gameField?.showField();
                    }
                }
                catch (SocketException se)
                {
                    Console.WriteLine($"{se.ErrorCode} - {se.Message}");
                    Console.Read();
                }
                finally
                {
                    Console.Read();
                    client?.Close();
                    Console.WriteLine($"client | player {player?.userName}  Stop!");
                }             
            }
        }
    }
}