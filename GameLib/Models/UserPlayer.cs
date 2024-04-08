using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLib.Models
{
    public class UserPlayer : Player
    {
        public char type { get; set; } = default;
        public string userName { get; set; } = "unknown";
        public static Dictionary<string, int> fieldData { get; }

        static UserPlayer()
        {
            fieldData = new Dictionary<string, int>()
            {
                { "row", GameField.row},
                { "col", GameField.col}
            };
        }

        public UserPlayer() : base() { }

        public UserPlayer(char type) : base()
        {
            this.type = type;
        }

        public void setUserName(char type)
        {
            Console.Write($"player {type} enter your name: ");
            this.userName = Console.ReadLine();
            Console.Clear();
        }

        public void setPosPlayer()
        {

            for (int i = 0; i != fieldData?.Count;)
            {
                if (!int.TryParse(Console.ReadLine(), out int pos))
                {
                    msgError("!Value is not digit!");
                    continue;
                }

                if (Math.Abs(pos) >= fieldData?.Values.ToArray()[i])
                {
                    msgError($"!Value is out {fieldData?.Keys.ToArray()[i]} {0} : {fieldData?.Values.ToArray()[i] - 1}!");
                    continue;
                }

                if (i == 0) { this.pointX = Math.Abs(pos); }
                else { this.pointY = Math.Abs(pos); }
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
    }
}
