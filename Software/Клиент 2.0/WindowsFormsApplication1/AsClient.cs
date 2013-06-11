using System;
using System.Text;
using System.Net.Sockets;
using System.Threading;
using System.Net;
using System.Collections;
using System.Text.RegularExpressions;
using System.IO;

namespace AsyncClient
{

    public delegate void IsNeedShowDelegate();
    public delegate void IsNeedShowOperationResult(string result);
    public delegate void IsNeedToChangeConfStatus(string role);
    public delegate void IsNeedShowDataDelegate(string data);
    public delegate void IsNeedToPlotDelegate(string reportData);
    public delegate void IsNeedUpdateThreeDelegate(string DevData);
    public delegate void IsNeedChangeStatus();

    public class Params
    {
        /// <summary>
        /// поле с IP адресом сервера
        /// </summary>
        public string IP = "";
        /// <summary>
        /// поле с портом сервера
        /// </summary>
        public int Port = 0;
        /// <summary>
        /// признак указывающий на 
        /// необходимость использования контроля подключения
        /// </summary>
        public bool IsNeedUseKeepAlive = false;
        /// <summary>
        /// признак указывающий на 
        /// необходимость шифрования передаваемых данных
        /// </summary>
        public bool IsNeedUseEncrypt = false;
        /// <summary>
        /// указывает какой комбинацей клавиш отправляется сообщение
        /// </summary>
        public bool SendType = false;
    }

    public class AsynchronousClient
    {
        public bool Keep_Alive = false;

        // Для проверки флага alive.
        static AutoResetEvent autoEvent;

        // Для начала отправки сообщений.
        static AutoResetEvent beginSend;

        //Указывает на активность подключения
        public bool alive = false;

        /// <summary>
        /// Поток проверяющий подключение к серверу
        /// </summary>
        private Thread KeepAlive;

        /// <summary>
        /// Объект для получения данных
        /// </summary>
        public class StateObject
        {
            // Сокет сервера.
            public Socket workSocket = null;
            // Size of receive buffer.
            public const int BufferSize = 2048;
            // Receive buffer.
            public byte[] buffer = new byte[BufferSize];
            //Активность подключения
            public bool status = false;
            //Роль пользователя
            public string role = "";
            //Необходимость шифрования
            public bool encryptIt = false;
            //Тип отправки сообщеия
            public bool SendType = false;
        }

        string reportString = "";

        public event IsNeedShowDelegate IsNeedShowLoginFormEvent;
        public event IsNeedToPlotDelegate IsNeedToPlotEvent;
        public event IsNeedUpdateThreeDelegate IsNeedUpdateThreeEvent;
        public event IsNeedChangeStatus IsNeedChangeStatusEvent;
        public event IsNeedToChangeConfStatus IsNeedToChangeConfStatusEvent;
        public event IsNeedShowOperationResult IsNeedShowOperationResultEvent;

        public StateObject _srv;

        public void StartClient(string IP, int port, bool KeepAlive_)
        {
            // Настраиваем и начинаем подключение к удалённой точке.
            try
            {
                beginSend = new AutoResetEvent(false);
                autoEvent = new AutoResetEvent(false);

                IPAddress ipAddress = IPAddress.Parse(IP);
                IPEndPoint remoteEP = new IPEndPoint(ipAddress, port);
                _srv = new StateObject();
                _srv.workSocket = new Socket(AddressFamily.InterNetwork,
                    SocketType.Stream, ProtocolType.Tcp);
                // Начинаем подключение к серверу.
                _srv.workSocket.BeginConnect(remoteEP, new AsyncCallback(ConnectCallback), _srv);
                Keep_Alive = KeepAlive_;
                if (Keep_Alive)
                {
                    // Запускаем поток проверки состояния подключения.
                    KeepAlive = new Thread(new ThreadStart(GetStatus));
                    KeepAlive.IsBackground = true;
                    KeepAlive.Start();
                }
            }
            catch (Exception e)
            {
                if (IsNeedShowOperationResultEvent != null)
                    IsNeedShowOperationResultEvent(e.Message);
            }
        }

