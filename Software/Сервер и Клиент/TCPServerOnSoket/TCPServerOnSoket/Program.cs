using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите порт для прослушки:");
            AsynchronousIoServer ts = new AsynchronousIoServer(Convert.ToInt32(Console.ReadLine()));
            ts.Start();
            Console.WriteLine("Ожидание клиентов:");
            Console.ReadLine();
        }
    }
}
