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

                UserPlayerA? playerA = null;
                UserPlayerA? playerB = null;
                string msg = string.Empty;

                try
                {
                    while (true)
                    {
                        msg = Extensions.getMsg(clientA);
                        Extensions.sendMsg(clientB, msg);
                        msg = Extensions.getMsg(clientB);
                        Extensions.sendMsg(clientA, msg);
                    }

                    //playerA = Extensions.getMsgPlayerA(clientA);
                    //playerA.status = "update";

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