using GameLib;
using System.Net.Sockets;

namespace ClientB
{
    internal class ProgramClientB
    {
        static void Main(string[] args)
        {
            using (SocketClient clientB = new SocketClient(new ConnectIPEndP().getIpeP()))
            {
                Console.WriteLine($"clientB connect to {clientB.RemoteEndPoint}");

                UserPlayerA? playerA = Extensions.getPlayerA(clientB);

                UserPlayerB? playerB = new UserPlayerB("Mare");
                Extensions.sentPlayer(clientB, playerB);

                GameField gameField = new GameField();

                try
                {
                    gameField.showField();

                    while (true)
                    {                      
                        Console.WriteLine($"wait answer {playerA?.userName}");
                        playerA = Extensions.getPlayerA(clientB);
                        Console.Clear();

                        gameField.field[playerA.pointX, playerA.pointY] = playerA.type;

                        gameField.showField();
                        Console.WriteLine($"answer {playerA?.userName}: x{playerA?.pointX} - y{playerA?.pointY}");

                        if (playerA?.status == "win")
                        {
                            Console.WriteLine($"client {clientB.RemoteEndPoint} you GAME OVER!");
                            break;
                        }                       

                        while (true)
                        {
                            Extensions.setPosPlayer(playerB);
                            Console.Clear();    
                            gameField.showField();

                            Extensions.sentPlayer(clientB, playerB);
                            playerB = Extensions.getPlayerB(clientB);
                            if (playerB?.status == "error") 
                            {
                                Console.Clear();
                                Console.WriteLine($"position {playerB?.pointX} - {playerB?.pointY} is occupied");
                                gameField.showField();
                                continue;
                            }

                            gameField.field[playerB.pointX, playerB.pointY] = playerB.type;
                            Console.Clear();
                            break;
                        }

                        gameField.showField();

                        if(playerB?.status == "win")
                        {
                            Console.WriteLine($"client {clientB.RemoteEndPoint} you WIN!");
                            break;
                        }

                        Extensions.sentPlayer(clientB, playerB);
                    }
                }
                catch (SocketException se)
                {
                    Console.WriteLine($"{se.ErrorCode} - {se.Message}");

                }
                finally
                {
                    Console.Read();
                    clientB.Close();
                }
            }         
        }
    }
}