using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Net.Sockets;
using System.Net;

namespace Server
{
    class AsServer
    {
        /// <summary>
        /// Поток, в котором происходит проверка активности клиентов.
        /// </summary>
        private Thread _clientChecker;

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
                        while(i < _clients.Count)
                        {
                            try
                            {
                                if (!_clients[i].IsAuth && (_clients[i].ConnDate > DateTime.Now))
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
                aceptClient.ConnDate = DateTime.Now.AddSeconds(30);
                aceptClient.IsActive = true;

                // Начало операции авторизации 
                aceptClient.Socket.BeginReceive(aceptClient.Buffer,
                    0, aceptClient.Buffer.Length, SocketFlags.None,
                    new AsyncCallback(AuthCallback),
                    aceptClient);
                WinLog.Write("Клиент с IP - " + aceptClient.Socket.RemoteEndPoint + " запросил авторизацию",
                             System.Diagnostics.EventLogEntryType.Information);

                Send(aceptClient, "ENTER_LOGIN_AND_PASSWORD",true);

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
                        //TODO создать метод обращающийся к базе для проверки логина и пароля
                        if (authData[0] == "adm" && authData[1] == "123")
                        {
                            authClient.IsAuth = true;
                            authClient.IsActive = true;
                            authClient.login = authData[0];
                            WinLog.Write("Клиент " + authClient.login + " авторизован",
                                         System.Diagnostics.EventLogEntryType.SuccessAudit);
                            Send(authClient, "successful authorization", true);
                            authClient.Socket.BeginReceive(authClient.Buffer,
                                0, authClient.Buffer.Length, SocketFlags.None,
                                new AsyncCallback(ReceiveCallback),
                                authClient);
                        }
                        else
                        {
                            Send(authClient, "authorization error", true);
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
            string[] messDate = null;
            int ReadedByte = 0;
            Client processClient = (Client)result.AsyncState;
            ReadedByte = processClient.Socket.EndReceive(result);
            try
            {

                messDate = Crypto.Decrypt(Encoding.ASCII.GetString(processClient.Buffer, 0, ReadedByte)).Split('.');

                switch (messDate[0])
                {
                    case "COMMAND":
                        {
                            //TODO поместить команду в очередь комманд.
                        }
                        break;
                    case "EXIT":
                        {
                            CloseConnection(processClient);
                            processClient.IsActive = false;
                        }
                        break;
                    default:
                        {
                            Send(processClient, "Incorrect command format!", true);
                        }
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
        /// Отправляет данные определённому клиенту.
        /// </summary>
        /// <param name="client">клиент которому необходимо отправить данные</param>
        /// <param name="data">данные для отправки</param>
        private static void Send(Client client, string login, String data, bool NeedEncrypt)
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
        /// Метод для отправки данных всем клиентам.
        /// </summary>
        /// <param name="data">данные которые необходимо отправить.</param>
        public void Send(String data)
        {
            int i = 0;
            try
            {
                for (i = 0; i < _clients.Count; i++)
                {
                    lock (_clients)
                    {
                        Send(_clients[i], data, true);
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
    }

}
