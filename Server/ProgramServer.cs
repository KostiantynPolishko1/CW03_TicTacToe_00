using GameLib;
using GameLib.Models;
using System.Drawing;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;

namespace ClientA
{
    internal class ProgramServer
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Server!");            

            using (SocketServer server = new SocketServer(new ConnectIPEndP().getIpeP()))
            {
                const int SIZE = 2;
                GameField gameField = new GameField();
                List<Socket?> clients = new List<Socket?>(SIZE);
                List<UserPlayer?> players = new List<UserPlayer?>() { new UserPlayer('X'), new UserPlayer('O') };

                try
                {
                    server.fillClients(SIZE, clients);

                    foreach (var client in clients) { Console.WriteLine($"server connect client {client?.RemoteEndPoint}"); }

                    for (int i = 0; i != SIZE; i++) 
                    { 
                        Extensions.sentPlayer(clients[i], players[i]);
                    }

                    while (true)
                    {
                        for(int i = 0; i != SIZE; i++)
                        {
                            for(int j = 0; j != SIZE; j++)
                            {
                                Extensions.sentGameField(clients[j], gameField);
                            }

                            while (true)
                            {
                                players[i] = Extensions.getPlayer(clients[i]);
                                gameField?.checkCellPlayer(players[i]);
                                Extensions.sentGameField(clients[i], gameField);
                                
                                if (gameField?.status == "error")
                                {
                                    continue;
                                }

                                break;
                            }

                            gameField?.setCellPlayer(players[i]);
                        }
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
                    foreach (var client in clients) { client?.Close(); }
                    Console.WriteLine("Server Stop!");
                }
            }
        }
    }
}