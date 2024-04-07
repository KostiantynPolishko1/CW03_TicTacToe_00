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
                        Extensions.setPosPlayer(playerA);
                        Console.Clear();
                        gameField.showField();

                        Extensions.sentPlayer(clientA, playerA);

                        Console.WriteLine($"wait answer {playerB?.userName}");
                        playerB = Extensions.getPlayerB(clientA);
                        Console.Clear();

                        gameField.showField();
                        Console.WriteLine($"answer {playerB?.userName}: x{playerB?.pointX} - y{playerB?.pointY}");
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
                    Console.WriteLine($"clientA stop");
                }
            }                
        }
    }
}