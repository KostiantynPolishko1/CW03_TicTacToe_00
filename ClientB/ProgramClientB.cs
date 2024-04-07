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

                UserPlayerA? playerA = null;
                UserPlayerB? playerB = new UserPlayerB("Mare");
                GameField gameField = new GameField();

                try
                {
                    gameField.showField();

                    while (true)
                    {                      
                        Console.WriteLine("wait answer clientA");
                        playerA = Extensions.getPlayerA(clientB);
                        Console.Clear();

                        gameField.showField();
                        Console.WriteLine($"answer {playerA?.userName}: x{playerA?.pointX} - y{playerA?.pointY}");

                        Extensions.setPosPlayer(playerB);
                        Console.Clear();    
                        gameField.showField();

                        Extensions.sentPlayerBtoA(clientB, playerB);
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