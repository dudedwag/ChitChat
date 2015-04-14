using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net.Sockets;
using System.Threading;

namespace ChatClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Host IP Adress?");
            string hostIp = Console.ReadLine();

            TcpClient client = new TcpClient(hostIp, 8000);
            StreamReader sr = new StreamReader(client.GetStream());
            StreamWriter sw = new StreamWriter(client.GetStream());
            ThreadedReader myReader = new ThreadedReader(client);

            string userInput = Console.ReadLine();
            while (userInput != "quit")
            {
                sw.WriteLine(userInput);
                sw.Flush();
                userInput = Console.ReadLine();
            }
            client.Close();
        }
    }
}
