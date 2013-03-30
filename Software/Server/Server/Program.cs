using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;


namespace Server
{
    class Program
    {

        static void StartService()
        {
            Core CoreInSer = new Core();
            AsServer Server = new AsServer(5000);
            ComPortListener ComPotrList = new ComPortListener();
            WinLog.Init();
            CoreInSer.Start();
            Thread ServerThread = new Thread(Server.Start);
            ServerThread.IsBackground = true;
            ServerThread.Start();
            Thread CPList = new Thread(ComPotrList.OpenCom);
            CPList.IsBackground = true;
            CPList.Start();
            CoreInSer.UpdateClient();
        }
        static void Main(string[] args)
        {
            Core CoreInSer = new Core();
            AsServer Server = new AsServer(5000);
            ComPortListener ComPotrList = new ComPortListener();
            WinLog.Init();
            CoreInSer.Start();
            Thread ServerThread = new Thread(Server.Start);
            ServerThread.IsBackground = true;
            ServerThread.Start();
            Thread CPList = new Thread(ComPotrList.OpenCom);
            CPList.IsBackground = true;
            CPList.Start();
            CoreInSer.UpdateClient();
            Console.ReadLine();
        }
    }
}
