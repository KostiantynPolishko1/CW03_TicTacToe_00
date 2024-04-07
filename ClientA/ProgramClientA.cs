using GameLib;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text.Json;

namespace ClientA
{
    internal class ProgramClientA
    {
        static void Main(string[] args)
        {         
            using (SocketClient clientA = new SocketClient(new ConnectIPEndP().getIpeP()))
            {
                Console.WriteLine($"clientA connect to {clientA.RemoteEndPoint}");

                UserPlayerA? playerA = new UserPlayerA("Kopo");
                Extensions.sentPlayer(clientA, playerA);

                UserPlayerB? playerB = Extensions.getPlayerB(clientA);
                GameField gameField = new GameField();
                
                try
                {
                    gameField.showField();

                    while (true)
                    {   
                        while(true)
                        {
                            Extensions.setPosPlayer(playerA);
                            Console.Clear();
                            gameField.showField();

                            Extensions.sentPlayer(clientA, playerA);
                            playerA = Extensions.getPlayerA(clientA);

                            if (playerA?.status == "error") 
                            { 
                                Console.Clear();
                                Console.WriteLine($"position {playerA?.pointX} - {playerA?.pointY} is occupied");
                                gameField.showField();
                                continue; 
                            }

                            gameField.field[playerA.pointX, playerA.pointY] = playerA.type;
                            Console.Clear();
                            break;
                        }

                        gameField.showField();

                        if(playerA?.status == "win")
                        {
                            Console.Clear();
                            gameField.showField();
                            Console.WriteLine($"client {clientA.RemoteEndPoint} you WIN!");
                        }

                        Console.WriteLine($"wait answer {playerB?.userName}");

                        playerB = Extensions.getPlayerB(clientA);
                        gameField.field[playerB.pointX, playerB.pointY] = playerB.type;

                        Console.Clear();
                        gameField.showField();
                        Console.WriteLine($"answer {playerB?.userName}: x{playerB?.pointX} - y{playerB?.pointY}");

                        if (playerB?.status == "win")
                        {
                            Console.WriteLine($"client {clientA.RemoteEndPoint} you GAME OVER!");
                            break;
                        }
                    }
                }
                catch (SocketException se)
                {
                    Console.WriteLine($"{se.ErrorCode} - {se.Message}");
                }
                finally
                {
                    Console.Read();
                    clientA.Close();             
                }
            }                
        }
    }
}