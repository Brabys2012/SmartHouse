using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Threading;

namespace Server
{
    class TCPServer
    {
        public TcpListener threadListener;

        public TCPServer(TcpListener lis)
        {
            threadListener = lis;
            ThreadPool.QueueUserWorkItem(new WaitCallback(HandleConnection));

        }

        /// <summary>
        /// Обработка подключённого клиента
        /// </summary>
        public void HandleConnection(object state)
        {
            //локальная переменная для хранения размера в байтах принятого сообщения
            int recv;
            //локальная переменная для хранения принятого сообщения в байтах
            byte[] data = new byte[1024];
            //локальная переменная для хранения данных авторизации
            string[] auth = new string[2];
            //локальная переменная для хранения принятого сообщения как строки
            string cldata = null;

            TcpClient client = threadListener.AcceptTcpClient();
            NetworkStream ns = client.GetStream();

            Console.WriteLine("Подключился клиент {0}", client.Client.RemoteEndPoint);


            //При подключении клиент отсылает имя и пароль которые необходимо проверить
            recv = ns.Read(data, 0, data.Length);
            cldata = Encoding.Default.GetString(data, 0, recv);
            auth = cldata.Split('.');
            //Временные меры, вдальнейшем тут необходимо использовать метод для работы
            //с БД 
            if ((auth[0] == "adm") && (auth[1] == "123"))
            {
                //Если пароль и имя правильные
                string welcome = "Авторизация прошла успешно";
                data = Encoding.Default.GetBytes(welcome);
                ns.Write(data, 0, data.Length);

                while (true)
                {
                    try
                    {
                        data = new byte[1024];
                        recv = ns.Read(data, 0, data.Length);
                        cldata = Encoding.Default.GetString(data, 0, recv);
                        //Если клиент прислал команду на отключение
                        if (cldata == "EXIT")
                        {
                            Console.WriteLine("Клиент {0} отключился", client.Client.RemoteEndPoint);
                            break;
                        }
                        lock (Storage.QueueTCP)
                        {
                            Storage.QueueTCP.Enqueue(cldata);
                        }
                        Console.WriteLine("Клиент написал: {0}", cldata);
                        // ns.Write(data, 0, recv);
                        ns.Write(Encoding.Default.GetBytes("Данные приняты"), 0, Encoding.Default.GetBytes("Данные приняты").Length);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Ошибка: {0}", e.Message);
                        break;
                    }
                }


                ns.Close();
                client.Close();
            }
            else
            {
                //если имя и пароль неверные
                string failure = "Неверное имя или пароль";
                data = Encoding.Default.GetBytes(failure);
                ns.Write(data, 0, data.Length);
                ns.Close();
                client.Close();
            }
        }

        /// <summary>
        /// Проверяет правильность логина и пароля
        /// </summary>
        public bool ControlAuth(string login, string pass)
        {
            throw new System.NotImplementedException();
        }
    }
}
