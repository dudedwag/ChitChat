using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net.Sockets;
using System.Threading;

namespace Chat
{
    class Program
    {
        static void Main(string[] args)
        {
            Server myServer = new Server(8000);
        }
    }
}
