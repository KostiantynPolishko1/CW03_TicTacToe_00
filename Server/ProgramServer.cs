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

                server.fillClients(SIZE, clients);

                foreach (var client in clients) { Console.WriteLine($"server connect client {client?.RemoteEndPoint}"); }
                List<UserPlayer?> players = new List<UserPlayer?>() { new UserPlayer('X'), new UserPlayer('O') };

                for (int i = 0; i != SIZE; i++) 
                { 
                    Extensions.sentPlayer(clients[i], players[i]);
                    players[i] = Extensions.getPlayer(clients[i]);
                    Extensions.sentGameField(clients[i], gameField);
                }

                try
                {
                    while (true)
                    {
                        for(int i = 0; i != SIZE; i++)
                        {
                            players[i] = Extensions.getPlayer(clients[i]);
                            Extensions.sentGameField(clients[i], gameField);
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