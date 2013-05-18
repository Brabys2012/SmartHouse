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
    public delegate void IsNeedShowDataDelegate(string data);
    public delegate void StatusIsActiveDelegate(bool IsActive);
    public delegate void IsNeedToPlotDelegate(string reportData);
    public delegate void IsNeedUpdateThreeDelegate(string DevData);
    public delegate void IsNeedChangeStatus();

    public class AsynchronousClient
        {
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
            }

            string reportString = "";

            public event IsNeedShowDelegate IsNeedShowLoginFormEvent;
            public event IsNeedShowDataDelegate IsNeedShowDataEvent;
            public event StatusIsActiveDelegate StatusIsActiveEvent;
            public event IsNeedToPlotDelegate IsNeedToPlotEvent;
            public event IsNeedUpdateThreeDelegate IsNeedUpdateThreeEvent;
            public event IsNeedChangeStatus IsNeedChangeStatusEvent;

            public StateObject _srv;

            public void StartClient(string IP, int port)
            {
                // Настраиваем и начинаем подключение к удалённой точке.
                try
                {

                    IPAddress ipAddress = IPAddress.Parse(IP);
                    IPEndPoint remoteEP = new IPEndPoint(ipAddress, port);
                    _srv = new StateObject();
                    _srv.workSocket = new Socket(AddressFamily.InterNetwork,
                        SocketType.Stream, ProtocolType.Tcp);
                    // Начинаем подключение к серверу.
                    _srv.workSocket.BeginConnect(remoteEP, new AsyncCallback(ConnectCallback), _srv);
                }
                catch (Exception e)
                {
                    if (IsNeedShowDataEvent != null)
                        IsNeedShowDataEvent(e.Message);
                }
            }

            /// <summary>
            /// Обратная функция для подключения
            /// </summary>
            /// <param name="ar">Результат выполнения подключения(сокет)</param>
            private  void ConnectCallback(IAsyncResult ar)
            {
                try
                {
                    //Получаем сокет.
                    StateObject client = (StateObject)ar.AsyncState;

                    // Завершаем подключение.
                    client.workSocket.EndConnect(ar);
                    client.status = true;
                    IsNeedChangeStatusEvent();
                    //Начинаем получать данные от удалённой точки.
                    client.workSocket.BeginReceive(client.buffer, 0, 
                        StateObject.BufferSize, 0, new AsyncCallback(ReceiveCallback), client);
                }
                catch (Exception e)
                {
                    if (IsNeedShowDataEvent != null)
                        IsNeedShowDataEvent(e.Message);
                }
            }
            
            /// <summary>
            /// 
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
                    if (IsNeedShowDataEvent != null)
                        IsNeedShowDataEvent(e.Message);
                }
            }

            private void Parser(int bytesRead, bool needToEncrypt)
            {  
                string[] message;
                string[] tmpString;
                if (needToEncrypt)
                    message = Crypto.Decrypt(
                        Encoding.ASCII.GetString(_srv.buffer, 0, bytesRead)).Split('?');
                else
                    message = Encoding.ASCII.GetString(_srv.buffer, 0, bytesRead).Split('?');

                for (int i = 0; i < message.Length; i++)
                {
                    string[] command = message[i].Split('/');
                    switch (command[0])
                    {
                        case "AuthAnsver":
                            string[] tmpArray;
                            tmpArray = command[1].Split('*');
                            if (tmpArray[0] == "1")
                            {
                                lock (_srv.role)
                                    _srv.role = tmpArray[1];
                            }
                            else 
                            {
                                IsNeedShowDataEvent("Неверный логин или пароль");
                                _srv.role = "";
                            }

                            break;
                        case "Auth":
                            // Генерируем событие необходимости отобразить форму ввода логина/пароля
                            if (IsNeedShowLoginFormEvent != null)
                                IsNeedShowLoginFormEvent();
                            break;
                        case "mess":
                            if (IsNeedShowDataEvent != null)
                                IsNeedShowDataEvent(command[1]);
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
                        case "Update":
                            IsNeedUpdateThreeEvent(command[1]);
                            break;
                    }
                }    
            }

            public void Send(String data, bool needToEncrypt)
            {
                try
                {
                    byte[] byteData = Encoding.ASCII.GetBytes(data);
                    if (needToEncrypt)
                    {
                        // Convert the string data to byte data using ASCII encoding.
                        byteData = Encoding.ASCII.GetBytes(Crypto.Encrypt(data));
                    }
                    // Begin sending the data to the remote device.
                    _srv.workSocket.BeginSend(byteData, 0, byteData.Length, 0, new AsyncCallback(SendCallback), _srv.workSocket);
                }
                catch (Exception ex)
                {

                    IsNeedShowDataEvent(ex.Message);
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
                catch (Exception e)
                {
                    if (IsNeedShowDataEvent != null)
                        IsNeedShowDataEvent(e.Message);
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

                    FileData =  Regex.Replace(FileData, pattern, param);
                    using (var sr = new StreamWriter("Config.dat"))
                    {
                        sr.Write(FileData);
                    }
                }
                catch (Exception ex)
                {
                    IsNeedShowDataEvent(ex.Message);
                }
            }

            public void CloseConnection()
            {
                _srv.workSocket.Close();
                IsNeedChangeStatusEvent();
            }
        }
    }
