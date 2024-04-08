using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLib
{
    public abstract class Player
    {
        public int pointX { get; set; } = default;
        public int pointY { get; set; } = default;
        public int stepGame { get; set; } = default;
        public abstract char type { get; set; }
        public abstract string userName { get; set; }
        public abstract string status { get; set; }

        public Player() { }
        public Player(int stepGame) => this.stepGame = stepGame;
    }
}
