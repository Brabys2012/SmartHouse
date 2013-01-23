using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Server
{

    /// <summary>
    /// Ядро, класс отвечающий за создание и правильную работы потоков связанных с командами Исполнительному компорту
    /// </summary>
    public partial class Core
    {

        /// <summary>
        /// Поток ядра.
        /// </summary>
        private Thread _coreThread;
        /// <summary>
        /// Список потоков, запущенных основным ядром как реакция на событие.
        /// </summary>
        private ArrayList _threads;

        /// <summary>
        /// Элемент класса для принятия экстренных решение
        /// </summary>
        private ExtremDecisionmaking ExDM = new ExtremDecisionmaking();
        /// <summary>
        /// Ком порт исполнитель
        /// </summary>
        private ComPortExecutable ExeComPort = new ComPortExecutable();

        /// <summary>
        /// Инициализирует экземпляр класса Core.
        /// </summary>
        public Core()
        {
            _threads = new ArrayList();
        }
        /// <summary>
        /// Запускает работу ядра.
        /// </summary>
        /// <exception cref="Exception"></exception>
        public void Start()
        {
            _coreThread = new Thread(new ThreadStart(MainCore));
            _coreThread.IsBackground = true;
            _coreThread.Priority = ThreadPriority.Highest;
            _coreThread.Start();
        }


        /// <summary>
        /// Главный метод ядра, просматривает очередь команд от слушателя и от пользователей и создает отдельный поток для каждой команды, который запускает обработку той или иной команды
        /// </summary>
        public void MainCore()
        {
            // Признак наличия записей в очереди на обработку срочных сообщений
            bool _qListQueue = false;
            // Команда из очереди
            object comandObj = null;
            while (true)
            {
                lock (Storage.QueueList)
                {
                    if (Storage.QueueList.Count != 0)
                    {
                        // Устанавливаем признак наличия команды
                        _qListQueue = true;
                        // Считываем команду
                        comandObj = Storage.QueueList.Dequeue();
                    }
                }

                if (_qListQueue)
                {
                    try
                    {
                        // Создаем и запускаем поток для обработки команды
                        Thread t = new Thread(ProcessThreadList);
                        t.IsBackground = true;
                        t.Start(comandObj);
                        // Сохраняем поток в пул потоков
                        _threads.Add(t);
                    }
                    catch (Exception exc)
                    {
                        // Сообщаем об ошибке + заносим в лог
                        WinLog.Write("Произошла ошибка: " + exc.Message, System.Diagnostics.EventLogEntryType.Error);
                    }
                }
                else
                {
                    //Проверка команд от пользователей
                    lock (Storage.QueueTCP)
                    {
                        if (Storage.QueueTCP.Count != 0)
                        {
                            //Операция извлечения команды из очереди команд слушателя
                            comandObj = Storage.QueueTCP.Dequeue();
                            //Создаем поток для обработки команды
                            Thread t = new Thread(ProcessThreadTCP);
                            t.Start(comandObj);
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
                    ExDM.ParserAnswer(ProcComand);
                }
            }

            // Удалем поток из пула потоков
            _threads.Remove(Thread.CurrentThread);
        }

        /// <summary>
        /// Обработка команд от TCP сервера
        /// </summary>
        /// <param name="comand">Команда пользователя</param>
        public void ProcessThreadTCP(object comand)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Метод через определенный интервал инициализирует обновление листа списка устройств
        /// Работает в отдельном потоке
        /// </summary>
        public void UpdateClient()
        {
            while (true)
            {
                ExDM.BdDevice.UpdateUserApp();
                Thread.Sleep(TimeSpan.FromMinutes(10)); 
            }
        }
    }
}
