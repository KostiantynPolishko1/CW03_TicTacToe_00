using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLib
{
    public class UserPlayerA : Player
    {
        public char type { get; } = 'X';
        public string userName { get; } = string.Empty;
        public string status { get; set; } = string.Empty;

        public UserPlayerA() : base(5) { }

        public UserPlayerA(in string userName) : this() => this.userName = userName;
    }
}
