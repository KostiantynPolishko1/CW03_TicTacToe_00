using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLib.Models
{
    public class GameField
    {
        public static int row { get; }
        public static int col { get; }
        public char cell { get; } = '-';
        private char vdivider { get; set; } = '|';
        private char hdivider { get; set; } = '-';

        public char[]? field { get; set; } = null;
        public string? status { get; set; } = null;

        static GameField() => row = col = 3;

        public GameField()
        {
            field = new char[row * col];
            createField();
        }

        private void createField()
        {
            int increment = 0;
            for (int i = 0; i != row; i++)
            {
                for (int j = 0; j != row; j++)
                {
                    field[j + increment] = cell;                    
                }
                increment += col;
            }
        }

        public void showField()
        {
            int increment = 0;
            Console.WriteLine($"\n\t{fillHline(row * 4 + 1)}");

            for (int i = 0; i != row; i++)
            {
                Console.Write($"\t{vdivider} ");
                for (int j = 0; j != col; j++)
                {
                    Console.Write($"{field[j+increment]} {vdivider} ");
                }
                Console.Write($"\n\t{fillHline(row * 4 + 1)}\n");
                increment += col;
            }
            Console.WriteLine();

            string fillHline(int count)
            {
                string hline = string.Empty;
                for (int i = 0; i != count; i++) { hline += hdivider; }
                return hline;
            }
        }

        public void setCellPlayer(UserPlayer? player)
        {           
            field[ 3 * player.pointX + player.pointY] = player.type;
        }
    }
}
