using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLib
{
    public class GameField
    {
        public static int row { get; }
        public static int col { get; }
        public char cell { get; } = '-';
        private char vdivider { get; set; } = '|';
        private char hdivider { get; set; } = '-';

        public char[,]? field {  get; set; } = null;

        static GameField() => row = col = 3;

        public GameField() 
        { 
            field = new char[row, col];
            createField();
        }

        private void createField()
        {
            for (int i = 0; i != row; i++) 
            { 
                for(int j = 0; j != col; j++) { field[i, j] = cell; }
            }
        }

        public void showField()
        {
            Console.WriteLine($"\n\t{fillHline(row * 4 + 1)}");
            for (int i = 0; i != row; i++)
            {
                Console.Write($"\t{vdivider} ");
                for (int j = 0; j != col; j++)
                {
                    Console.Write($"{field[i, j]} {vdivider} ");
                }
                Console.Write($"\n\t{fillHline(row * 4 + 1)}\n");
            }
            Console.WriteLine();

            string fillHline(int count) 
            {
                string hline = string.Empty;
                for (int i = 0; i != count; i++) { hline += hdivider; }
                return hline;
            }
        }
    }
}
