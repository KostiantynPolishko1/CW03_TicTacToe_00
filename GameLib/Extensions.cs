using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

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

        public static void setPosPlayer(GameField game, Player player)
        {

            for(int i = 0; i != fieldData?.Count;)
            {
                game.showField();
                Console.Write($"{player.userName} enter in {fieldData?.Keys.ToArray()[i]} {0} : {fieldData?.Values.ToArray()[i]-1}: ");
                if (!int.TryParse(Console.ReadLine(), out int pos)) 
                {
                    Console.Clear();
                    msgError("!Value is not digit!");
                    continue; 
                }

                if(Math.Abs(pos) >= fieldData?.Values.ToArray()[i])
                {
                    Console.Clear();
                    msgError($"!Value is out {fieldData?.Keys.ToArray()[i]} {0} : {fieldData?.Values.ToArray()[i] - 1}!");
                    continue;
                }

                if (i == 0) { player.pointX = Math.Abs(pos); }
                else { player.pointY = Math.Abs(pos); }
                i++;
                Console.Clear();
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
    }
}
