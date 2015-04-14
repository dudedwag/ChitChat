using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net.Sockets;
using System.Threading;

namespace ChatClient
{
    public class ThreadedReader
    {
        Thread theThread;
        TcpClient client;
        StreamReader sr;
        public bool isAlive;

        public ThreadedReader(TcpClient c)
        {
            client = c;
            sr = new StreamReader(client.GetStream());
            theThread = new Thread(new ThreadStart(ReadFromSocket));
            isAlive = true;
            theThread.Start();
        }
        public void ReadFromSocket()
        {
            try
            {
                string data = "";
                while (true)
                {
                    data = sr.ReadLine();
                    if (data != "")
                    {
                        Console.WriteLine(data);
                    }
                }
            }
            catch (Exception e)
            {
                isAlive = false;
            }
        }
    }
}
