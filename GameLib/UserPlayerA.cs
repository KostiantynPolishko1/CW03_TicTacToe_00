using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLib
{
    public class UserPlayerA : Player
    {
        public override char type { get; set; } = 'X';
        public override string userName { get; set; } = string.Empty;
        public override string status { get; set; } = string.Empty;

        public UserPlayerA() : base(5) { }

        public UserPlayerA(in string userName) : this() => this.userName = userName;
    }
}
