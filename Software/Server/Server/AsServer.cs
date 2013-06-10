using System;
using System.Collections.Generic;
using System.Data;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Collections;

namespace Server
{

    /// <summary>
    /// Класс представляющий асинхронный TCP сервер.
    /// </summary>
    public class AsServer
    {
        /// <summary>
        /// Флаг определяющий необходимость шифрования.
        /// </summary>
        private bool _NeedEncrypt;

        Queue MessageQ = new Queue();

        class ChatSettings
        {
            // количество сообщение в истории чата.
            public int count = 0;
        }

        public class Message
        {
          public  string _text = "";
          public  DateTime _date = DateTime.Now;
          public  string _sender = "";
        }

        ChatSettings ChSett = new ChatSettings();
        Message mess = new Message();
        string[] HistMess = new string[10];

        /// <summary>
        /// Класс, предоставляющий информацию о клиенте.
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
            public int AuthCount = 0;
            /// <summary>
            /// Указывает авторизован ли клиент.
            /// </summary>
            public bool IsAuth = false;
            /// <summary>
            /// Время подключения клиента.
            /// </summary>
            public DateTime ConnDate = new DateTime();
            /// <summary>
            /// Указывает текущее состояние клиента.  
            /// </summary>
            public bool IsActive = false;
            /// <summary>
            /// Логин клиента.
            /// </summary>
            public string login = "";
            /// <summary>
            /// Пароль клиента.
            /// </summary>
            public string password = "";
        }

        /// <summary>
        /// Поток, в котором происходит проверка активности клиентов.
        /// </summary>
        private Thread _clientChecker;

        /// <summary>
        /// Поток, в котором проверяется очередь сообщений для пользователей.
        /// </summary>
        private Thread _QueueChecker;

        /// <summary>
        /// Поток в котором отправляются данные о текущей конфигурации системы.
        /// </summary>
        private Thread _Updater;

        /// <summary>
        /// Сокет сервера.
        /// </summary>
        private Socket _serverSocket;

        /// <summary>
        /// IP адрес сетевого интерфеса который будет прослушивать сервер.
        /// </summary>
        private IPAddress _serverAddress;

        /// <summary>
        /// Порт сервера.
        /// </summary>
        private int _serverPort;

        /// <summary>
        /// Лист содержит информацию о всех активных подключениях.
        /// </summary>
        private List<Client> _clients = new List<Client>();

        /// <summary>
        /// Инициализирует экземпляр класса AsServer.
        /// </summary>
        /// <param name="address">IP адрес сетевого интерфейса, на котором будет прослушиваться указанный порт.</param>
        /// <param name="port">Порт, который будет прослушивать сервер.</param>
        public AsServer(IPAddress address, int port)
        {
            _serverAddress = address;
            _serverPort = port;
        }

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

            // Запускаем поток для проверки наличия сообщений для отправки в очереди.
            _QueueChecker = new Thread(new ThreadStart(QueueCheckerThread));
            _QueueChecker.IsBackground = true;
            _QueueChecker.Start();

            // Создаём конечную точку
            IPEndPoint _endPoint = new IPEndPoint(_serverAddress, _serverPort);
            // Создаем сокет, привязываем его к адресу и начинаем прослушивание
            _serverSocket = new Socket(_endPoint.Address.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            _serverSocket.Bind(_endPoint);
            _serverSocket.Listen((int)SocketOptionName.MaxConnections);
            // Начинаем прослушивание
            for (int i = 0; i < 10; i++)
                _serverSocket.BeginAccept(new AsyncCallback(AcceptCallback), _serverSocket);
            Console.WriteLine("Сервер {0}:{1} успешно запущен.", _serverAddress, _serverPort);
            WinLog.Write(string.Format("Сервер {0}:{1} успешно запущен.", _serverAddress, _serverPort),
                System.Diagnostics.EventLogEntryType.Information);
            _NeedEncrypt = true;
        }

