using GameLib;
using System.Net.Sockets;

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

                try
                {

                }
                catch (SocketException se)
                {
                    Console.WriteLine($"{se.ErrorCode} - {se.Message}");
                }
                finally
                {
                    clientA?.Close();
                    clientB?.Close();
                    Console.WriteLine("Server Stop!");
                }
            }

            Console.Read();
        }
    }
}