using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using RUS_Project.libExtendLog;


namespace RUS_Project.libTCPServer
{

    /// <summary>
    /// Простой асинхронный TCP сервер.
    /// </summary>
    public class SimpleAsyncServer
    {

        #region Объявление полей, свойств и событий

        /// <summary>
        /// Сокет сервера.
        /// </summary>
        private Socket _sSocket;
        /// <summary>
        /// Список текущих клиентских подключений.
        /// </summary>
        private List<SimpleAsyncClient> _curConnections;
        /// <summary>
        /// Журнал для записи происходящих событий.
        /// </summary>
        private ExtendLog _log;
        /// <summary>
        /// Буфферы данных для приема и отправки удаленным клиентам.
        /// </summary>
        private byte[] _rBuffer, _wBuffer;


        /// <summary>
        /// Возвращает признак того, что сервер запущен.
        /// </summary>
        public bool IsStarted { private set; get; }
        /// <summary>
        /// Возвращает идентификатор порта данного TCP сервера.
        /// </summary>
        public string Name { private set; get; }
        /// <summary>
        /// Возвращает локальную конечную точку подключения сервера.
        /// </summary>
        public EndPoint ServerPoint { private set; get; }
        /// <summary>
        /// Задает или возвращает размер буффера чтения/записи данных в байтах.
        /// </summary>
        public int IOBufferLength { set; get; }
        /// <summary>
        /// Задает или возвращает таймаут операций ввода/вывода.
        /// </summary>
        /// <exception cref="Exception">Возникает в случае попытки установить значение свойства в то время, когда сервер запущен.</exception>
        public int IOTimeout { set; get; }


        /// <summary>
        /// Событие возникает при получении данных от клиента.
        /// </summary>
        public event IODataDelegate ClientsDataReceiveEvent;
        /// <summary>
        /// Событие возникает при той или иной ситуации, произошедшей с сервером.
        /// </summary>
        public event EventRepresentationDelegate ServerEventsEvent;
        
        #endregion


        /// <summary>
        /// Инициализирует новый экземпляр класса SimpleAsyncServer.
        /// </summary>
        /// <param name="name">Имя порта прозрачной переброски данных, в который входит данный TCP сервер.</param>
        /// <param name="ipPoint">IP:Port адрес сервера.</param>
        /// <param name="eventLog">Журнал событий, в который будут записываться сообщения о работе сервера и состоянии клиентов.</param>
        public SimpleAsyncServer(string name, IPEndPoint ipPoint, ExtendLog eventLog)
        {
            // Устанавливаем значения свойств
            IsStarted = false;
            Name = name;
            ServerPoint = ipPoint;
            IOBufferLength = 1024;
            IOTimeout = 250;
            // Устанавливаем значения параметров
            _sSocket = null;
            _curConnections = new List<SimpleAsyncClient>();
            _log = eventLog;
            _log.LogEntryWritedEvent += new LogEntryEventDelegate(log_LogEntryWritedEvent);
        }
        /// <summary>
        /// Запускает на исполнение данный TCP сервер.
        /// </summary>
        /// <exception cref="TCPServerException">Возникает в случае ошибки запуска сервера для прослушивания заданного TCP порта.</exception>
        public void Start()
        {
            try
            {
                // Инициируем сокет сервера
                _sSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                // Разрешаем использовать порт только текущим процессом
                _sSocket.ExclusiveAddressUse = true;
                // Запрещаем задерживать закрытие порта для отправки всех отложенных данных
                _sSocket.LingerState.Enabled = false;
                // Отключаем использование алгоритма Nagle
                _sSocket.NoDelay = true;
                // Задаем таймаут операций ввода/вывода
                _sSocket.SendTimeout = _sSocket.ReceiveTimeout = IOTimeout;
                // Задаем размер буффера для операций ввода/вывода
                _sSocket.SendBufferSize = _sSocket.ReceiveBufferSize = IOBufferLength;
                // Переводим сокет в неблокирующий режим
                _sSocket.Blocking = false;
                // Связываем сокет с локальной конечной точкой
                _sSocket.Bind(ServerPoint);
                // Начинаем прослушивать сокет
                _sSocket.Listen((int)SocketOptionName.MaxConnections);
                // Устанавливаем признак того, что сервер успешно запущен
                IsStarted = true;
                // Протоколируем успешный запуск
                _log.Write(EventType.State, string.Format("Локальный TCP сервер {0} успешно запущен. Ожидание подключений.", ServerPoint));
                // Принимаем соединения
                for (int i = 0; i < 10; i++)
                    _sSocket.BeginAccept(new AsyncCallback(AceptCallback), _sSocket);
            }
            catch (ExtendLogException exc)
            {
                throw new TCPServerException(String.Format("Ошибка запуска локального TCP сервера {0} (Причина: {1}).", ServerPoint, exc.Message));
            }
            catch (Exception exc)
            {
                if (_log != null)
                    _log.Write(EventType.Error, String.Format("Ошибка запуска локального TCP сервера {0} (Причина: {1}).", ServerPoint, exc.Message));
                throw new TCPServerException(String.Format("Ошибка запуска локального TCP сервера {0} (Причина: {1}).", ServerPoint, exc.Message));
            }
        }
        /// <summary>
        /// Прекращает работу данного сервера.
        /// </summary>
        public void Stop()
        {
            // Закрываем все соединения
            lock (_curConnections)
            {
                foreach (SimpleAsyncClient _client in _curConnections)
                    client_ClientNeedDisconnectEvent(_client, new SimpleClientEventArgs("произошла остановка сервера"));
            }
            // Закрываем сокет
            _sSocket.Close();
            // Записываем в журнал событий сообщение об становке сервера и закрываем журнал
            try
            {
                _log.Write(EventType.State, string.Format("Cервер {0} успешно остановлен.", ServerPoint));
                _log.LogEntryWritedEvent -= log_LogEntryWritedEvent;
                _log.Close("остановка сервера");
                _log = null;
            }
            catch (Exception) { }
        }