        private void GetStatus()
        {
            beginSend.WaitOne();
            while (true)
            {
                alive = false;
                Send("AUALIVE", _srv.encryptIt);
                autoEvent.WaitOne(5000, false);
                if (!alive)
                {
                    IsNeedChangeStatusEvent();
                    break;
                }
                autoEvent.Reset();
                Thread.Sleep(5000);
            }
        }

        /// <summary>
        /// Обратная функция для подключения
        /// </summary>
        /// <param name="ar">Результат выполнения подключения(сокет)</param>
        private void ConnectCallback(IAsyncResult ar)
        {
            try
            {
                //Получаем сокет.
                StateObject client = (StateObject)ar.AsyncState;

                // Завершаем подключение.
                client.workSocket.EndConnect(ar);
                IsNeedChangeStatusEvent();
                //Начинаем получать данные от удалённой точки.
                client.workSocket.BeginReceive(client.buffer, 0,
                    StateObject.BufferSize, 0, new AsyncCallback(ReceiveCallback), client);
            }
            catch (Exception e)
            {
                if (IsNeedShowOperationResultEvent != null)
                    IsNeedShowOperationResultEvent("Ошибка при подключении: " + e.Message);
                if (IsNeedChangeStatusEvent != null)
                    IsNeedChangeStatusEvent();
            }
        }

        /// <summary>
        /// Обратная функция для обработки полученного сообщения
        /// </summary>
        /// <param name="ar">Результат выполнения подключения(сокет)</param>
        private void ReceiveCallback(IAsyncResult ar)
        {
            try
            {
                //Получаем сокет.
                StateObject state = (StateObject)ar.AsyncState;
                Socket client = state.workSocket;

                // Считываем данные и отправляем их на обработку.
                int bytesRead = client.EndReceive(ar);
                if (bytesRead > 0)
                {
                    Parser(bytesRead, _srv.encryptIt);
                }
                //Запускаем новую функцию приёма данных.
                client.BeginReceive(state.buffer, 0,
                    StateObject.BufferSize, 0, new AsyncCallback(ReceiveCallback), state);

            }
            catch (Exception e)
            {
                if (IsNeedShowOperationResultEvent != null)
                    IsNeedShowOperationResultEvent("Ошибка при получении сообщения: " + e.Message);
                if (IsNeedChangeStatusEvent != null)
                    IsNeedChangeStatusEvent();
            }
        }

