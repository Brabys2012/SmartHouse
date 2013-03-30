using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Net.Sockets;
using System.Net;
using System.Data;

namespace Server
{
    class AsServer
    {
        /// <summary>
        /// Поток, в котором происходит проверка активности клиентов.
        /// </summary>
        private Thread _clientChecker;

        /// <summary>
        /// Поток, в котором проверяется очередь сообщений для пользователей;
        /// </summary>
        private Thread _QueueChecker;

        /// <summary>
        /// Поток в котором отправляются данные о текущей конфигурации системы.
        /// </summary>
        private Thread _Updater;

        TableUser TabUser;

        private Socket _serverSocket;
        private int _port;

        public AsServer(int port) { _port = port; }

        /// <summary>
        /// Данный тип хранит информацию о клиенте.
        /// </summary>
        private class Client
        {
            /// <summary>
            /// Сокет клиента.
            /// </summary>
            public Socket Socket;
            /// <summary>
            /// Буфер для приёма передачи данных.
            /// </summary>
            public byte[] Buffer;
            /// <summary>
            /// Количество сделанных попыток подключения.
            /// </summary>
            public int AuthCount;
            /// <summary>
            /// Указывает авторизован ли клиент.
            /// </summary>
            public bool IsAuth;
            /// <summary>
            /// Время подключения клиента.
            /// </summary>
            public DateTime ConnDate = new DateTime();
            /// <summary>
            /// Указывает текущее состояние клиента.  
            /// </summary>
            public bool IsActive;
            /// <summary>
            /// логин 
            /// </summary>
            public string login = "";
            /// <summary>
            /// пароль
            /// </summary>
            public string password = "";
        }

        //Лист содержит информацию о всех активных подключениях.
        private List<Client> _clients = new List<Client>();

