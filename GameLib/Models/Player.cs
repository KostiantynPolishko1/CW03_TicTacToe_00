using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLib.Models
{
    public class Player
    {
        public int pointX { get; set; } = default;
        public int pointY { get; set; } = default;
        public int stepGame { get; set; } = default;

        public Player() { }
        public Player(int stepGame) => this.stepGame = stepGame;
    }
}
