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

                try
                {

                }
                catch (SocketException se)
                {
                    Console.WriteLine($"{se.ErrorCode} - {se.Message}");

                }
                finally
                {
                    clientB.Close();
                    Console.WriteLine($"clientB stop");
                }
            }

            Console.Read();            
        }
    }
}