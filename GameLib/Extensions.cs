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
    }
}
