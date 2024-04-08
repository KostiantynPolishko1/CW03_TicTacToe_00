using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLib.Models
{
    public class UserPlayerB : Player
    {
        public override char type { get; set; } = 'O';
        public override string userName { get; set; } = string.Empty;
        public override string status { get; set; } = string.Empty;

        public UserPlayerB() : base(4) { }

        public UserPlayerB(in string userName) : this() => this.userName = userName;
    }
}
