using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLib
{
    public class UserPlayerB : Player
    {
        public char type { get; } = 'O';
        public string userName { get; } = string.Empty;
        public string status { get; set; } = string.Empty;

        public UserPlayerB() : base(4) { }

        public UserPlayerB(in string userName) : this() => this.userName = userName;
    }
}
