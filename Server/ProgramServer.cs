using GameLib;
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

            using(SocketServer server = new SocketServer(new ConnectIPEndP().getIpeP()))
            {

                if (Extensions.getClient(server, out Socket? clientA)) { Console.WriteLine($"server connect clientA {clientA?.RemoteEndPoint}"); }                
                if (Extensions.getClient(server, out Socket? clientB)) { Console.WriteLine($"server connect clientB {clientB?.RemoteEndPoint}"); }

                UserPlayerA? playerA = Extensions.getPlayerA(clientA);
                Extensions.sentPlayer(clientB, playerA);

                UserPlayerB? playerB = Extensions.getPlayerB(clientB);
                Extensions.sentPlayer(clientA, playerB);

                GameField gameField = new GameField();

                try
                {
                    while (true)
                    {
                        while (true)
                        {
                            playerA = Extensions.getPlayerA(clientA);

                            server.checkField(gameField, playerA);
                            Extensions.sentPlayer(clientA, playerA);

                            if(playerA?.status == "error") { continue; }
                            break;
                        }

                        Extensions.sentPlayer(clientB, playerA);

                        while (true)
                        {
                            playerB = Extensions.getPlayerB(clientB);

                            server.checkField(gameField, playerB);
                            Extensions.sentPlayer(clientB, playerB);

                            if(playerB?.status == "error") { continue; }                            
                            break;
                        }

                        Extensions.sentPlayer(clientA, playerB);
                    }

                }
                catch (SocketException se)
                {
                    Console.WriteLine($"{se.ErrorCode} - {se.Message}");
                }
                finally
                {
                    Console.Read();
                    clientA?.Close();
                    clientB?.Close();
                    Console.WriteLine("Server Stop!");
                }
            }
        }
    }
}