        /// <summary>
        /// Запускает на выполнение сервер.
        /// </summary>
        /// <exception cref="Exception"></exception>
        public void Start()
        {
            // Запускаем поток клиентской проверки
            _clientChecker = new Thread(new ThreadStart(ClientCheckerThread));
            _clientChecker.IsBackground = true;
            _clientChecker.Start();

            //Запускаем поток для проверки наличия сообщений для отправки в очереди.
            _QueueChecker = new Thread(new ThreadStart(QueueCheckerThread));
            _QueueChecker.IsBackground = true;
            _QueueChecker.Start();

            //Объект для работы с таблицей USER базы данных
            TabUser = new TableUser();

            // Получаем информацию о локальном компьютере
            IPHostEntry localMachineInfo = Dns.GetHostEntry(Dns.GetHostName());
            IPEndPoint myEndpoint = new IPEndPoint(localMachineInfo.AddressList[2], _port);
            Console.WriteLine("IP - {0}", localMachineInfo.AddressList[2].ToString());
            // Создаем сокет, привязываем его к адресу и начинаем прослушивание
            _serverSocket = new Socket(myEndpoint.Address.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            _serverSocket.Bind(myEndpoint);
            _serverSocket.Listen((int)SocketOptionName.MaxConnections);
            // Начинаем прослушивание
            for (int i = 0; i < 10; i++)
                _serverSocket.BeginAccept(new
                    AsyncCallback(AcceptCallback), _serverSocket);
            WinLog.Write("Сервер успешно запущен. Порт - " + _port.ToString() + ", IP - "
                          + localMachineInfo.AddressList[2].ToString(),
                          System.Diagnostics.EventLogEntryType.Information);
        }


        /// <summary>
        /// Поток в котором проверяется наличие данных в очереди на отправку клиенту
        /// </summary>
        private void QueueCheckerThread()
        {
            string data = "";
            string[] DataToSend = null;
            while (true)
            {
                lock (Storage.MessegesForUser)
                {
                    while (Storage.MessegesForUser.Count != 0)
                    {
                        data = (string)Storage.MessegesForUser.Dequeue();
                        DataToSend = data.Split('@');
                        Send(DataToSend[1], DataToSend[0], true);
                        data = "";
                        DataToSend = null;
                    }
                }
                Thread.Sleep(30000);
            }
        }

        /// <summary>
        /// Поток, в котором выполняется проверка клиентов.
        /// </summary>
        private void ClientCheckerThread()
        {
            try
            {
                // Входим в бесконечный цикл
                while (true)
                {
                    int i = 0;
                    lock (_clients)
                    {
                        while (i < _clients.Count)
                        {
                            try
                            {
                                if (!_clients[i].IsAuth && (_clients[i].ConnDate <= DateTime.Now))
                                {
                                    CloseConnection(_clients[i]);
                                    i--;
                                }
                                else
                                {
                                    i++;
                                }
                            }
                            catch (Exception)
                            {

                            }
                        }
                    }
                    // Усыпляем поток
                    Thread.Sleep(10000);
                }
            }
            catch (Exception) { }
        }

        /// <summary>
        /// Функция обратного вызова для приёма подключения.
        /// </summary>
        /// <param name="result"></param>
        private void AcceptCallback(IAsyncResult result)
        {
            Client aceptClient = new Client();
            try
            {
                // Завершение операции Accept
                Socket s = (Socket)result.AsyncState;
                aceptClient.Socket = s.EndAccept(result);
                aceptClient.Buffer = new byte[255];

                //Добавление нового клиента в общий список. 
                lock (_clients) _clients.Add(aceptClient);

                // Установка даты подключения
                aceptClient.ConnDate = DateTime.Now.AddSeconds(60);
                aceptClient.IsActive = true;

                // Начало операции авторизации 
                aceptClient.Socket.BeginReceive(aceptClient.Buffer,
                    0, aceptClient.Buffer.Length, SocketFlags.None,
                    new AsyncCallback(AuthCallback),
                    aceptClient);
                WinLog.Write("Клиент с IP - " + aceptClient.Socket.RemoteEndPoint + " запросил авторизацию",
                             System.Diagnostics.EventLogEntryType.Information);

                Send(aceptClient, "Auth?", true);

                //Начало новой операции приёма подключения
                _serverSocket.BeginAccept(new AsyncCallback(
                    AcceptCallback), result.AsyncState);
            }
            catch (SocketException exc)
            {
                WinLog.Write(exc.Message, System.Diagnostics.EventLogEntryType.Error);
                CloseConnection(aceptClient);
                Console.WriteLine("Socket exception: " +
                    exc.SocketErrorCode + exc.Message);
            }
            catch (Exception exc)
            {
                WinLog.Write(exc.Message, System.Diagnostics.EventLogEntryType.Error);
                CloseConnection(aceptClient);
                Console.WriteLine("Exception: " + exc.Message);
            }
        }

        /// <summary>
        /// Функция обратного вызова для авторизации.
        /// </summary>
        /// <param name="result"></param>
        private void AuthCallback(IAsyncResult result)
        {
            Client authClient = (Client)result.AsyncState;
            string role = "";
            try
            {
                if (authClient.AuthCount <= 2)
                {
                    if (authClient.IsActive)
                    {
                        string[] authData = null;
                        int count = 0;
                        count = authClient.Socket.EndReceive(result);
                        authData = Encoding.ASCII.GetString(authClient.Buffer, 0, count).Split('.');
                        role = TabUser.CheckUser(authData[0], authData[1]);
                        //TODO создать метод обращающийся к базе для проверки логина и пароля
                        if (role != "")
                        {
                            authClient.IsAuth = true;
                            authClient.IsActive = true;
                            authClient.login = authData[0];
                            WinLog.Write("Клиент " + authClient.login + " авторизован",
                                         System.Diagnostics.EventLogEntryType.SuccessAudit);
                            Send(authClient, "AuthAnsver/1*" + role + "?", true);
                            authClient.Socket.BeginReceive(authClient.Buffer,
                                0, authClient.Buffer.Length, SocketFlags.None,
                                new AsyncCallback(ReceiveCallback),
                                authClient);
                        }
                        else
                        {
                            Send(authClient, "AuthAnsver/0/" + role + "?", true);
                            authClient.IsAuth = false;
                            // Начало операции авторизации 
                            authClient.Socket.BeginReceive(authClient.Buffer,
                                0, authClient.Buffer.Length, SocketFlags.None,
                                new AsyncCallback(AuthCallback),
                                authClient);
                            authClient.AuthCount++;
                            WinLog.Write("Ошибка авторизации клиента с IP - " + authClient.Socket.RemoteEndPoint,
                                         System.Diagnostics.EventLogEntryType.FailureAudit);
                            Send(authClient, "Wrong login or password.The number of attempts" + Convert.ToString(3 - authClient.AuthCount), true);
                        }
                    }
                }
                else
                {
                    Send(authClient, "Превышено количество попыток подключения", true);
                    CloseConnection(authClient);
                }
            }
            catch (Exception ex)
            {
                WinLog.Write(ex.Message, System.Diagnostics.EventLogEntryType.Error);
                Console.WriteLine("Ошибка {0}", ex.Message);
            }
        }

        /// <summary>
        /// Функция обратного вызова для принятия данных от клиента.
        /// </summary>
        /// <param name="result"></param>
        private void ReceiveCallback(IAsyncResult result)
        {
            string messData = "";
            string[] Data = null;
            int ReadedByte = 0;
            Client processClient = (Client)result.AsyncState;
            ReadedByte = processClient.Socket.EndReceive(result);
            try
            {
                messData = Crypto.Decrypt(Encoding.ASCII.GetString(processClient.Buffer, 0, ReadedByte));
                Data = messData.Split('/');

                switch (Data[0])
                { 
                    case "Exit":
                        CloseConnection(processClient);
                        processClient.IsActive = false;
                        break;
                    case "GetUpdate":
                        _Updater = new Thread(delegate() { UpdateData(processClient.login); });
                        _Updater.IsBackground = true;
                        _Updater.Start();
                        break;
                    case "GetCounterRec":
                    case "AddUser":
                    case "AddDev":
                        Storage.QueueTCP.Enqueue(messData + "/" + processClient.login);
                        break;
                    default:
                        Send(processClient.login, "Incorrect command format!", true);
                        break;
                }

                if (processClient.IsActive)
                {
                    processClient.Socket.BeginReceive(processClient.Buffer,
                     0, processClient.Buffer.Length, SocketFlags.None,
                     new AsyncCallback(ReceiveCallback),
                     processClient);
                }
            }
            catch (SocketException exc)
            {
                WinLog.Write(exc.Message, System.Diagnostics.EventLogEntryType.Error);
                CloseConnection(processClient);
                Console.WriteLine("Socket exception: " +
                    exc.SocketErrorCode);
            }
            catch (Exception exc)
            {
                WinLog.Write(exc.Message, System.Diagnostics.EventLogEntryType.Error);
                CloseConnection(processClient);
                Console.WriteLine("Exception: " + exc);
            }
        }

        /// <summary>
        /// Отправка данных определённому клиенту, определяемого по ссылке из списка клиентов.
        /// </summary>
        /// <param name="client">Ссылка на клиента</param>
        /// <param name="data">данные для отправки</param>
        /// <param name="NeedEncrypt">флаг необходимости шифрования</param>
        private void Send(Client client, String data, bool NeedEncrypt)
        {
            try
            {
                byte[] byteData = Encoding.ASCII.GetBytes(data);
                if (NeedEncrypt)
                {
                    byteData = Encoding.ASCII.GetBytes(Crypto.Encrypt(data));
                }
                else
                {
                    byteData = Encoding.ASCII.GetBytes(data);
                }


                // Начинаем отправку данных.
                client.Socket.BeginSend(byteData, 0, byteData.Length, 0,
                  new AsyncCallback(SendCallback), client);
            }
            catch (Exception ex)
            {
                WinLog.Write(ex.Message, System.Diagnostics.EventLogEntryType.Error);
                Console.WriteLine("Ошибка: {0}", ex.Message);
            }
        }

        /// <summary>
        /// Отправляет данные определённому клиенту, определяемого логином.
        /// </summary>
        /// <param name="ClLogin">логин клиента</param>
        /// <param name="data">данные для отправки</param>
        /// <param name="NeedEncrypt">необходимость шифрования</param>
        private void Send(string ClLogin, String data, bool NeedEncrypt)
        {
            try
            {
                byte[] byteData = Encoding.ASCII.GetBytes(data);
                if (NeedEncrypt)
                {
                    byteData = Encoding.ASCII.GetBytes(Crypto.Encrypt(data));
                }
                else
                {
                    byteData = Encoding.ASCII.GetBytes(data);
                }

                foreach (var cl in _clients)
                {
                    if (cl.login == ClLogin)
                    {
                        // Начинаем отправку данных.
                        cl.Socket.BeginSend(byteData, 0, byteData.Length, 0,
                          new AsyncCallback(SendCallback), cl);
                    }
                }
            }
            catch (Exception ex)
            {
                WinLog.Write(ex.Message, System.Diagnostics.EventLogEntryType.Error);
                Console.WriteLine("Ошибка: {0}", ex.Message);
            }
        }

        /// <summary>
        /// Метод для отправки данных всем клиентам.
        /// </summary>
        /// <param name="data">данные которые необходимо отправить.</param>
        public void Send(String data)
        {
            int i = 0;
            try
            {
                lock (_clients)
                {
                    for (i = 0; i < _clients.Count; i++)
                    {

                    }
                }

            }
            catch (Exception ex)
            {
                //????Необходимо закрыть подключение которое вызвало исключение?????.
                //CloseConnection((Client)_clients[i]);
                WinLog.Write(ex.Message, System.Diagnostics.EventLogEntryType.Error);
                Console.WriteLine("Ошибка: {0}", ex.Message);
            }
        }

        /// <summary>
        /// Функция обратного вызова для отправки сообщения клиенту.
        /// </summary>
        /// <param name="result"></param>
        private static void SendCallback(IAsyncResult result)
        {
            // Определяем клиента.
            Client client = (Client)result.AsyncState;
            try
            {

                // Завершаем отправку данных клиенту.
                int bytesSent = client.Socket.EndSend(result);
                Console.WriteLine("Отправлено {0} байтов, клиенту с IP - {1}.", bytesSent, client.Socket.RemoteEndPoint);

            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка: {0}", ex.Message);
            }
        }


        /// <summary>
        /// закрывает подключение и удаляет клиента из списка клиентов.
        /// </summary>
        /// <param name="cl">ссылка на клиента.</param>
        private void CloseConnection(Client cl)
        {
            cl.Socket.Close();
            cl.IsActive = false;
            lock (_clients) _clients.Remove(cl);
        }

        /// <summary>
        /// Поток отправляет данные клиенту для построения списка устройств. 
        /// </summary>
        /// <param name="login">логин клиента которому необходимо отправить данные</param>
        private void UpdateData(string login)
        {
            string CommandString = "Update/";
            foreach (DataRow row in Storage.ArrayUpdate.Tables[0].Rows)
            {
                foreach (DataColumn col in Storage.ArrayUpdate.Tables[0].Columns)
                {
                    if (!(col.Ordinal == 0))
                    CommandString += "*" + row[col].ToString();
                    else CommandString += row[col].ToString();
                }
                CommandString += "?";
                Send(login, CommandString, true);
                CommandString = "Update/";
            }
            //TODO как закрыть поток?
        }
    }
}