        private void Parser(int bytesRead, bool needToEncrypt)
        {
            string sMessage = "";
            string[] message;
            string[] tmpString;
            if (_srv.encryptIt)
            {
                message = Encoding.ASCII.GetString(_srv.buffer, 0, bytesRead).Split('?');
                for (int i = 0; i < message.Length; i++)
                {
                    if (message[i] != "")
                        message[i] = Crypto.Decrypt(message[i]);
                }
            }
            else
                message = Encoding.ASCII.GetString(_srv.buffer, 0, bytesRead).Split('?');

            for (int i = 0; i < message.Length; i++)
            {
                string[] command = message[i].Split('/');
                if (command[0] != "")
                {
                    switch (command[0])
                    {
                        case "I'MALIVE":
                            alive = true;
                            autoEvent.Set();
                            break;
                        case "AuthAnsver":
                            string[] tmpArray;
                            tmpArray = command[1].Split('*');
                            if (tmpArray[0] == "1")
                            {
                                lock (_srv.role)
                                    _srv.role = tmpArray[1];
                                IsNeedToChangeConfStatusEvent(tmpArray[1]);
                                beginSend.Set();
                            }
                            else
                            {
                                IsNeedShowOperationResultEvent("Неверный логин или пароль");
                                _srv.role = "";
                            }
                            IsNeedToChangeConfStatusEvent(_srv.role);
                            break;
                        case "Auth":
                            // Генерируем событие необходимости отобразить форму ввода логина/пароля
                            if (IsNeedShowLoginFormEvent != null)
                                IsNeedShowLoginFormEvent();
                            break;
                        case "mess":
                            if (IsNeedShowOperationResultEvent != null)
                                IsNeedShowOperationResultEvent(command[1]);
                            break;
                        case "SetCounterRec":
                            tmpString = command[1].Split('^');
                            if (tmpString[1] == "END")
                            {
                                reportString += tmpString[0];
                                IsNeedToPlotEvent(reportString);
                                reportString = "";
                            }
                            else
                            {
                                reportString += tmpString[0] + "*";
                            }
                            break;
                        case "ResUpdatePas":
                            IsNeedShowOperationResultEvent(command[1]);
                            break;
                        case "ResAddUser":
                            if (Convert.ToBoolean(command[1]))
                                sMessage = "Пользователь успешно добавлен.";
                            else
                                sMessage = "При добавлении пользователя возникла ошибка.";
                            if (IsNeedShowOperationResultEvent != null)
                                IsNeedShowOperationResultEvent(sMessage);
                            break;
                        case "ResAddDev":
                            if (Convert.ToBoolean(command[1]))
                                sMessage = "Устройство успешно добавлено.";
                            else
                                sMessage = "При добавлении устройства возникла ошибка.";
                            if (IsNeedShowOperationResultEvent != null)
                                IsNeedShowOperationResultEvent(sMessage);
                            break;
                        case "ResDeleteDev":
                            if (Convert.ToBoolean(command[1]))
                                sMessage = "Устройство успешно удалено.";
                            else
                                sMessage = "При удалении устройства возникла ошибка.";
                            if (IsNeedShowOperationResultEvent != null)
                                IsNeedShowOperationResultEvent(sMessage);
                            break;
                        case "ResDeleteUser":
                            if (Convert.ToBoolean(command[1]))
                                sMessage = "Пользователь успешно удалён.";
                            else
                                sMessage = "При удалении пользователя возникла ошибка.";
                            if (IsNeedShowOperationResultEvent != null)
                                IsNeedShowOperationResultEvent(sMessage);
                            break;
                        case "Update":
                            IsNeedUpdateThreeEvent(command[1]);
                            break;
                    } 
                }
            }
        }

        public void Send(String data, bool needToEncrypt)
        {
            try
            {
                byte[] byteData = null;
                if (needToEncrypt)
                {
                    // Convert the string data to byte data using ASCII encoding.
                    byteData = Encoding.ASCII.GetBytes(Crypto.Encrypt(data));
                }
                else
                    byteData = Encoding.ASCII.GetBytes(data + "?");
                // Begin sending the data to the remote device.
                _srv.workSocket.BeginSend(byteData, 0, byteData.Length, 0, new AsyncCallback(SendCallback), _srv.workSocket);
            }
            catch (Exception ex)
            {

                if (IsNeedShowOperationResultEvent != null)
                    IsNeedShowOperationResultEvent("Ошибка при отправке сообщения: " + ex.Message);
            }
        }

        private void SendCallback(IAsyncResult ar)
        {
            try
            {
                // Retrieve the socket from the state object.
                Socket client = (Socket)ar.AsyncState;

                // Complete sending the data to the remote device.
                int bytesSent = client.EndSend(ar);

            }
            catch (Exception ex)
            {
                if (IsNeedShowOperationResultEvent != null)
                    IsNeedShowOperationResultEvent("Ошибка при окончании отправки сообщения: " + ex.Message);
            }
        }

        public void SaveConnData(string param, string pattern)
        {
            try
            {
                string FileData = null;
                string[] tempArray;
                using (var sr = new StreamReader("Config.dat", Encoding.GetEncoding(1251)))
                {
                    FileData = sr.ReadToEnd();
                }
                tempArray = FileData.Split(';');

                FileData = Regex.Replace(FileData, pattern, param);
                using (var sr = new StreamWriter("Config.dat"))
                {
                    sr.Write(FileData);
                }
            }
            catch (Exception ex)
            {
                if (IsNeedShowOperationResultEvent != null)
                    IsNeedShowOperationResultEvent(ex.Message);
            }
        }

        public void CloseConnection()
        {
            _srv.workSocket.Close();
            if (Keep_Alive)
                KeepAlive.Abort();
            IsNeedChangeStatusEvent();
        }
    }
}
