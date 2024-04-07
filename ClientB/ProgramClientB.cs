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

                UserPlayerA playerB = new UserPlayerA("Mare");
                GameField field = new GameField();

                try
                {
                    while (true)
                    {
                        Console.WriteLine($"answerA: {Extensions.getMsg(clientB)}");
                        Extensions.sendMsg(clientB, null);
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