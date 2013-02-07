using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace SimpleAsyncServer
{
    class Program
    {

        static SimpleAsyncServer srv;

        static void Main(string[] args)
        {
            srv = new SimpleAsyncServer("TestServer", new IPEndPoint(IPAddress.Parse("127.0.0.1"), 5000));
            srv.IOBufferLength = 1024;
            srv.IOTimeout = 250;
            srv.ServerEventsEvent += new EventRepresentationDelegate(srv1_ServerEventsEvent);
            srv.ClientsDataReceiveEvent += new IODataDelegate(srv1_ClientsDataReceiveEvent);
            srv.Start();

            while (true)
            {
                byte[] s = Encoding.ASCII.GetBytes(Console.ReadLine());
                srv.SendData(s, 0, s.Length);
            }
        }

        static void srv1_ClientsDataReceiveEvent(byte[] data, int pos, int len)
        {
            Console.WriteLine(string.Format("{0} = >>>{1}<<<", "DATA", Encoding.ASCII.GetString(data, pos, len)));
        }

        static void srv1_ServerEventsEvent(EventRepresentetionType eventType, string eventStr)
        {
            Console.WriteLine(eventType.ToString() + " = " + eventStr + "\n");
        }

    
    }
}
