using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Runtime.Serialization;

namespace GameLib
{
    public static class Extensions
    {
        public static Dictionary<string, int> fieldData { get; }

        static Extensions()
        {
            fieldData = new Dictionary<string, int>()
            {
                { "row", GameField.row},
                { "col", GameField.col}
            };
        }

        public static bool getClient(SocketServer server, out Socket? socket)
        {
            try
            {
                socket = server.Accept();
                return true;
            }
            catch
            { 
                socket = null;
                return false;
            }            
        }

        public static void setPosPlayer(Player? player)
        {

            for(int i = 0; i != fieldData?.Count;)
            {               
                Console.Write($"{player?.userName} enter in {fieldData?.Keys.ToArray()[i]} {0} : {fieldData?.Values.ToArray()[i]-1}: ");
                if (!int.TryParse(Console.ReadLine(), out int pos)) 
                {
                    //Console.Clear();
                    msgError("!Value is not digit!");
                    continue; 
                }

                if(Math.Abs(pos) >= fieldData?.Values.ToArray()[i])
                {
                    //Console.Clear();
                    msgError($"!Value is out {fieldData?.Keys.ToArray()[i]} {0} : {fieldData?.Values.ToArray()[i] - 1}!");
                    continue;
                }

                if (i == 0) { player.pointX = Math.Abs(pos); }
                else { player.pointY = Math.Abs(pos); }
                i++;
            }
        }

        public static void msgError(in string msg)
        {
            Console.BackgroundColor = ConsoleColor.Yellow;
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine(msg);
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static byte[] sizeBytes(int size) => BitConverter.GetBytes(size);

        public static UserPlayerA? getPlayerA(Socket? client)
        {
            byte[] byteSize = new byte[4];
            client?.Receive(byteSize);

            byte[] dataPlayer = new byte[BitConverter.ToInt32(byteSize)];
            client?.Receive(dataPlayer);

            Utf8JsonReader utf8Reader = new Utf8JsonReader(dataPlayer);
            return JsonSerializer.Deserialize<UserPlayerA>(ref utf8Reader);
        }

        public static UserPlayerB? getPlayerB(Socket? client)
        {
            byte[] byteSize = new byte[4];
            client?.Receive(byteSize);

            byte[] dataPlayer = new byte[BitConverter.ToInt32(byteSize)];
            client?.Receive(dataPlayer);

            Utf8JsonReader utf8Reader = new Utf8JsonReader(dataPlayer);
            return JsonSerializer.Deserialize<UserPlayerB>(ref utf8Reader);
        }

        public static void sentPlayer(Socket? client, Player? player)
        {
            byte[] buffer = JsonSerializer.SerializeToUtf8Bytes(player);
            client?.Send(sizeBytes(Buffer.ByteLength(buffer)));
            client?.Send(buffer);
        }

        public static void sendMsg(Socket? socket, in string? clientMsg = default)
        {
            if (clientMsg != null) { socket?.Send(Encoding.Unicode.GetBytes(clientMsg)); }
            else 
            {
                Console.Write($"enter msg: ");
                socket?.Send(Encoding.Unicode.GetBytes(Console.ReadLine()));
            }
        }

        public static string getMsg(Socket? socket)
        {
            byte[] bytesMsg =  new byte[1024];
            int bytesRead = socket.Receive(bytesMsg);

            return Encoding.Unicode.GetString(bytesMsg, 0, bytesRead);
        }
    }
}
