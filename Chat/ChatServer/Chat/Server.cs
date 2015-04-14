using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net.Sockets;
using System.Threading;
using System.Net;

namespace Chat
{
    public class Server
    {
        TcpListener listener;
        TcpClient client;
        public List<ThreadedReader> readerList;
        public Server(int port)
        {
            listener = new TcpListener(IPAddress.Parse(GetIp()), port);
            readerList = new List<ThreadedReader>();
            StartListening();
        }
        public void StartListening()
        {
            listener.Start();
            while (true)
            {
                try
                {
                    Console.WriteLine("Waiting for a connection at: "+ listener.LocalEndpoint);
                    client = listener.AcceptTcpClient();
                    Console.WriteLine("Someone connected: " + client.Client.LocalEndPoint);
                    ThreadedReader tempReader = new ThreadedReader(client, this);
                    readerList.Add(tempReader);
                    for (int i = readerList.Count - 1; i >= 0; i--)
                    {
                        ThreadedReader temp2 = readerList[i];
                        if (!temp2.isAlive)
                        {
                            readerList.Remove(temp2);
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Something went wrong. . .");
                }
            }
        }
        public string GetIp()
        {
            IPHostEntry host;
            string localIp = "?";
            host = Dns.GetHostEntry(Dns.GetHostName());
            foreach(IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily== AddressFamily.InterNetwork)
                {
                    localIp = ip.ToString();
                }
            }            
            return localIp;
        }
    }
}
