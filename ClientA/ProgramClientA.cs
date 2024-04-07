using GameLib;
using System.Net.Sockets;
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

                UserPlayerA playerA = new UserPlayerA("Kopo");
                GameField field = new GameField();
                
                try
                {
                    while (true) 
                    { 
                        Extensions.sendMsg(clientA, null);
                        Console.WriteLine($"answerB: {Extensions.getMsg(clientA)}");
                    }

                    //while (true)
                    //{
                    //    Extensions.setPosPlayer(field, playerA);
                    //    byte[] buffer = JsonSerializer.SerializeToUtf8Bytes(playerA);
                    //
                    //    clientA.Send(Extensions.sizeBytes(Buffer.ByteLength(buffer)));
                    //    clientA.Send(buffer);
                    //
                    //    clientA.Receive(bytes);
                    //}


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