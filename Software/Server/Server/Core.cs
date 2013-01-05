using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Server
{
    /// <summary>
    /// Ядро, класс отвечающий за создание и правильную работы потоков связанных с командами Исполнительному компорту
    /// </summary>
    public class Core
    {
        /// <summary>
        /// Главный метод ядра, просматривает очередь команд от слушателя и от пользователей и создает отдельный поток для каждой команды, который запускает обработку той или иной команды
        /// </summary>
        public void MainCore()
        {
            while (true)
            {
                //Проверка команд, от порта слушателя, он проверяется первым, так как приоритетней команд от пользователя
                lock(Storage.QueueList)
                {
                    if (Storage.QueueList.Count != 0)
                    {
                        //Операция извлечения команды из очереди команд слушателя
                        object comandObj = Storage.QueueList.Dequeue();
                        //Создаем поток для обработки команды
                        Thread t = new Thread(ProcessThreadList);
                        t.Start(comandObj);
                    }
                    else
                    {
                        //Проверка команд от пользователей
                        lock (Storage.QueueTCP)
                        {
                            if (Storage.QueueTCP.Count != 0)
                            {
                                //Операция извлечения команды из очереди команд слушателя
                                object comandObj = Storage.QueueTCP.Dequeue();
                                //Создаем поток для обработки команды
                                Thread t = new Thread(ProcessThreadTCP);
                                t.Start(comandObj);
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Обработка команд от порта слушателя
        /// </summary>
        /// <param name="comand">Команда от порта слушателя</param>
        public void ProcessThreadList(object comand)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Обработка команд от TCP сервера
        /// </summary>
        /// <param name="comand">Команда пользователя</param>
        public void ProcessThreadTCP(object comand)
        {
            throw new System.NotImplementedException();
        }
    }
}