        /// <summary>
        /// Поток в котором проверяется наличие данных в очереди на отправку клиенту.
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
                        if (DataToSend[1] == "all")
                        {
                            Send(DataToSend[0]);
                            data = "";
                            DataToSend = null;
                        }
                        else
                        {
                            Send(DataToSend[1], DataToSend[0], _NeedEncrypt);
                            data = "";
                            DataToSend = null;
                        }
                        
                    }
                }
                Thread.Sleep(1000);
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
                                //Проверка времени соединения неавторизованных клиентов
                                if (!_clients[i].IsAuth && (_clients[i].ConnDate <= DateTime.Now))
                                {
                                    Send(_clients[i], @"mess\Time out autorization", _NeedEncrypt);
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
                if (aceptClient != null)
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
                    aceptClient.Socket.BeginReceive(aceptClient.Buffer, 0, aceptClient.Buffer.Length,
                        SocketFlags.None, new AsyncCallback(AuthCallback), aceptClient);
                    WinLog.Write(string.Format("Попытка авторизации клиента {0}.",
                        aceptClient.Socket.RemoteEndPoint), System.Diagnostics.EventLogEntryType.Information);
                    Send(aceptClient, "Auth", _NeedEncrypt);

                    //Начало новой операции приёма подключения
                    _serverSocket.BeginAccept(new AsyncCallback(AcceptCallback), result.AsyncState);
                }
            }
            catch (SocketException exc)
            {
                WinLog.Write(exc.Message, System.Diagnostics.EventLogEntryType.Error);
                CloseConnection(aceptClient);

                Console.WriteLine("Socket exception: " + exc.SocketErrorCode + exc.Message);
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
            try
            {
                Client authClient = (Client)result.AsyncState;
                if (authClient != null)
                {
                    string role = "";
                    if (authClient.AuthCount <= 2)
                    {
                        if (authClient.IsActive)
                        {
                            string[] authData = null;
                            string[] mess = null;
                            int count = 0;
                            count = authClient.Socket.EndReceive(result);
                            mess = Encoding.ASCII.GetString(authClient.Buffer, 0, count).Split('?');
                            authData = Crypto.Decrypt(mess[0]).Split('.');
                            role = TableUser.CheckUser(authData[0], authData[1]);
                            if (role != "")
                            {
                                authClient.IsAuth = true;
                                authClient.IsActive = true;
                                authClient.login = authData[0];
                                WinLog.Write("Клиент " + authClient.login + " авторизован",
                                             System.Diagnostics.EventLogEntryType.SuccessAudit);
                                Send(authClient, "AuthAnsver/1*" + role, _NeedEncrypt);
                                authClient.Socket.BeginReceive(authClient.Buffer,
                                    0, authClient.Buffer.Length, SocketFlags.None,
                                    new AsyncCallback(ReceiveCallback),
                                    authClient);
                            }
                            else
                            {
                                Send(authClient, "AuthAnsver/0/" + role, _NeedEncrypt);
                                authClient.IsAuth = false;
                                // Начало операции авторизации 
                                authClient.Socket.BeginReceive(authClient.Buffer,
                                    0, authClient.Buffer.Length, SocketFlags.None,
                                    new AsyncCallback(AuthCallback),
                                    authClient);
                                authClient.AuthCount++;
                                WinLog.Write("Ошибка авторизации клиента с IP - " + authClient.Socket.RemoteEndPoint,
                                             System.Diagnostics.EventLogEntryType.FailureAudit);
                                Send(authClient, @"mess/Wrong login or password.The number of attempts" + Convert.ToString(3 - authClient.AuthCount), _NeedEncrypt);
                            }
                        }
                    }
                    else
                    {
                        Send(authClient, @"mess/Превышено количество попыток подключения", _NeedEncrypt);
                        CloseConnection(authClient);
                    }
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
            //хранит принятую комманду.
            string[] messData;
            //хранит заголовок комманды.
            string[] Data = null;
            //хранит количество байтов для считывания из буфера.
            int ReadedByte = 0;

            //клиент с которым работаем.
            Client processClient = (Client)result.AsyncState;

            if (processClient != null)
            {
                try
                {
                    ReadedByte = processClient.Socket.EndReceive(result);
                    //если необходимо расшифровать,
                    if (_NeedEncrypt)
                    {
                        messData = Encoding.ASCII.GetString(processClient.Buffer, 0, ReadedByte).Split('?');
                        for (int i = 0; i < messData.Length; i++)
                        {
                            if (messData[i] != "")
                                messData[i] = Crypto.Decrypt(messData[i]);
                        }
                    }
                    else //иначе получаем команды без разшифровки.
                        messData = Encoding.ASCII.GetString(processClient.Buffer, 0, ReadedByte).Split('?');
                    //проходим по всем командам и выполняем необходимые действия.
                    for (int i = 0; i < messData.Length; i++)
                    {
                        if (messData[i] != "")
                        {
                            Data = messData[i].Split('/');
                            switch (Data[0])
                            {
                                //если необходимо отправить историю чата
                                case "GetChatHist":
                                    lock (MessageQ)
                                    {
                                        MessageQ.CopyTo(HistMess, 0);
                                        for (int k = 0; k < HistMess.Length; k++)
                                        {
                                            Send(processClient.login, HistMess[k], _NeedEncrypt);
                                        }
                                    }
                                    break;
                                //если необходимо отправить сообщение в чат.
                                case "ChatMessage":
                                    lock (MessageQ)
                                    {
                                        DateTime date = DateTime.Now;
                                        Send("(" + date + "):" + processClient + "-" + messData[1]);
                                        mess._text = processClient + "-" + messData[1];
                                        mess._date = date;
                                        mess._sender = processClient.login;
                                        lock (ChSett)
                                        {
                                            ChSett.count++;
                                            if (ChSett.count <= 10)
                                                MessageQ.Enqueue(mess);
                                            else
                                            {
                                                MessageQ.Dequeue();
                                                MessageQ.Enqueue(mess);
                                            }
                                        }
                                    }
                                    break;
                                //если необходимо отправить подтверждение подключения
                                case "AUALIVE":
                                    Send(processClient.login, "I'MALIVE", _NeedEncrypt);
                                    break;
                                //если необходимо отправить список всех on-line клиентов
                                case "GetOnLineClients":
                                    int counter = 0;
                                    string users = "";
                                    lock (_clients)
                                    {
                                        for (int j = 0; j < _clients.Count; j++)
                                        {
                                            if (counter == 6)
                                            {
                                                Send(processClient.login, users, _NeedEncrypt);
                                                users = "";
                                                counter = 0;
                                            }
                                            else
                                            {
                                                users += _clients[j].login;
                                                counter++;
                                            }
                                        }
                                    }
                                    break;
                                case "Exit":
                                    CloseConnection(processClient);
                                    processClient.IsActive = false;
                                    break;
                                //если необходимо отправить список устройств
                                case "GetUpdate":
                                    _Updater = new Thread(delegate() { UpdateData(processClient.login, messData[1]); });
                                    _Updater.IsBackground = true;
                                    _Updater.Start();
                                    break;
                                //если необходимо обновить пароль
                                case "UpdatePassword":
                                    Storage.QueueTCP.Enqueue(messData[i].Replace("LOGIN", processClient.login) + "@" + processClient.login);
                                    break;
                                //если необходимо выполнить операции в базе данных системы
                                case "GetCounterRec":
                                case "AddUser":
                                case "AddDev":
                                case "DeleteUser":
                                case "DeleteDevice":
                                    Storage.QueueTCP.Enqueue(messData[i] + "@" + processClient.login);
                                    break;
                                default:
                                    Send(processClient.login, @"mess/Incorrect command format!", _NeedEncrypt);
                                    break;
                            }
                        }
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
                byte[] byteData = null;
                if (NeedEncrypt)
                {
                    byteData = Encoding.ASCII.GetBytes(Crypto.Encrypt(data));
                }
                else
                {
                    byteData = Encoding.ASCII.GetBytes(data + "?");
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
                byte[] byteData = null;
                if (NeedEncrypt)
                {
                    byteData = Encoding.ASCII.GetBytes(Crypto.Encrypt(data));
                }
                else
                {
                    byteData = Encoding.ASCII.GetBytes(data + "?");
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
                        Send(_clients[i], data, _NeedEncrypt);
                    }
                }

            }
            catch (Exception ex)
            {
                //????Необходимо закрыть подключение которое вызвало исключение?????.
                //CloseConnection((Client)_clients[i]);
                WinLog.Write(ex.Message, System.Diagnostics.EventLogEntryType.Error);
                Console.WriteLine("Ошибка при отпрпавке сообщения: {0}", ex.Message);
            }
        }

        /// <summary>
        /// Функция обратного вызова для отправки сообщения клиенту.
        /// </summary>
        /// <param name="result"></param>
        private static void SendCallback(IAsyncResult result)
        {
            try
            {
                // Определяем клиента.
                Client client = (Client)result.AsyncState;

                // Завершаем отправку данных клиенту.
                int bytesSent = client.Socket.EndSend(result);
                Console.WriteLine("Отправлено {0} байтов, клиенту с IP - {1}.",
                    bytesSent, client.Socket.RemoteEndPoint);

            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка при отправке сообщения: {0}", ex.Message);
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
        private void UpdateData(string login, string type)
        {
            string CommandString = "Update/";
            
            foreach (DataRow row in Storage.ArrayUpdate.Tables[0].Rows)
            {
                foreach (DataColumn col in Storage.ArrayUpdate.Tables[0].Columns)
                {
                    if (row[0].ToString() == type)
                    {
                        if (col.Ordinal == 0)
                            CommandString += row[col].ToString();
                        else
                            CommandString += "*" + row[col].ToString();
                    }
                    else break;
                }
                Send(login, CommandString, _NeedEncrypt);
                CommandString = "Update/";
            }
        }
    }
}
