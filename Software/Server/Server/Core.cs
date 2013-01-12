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
        /// Элемент класса для принятия экстренных решение
        /// </summary>
        private ExtremDecisionmaking ExDM = new ExtremDecisionmaking();
        /// <summary>
        /// Ком порт исполнитель
        /// </summary>
        private ComPortExecutable ExeComPort;
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
            //Получаем команду для порта исполнителя
            byte[] ProcComand = ExDM.ParserComand(comand);
            if (ProcComand[0] != 0)
            {
                // Флаг который означает что КомПорт блокирован
                bool podflag = false;
                //Процедура отправки команды через ком порт исполнитель с соблюдением 
                //синхронизации потоков
                while (!podflag)
                {
                    lock (Storage.lockerComPort)
                    {
                        if (Storage.flagComPort)
                        {
                            podflag = true;
                            Storage.flagComPort = false;
                            ProcComand = ExeComPort.SendInform(ProcComand);
                            Storage.flagComPort = true;
                        }
                    }
                    //Что бы оптимизировать работу службы ожидание освобождения ком порта исполнителя 
                    //поток засыпает на 300 мс
                    if (!podflag)
                    {
                        Thread.Sleep(300);
                    }
                }
            }

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
