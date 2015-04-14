using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net.Sockets;
using System.Threading;

namespace Chat
{
    public class ThreadedReader
    {
        Thread theThread;
        TcpClient client;
        StreamReader sr;
        StreamWriter sw;
        Server parent;
        public bool isAlive;

        public ThreadedReader(TcpClient c, Server theServer)
        {
            client = c;
            parent = theServer;
            sr = new StreamReader(client.GetStream());
            sw = new StreamWriter(client.GetStream());
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
                        DateTime _currentTime = new DateTime();
                        _currentTime = DateTime.Now;
                        Console.WriteLine("[" + _currentTime.Hour + ":" + _currentTime.Minute + "] " + data);
                        foreach (ThreadedReader t in parent.readerList)
                        {
                            t.WriteMessageToSocket(data);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                isAlive = false;
            }
        }

        public void WriteMessageToSocket(string message)
        {
            sw.WriteLine(message);
            sw.Flush();
        }
    }
}
