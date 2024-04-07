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

                        gameField.showField();
                        Console.WriteLine($"answer {playerA?.userName}: x{playerA?.pointX} - y{playerA?.pointY}");

                        Extensions.setPosPlayer(playerB);
                        Console.Clear();    
                        gameField.showField();

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
                    Console.WriteLine($"clientB stop");
                }
            }         
        }
    }
}