using GameLib;
using System.Net.Sockets;

namespace ClientA
{
    internal class ProgramClientA
    {
        static void Main(string[] args)
        {         
            using (SocketClient clientA = new SocketClient(new ConnectIPEndP().getIpeP()))
            {
                Console.WriteLine($"clientA connect to {clientA.RemoteEndPoint}");

                try
                {
                    
                }
                catch (SocketException se)
                {
                    Console.WriteLine($"{se.ErrorCode} - {se.Message}");
                }
                finally
                {
                    clientA.Close();
                    Console.WriteLine($"clientA stop");
                }
            }

            Console.Read();            
        }
    }
}