        /// <summary>
        /// Асинхронная операция принятия подключения.
        /// </summary>
        /// <param name="result">Состояние асинхронной операции.</param>
        private void AceptCallback(IAsyncResult result)
        {
            // Получаем сокет сервера
            Socket _tsSocket = (Socket)result.AsyncState;
            // Создаем нового клиента
            SimpleAsyncClient _client = new SimpleAsyncClient();
            try
            {
                // Задаем сокет клиента
                _client.ClientSocket = _tsSocket.EndAccept(result);
                // Подписываемся на клиентские события
                _client.ClientDataReceiveEvent += new IODataDelegate(client_ClientDataReceiveEvent);
                _client.ClientNeedDisconnectEvent += new SimpleClientEventDelegate(client_ClientNeedDisconnectEvent);
                // Сохраняем клиента
                lock (_curConnections) { _curConnections.Add(_client); }
                // Вносим запись в журнал событий
                _log.Write(EventType.Connect, _client.RemoteEndPoint.ToString(), "", "Подключился локальный клиент.");
                // Запускаем новый слушатель сокета
                _tsSocket.BeginAccept(new AsyncCallback(AceptCallback), result.AsyncState);
            }
            catch (SocketException exc)
            {
                // Вносим запись в журнал событий
                _log.Write(EventType.ConnectError, string.Format("При подключении клиента {0} произошла ошибка (Ошибка: {1}).", _client.RemoteEndPoint, exc.Message));
                // Отключаем клиента
                client_ClientNeedDisconnectEvent(_client, new SimpleClientEventArgs("произошла ошибка при подключении"));
            }
            catch (Exception exc)
            {
                if (!EventLog.SourceExists("EnergyNetService"))
                    EventLog.CreateEventSource("EnergyNetService", "EnergyNetLog");
                EventLog winLog = new EventLog();
                winLog.Source = "EnergyNetService";
                winLog.Log = "EnergyNetLog";
                winLog.WriteEntry(string.Format("Ошибка простого TCP сервера {0} (Причина: {1}).", ServerPoint, exc.Message), EventLogEntryType.Error);
                // Отключаем клиента
                client_ClientNeedDisconnectEvent(_client, new SimpleClientEventArgs("произошла ошибка при подключении"));
            }
        }

        /// <summary>
        /// Обработчик события получения данных слушателем клиента.
        /// </summary>
        /// <param name="data">Массив данных для отправки.</param>
        /// <param name="pos">Позиция в массиве начала передаваемых данных.</param>
        /// <param name="len">Число полезных байт данных в массиве, начиная с заданной позиции.</param>
        private void client_ClientDataReceiveEvent(byte[] data, int pos, int len)
        {
            // Генерируем событие получения сервером данных для отправки этих данных в последовательный порт
            if (ClientsDataReceiveEvent != null)
                ClientsDataReceiveEvent(data, pos, len);
        }
        /// <summary>
        /// Обработчик события необходимости отключить клиента.
        /// </summary>
        /// <param name="sender">Экземпляр класса, соответствующий инициатору события.</param>
        /// <param name="e">Параметры произошедшего события.</param>
        private void client_ClientNeedDisconnectEvent(object sender, SimpleClientEventArgs e)
        {
            // Получаем текущего клиента
            SimpleAsyncClient _client = (SimpleAsyncClient)sender;
            // Отписываемся от событий
            _client.ClientDataReceiveEvent -= client_ClientDataReceiveEvent;
            _client.ClientNeedDisconnectEvent -= client_ClientNeedDisconnectEvent;
            // Закрываем подключение
            _client.ClientSocket.Close();
            // Удаляем клиента из списка текущих подключений
            lock (_curConnections) { _curConnections.Remove(_client); }
            // Записываем в лог
            _log.Write(EventType.Connect, _client.RemoteEndPoint.ToString(), "", e.ToString());
        }
        /// <summary>
        /// Обработчик события внесения записей в журнал событий сервера.
        /// </summary>
        /// <param name="sender">Экземпляр класса, соответствующий инициатору события.</param>
        /// <param name="e">Параметры произошедшего события.</param>
        private void log_LogEntryWritedEvent(object sender, LogEntryEventArgs e)
        {
            if (ServerEventsEvent != null)
                ServerEventsEvent(EventRepresentetionType.SERVER_LOG_ENTRY, string.Format("{0};;{1}", e.SenderName, e.ToLogEntryString()));
        }


        /// <summary>
        /// Осуществляет отправку всем подключенным клиентам блока данных заданной длины из однобайтного массива, начиная с заданной позиции.
        /// </summary>
        /// <param name="data">Массив данных для отправки.</param>
        /// <param name="pos">Позиция в массиве начала данных для отправки.</param>
        /// <param name="len">Число байт данных в массиве, которые необходимо отправить, начиная с заданной позиции.</param>
        public void SendData(byte[] data, int pos, int len)
        {
            // Блокируем список текущих соединений, чтобы отправить только тем, кто в списке, но не новым
            lock (_curConnections)
            {
                // Перебираем все текущие подключения
                for (int i = 0; i < _curConnections.Count; i++)
                    _curConnections[i].Send(data, pos, len);
            }
        }

    }

}