using System;
using System.Net;
using System.Threading;


namespace Server
{
    class Program
    {

        static void StartService()
        {
            SQL.Init();
            Core CoreInSer = new Core();
            AsServer Server = new AsServer(IPAddress.Any, 5000);
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
            SQL.Init();
            Core CoreInSer = new Core();

            // TODO: Настройки вынести в отдельный конфигурационный файл

            AsServer Server = new AsServer(IPAddress.Any, 5000);
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
            Console.WriteLine("Server started");
            Console.WriteLine(TableUser.CheckUser("admin", "admin"));
            Console.WriteLine("OK");
            Console.ReadLine();
        }
    }
}
