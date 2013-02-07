using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace SimpleAsyncServer
{

    /// <summary>
    /// Представляет простейшего асинхронного клиента, подключенного к серверу.
    /// </summary>
    public class SimpleAsyncClient
    {

        /// <summary>
        /// Сокет подключенного клиента.
        /// </summary>
        private Socket _socket;


        /// <summary>
        /// Задает или возвращает буффер, в который считываются полученные данные.
        /// </summary>
        public byte[] Buffer { set; get; }
        /// <summary>
        /// Задает или возвращает сокет удаленного клиента.
        /// </summary>
        /// <param name="clientSocket">Сокет клиента.</param>
        public Socket ClientSocket
        {
            set
            {
                // Заполняем свойства, содержащие информацию о подключившемся клиенте.
                _socket = value;
                // Инициируем буффер для чтения полученных данных
                Buffer = new byte[_socket.ReceiveBufferSize];
                // Устанавливаем значение свойства RemoteEndPoint
                RemoteEndPoint = _socket.RemoteEndPoint;
                // Запускаем слушателя получения данных от удаленного клиента
                _socket.BeginReceive(Buffer, 0, Buffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallback), this);
            }
            get
            {
                return _socket;
            }
        }
        /// <summary>
        /// Возвращает конечную удаленную точку подключенного клиента.
        /// </summary>
        public EndPoint RemoteEndPoint { private set; get; }


        /// <summary>
        /// Событие возникает при получении данных от удаленного клиента.
        /// </summary>
        public event IODataDelegate ClientDataReceiveEvent;
        /// <summary>
        /// Событие возникает при отключении клиента.
        /// </summary>
        public event SimpleClientEventDelegate ClientNeedDisconnectEvent;


        /// <summary>
        /// Инициирует новый экземпляр класса SimpleAsyncClient.
        /// </summary>
        public SimpleAsyncClient() { }

        /// <summary>
        /// Асинхронная операция чтения данных от удаленного клиента.
        /// </summary>
        /// <param name="result">Состояние асинхронной операции.</param>
        private static void ReceiveCallback(IAsyncResult result)
        {
            // Получаем текущего клиента
            SimpleAsyncClient _client = (SimpleAsyncClient)result.AsyncState;
            try
            {
                // Определяем число прочитанных байт
                int _bytesRead = _client.ClientSocket.EndReceive(result);
                // Если есть данные
                if (_bytesRead > 0)
                {
                    // Отправляем данные получателю
                    if (_client.ClientDataReceiveEvent != null)
                        _client.ClientDataReceiveEvent(_client.Buffer, 0, _bytesRead);
                }
                // Запускаем слушателя получения данных от удаленного клиента
                _client.ClientSocket.BeginReceive(_client.Buffer, 0, _client.Buffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallback), _client);
            }
            catch (Exception)
            {
                // В случае любой другой ошибки генерируем событие закрытия соединения и отключения клиента
                if (_client.ClientNeedDisconnectEvent != null)
                    _client.ClientNeedDisconnectEvent(_client, new SimpleClientEventArgs("произошла ошибка при получении данных вследствие разрыва соединения"));
            }
        }
        /// <summary>
        /// Возвращает полученные и доступные от клиента данные, и, если установлен флаг, то ожидает в течение таймаута также и поступления новых данных. выполняется в синхронном режиме.
        /// </summary>
        /// <param name="data">Буффер для данных.</param>
        /// <param name="useTimeout">Признак необходимости использовать таймаут. Если true - то вернет доступные данные и данные, полученные в течение таймаута. Если false, то вернет только доступные данные.</param>
        /// <returns>Число полученных байт данных в массиве начиная с нуля. Вернет 0, если не было получено ни одного байта.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ObjectDisposedException"></exception>
        /// <exception cref="SecurityException"></exception>
        /// <exception cref="SocketException"></exception>
        /// <exception cref="TCPServerException"></exception>
        public int Receive(ref byte[] data, bool useTimeout)
        {
            bool _blocking = _socket.Blocking;
            _socket.Blocking = useTimeout;
            int _bytesRead = _socket.Receive(data);
            _socket.Blocking = _blocking;
            return _bytesRead;
        }

        /// <summary>
        /// Асинхронная операция отправки данных удаленному клиенту.
        /// </summary>
        /// <param name="result">Состояние асинхронной операции.</param>
        private static void SendCallback(IAsyncResult result)
        {
            // Получаем текущего клиента
            SimpleAsyncClient _client = (SimpleAsyncClient)result.AsyncState;
            try
            {
                // Отправляем данные подключенному клиенту
                _client.ClientSocket.EndSend(result);
            }
            catch (Exception)
            {
                // В случае любой другой ошибки генерируем событие закрытия соединения и отключения клиента
                if (_client.ClientNeedDisconnectEvent != null)
                    _client.ClientNeedDisconnectEvent(_client, new SimpleClientEventArgs("произошла ошибка при отправке данных вследствие разрыва соединения"));
            }
        }
        /// <summary>
        /// Осуществляет отправку клиенту байтового массива данных указанной длины, начиная с заданной позиции.
        /// </summary>
        /// <param name="data">Массив передаваемых данных.</param>
        /// <param name="pos">Позиция начала передаваемых данных в массиве.</param>
        /// <param name="len">Число байт передаваемых данных, начиная с указанной позиции.</param>
        public void Send(byte[] data, int pos, int len)
        {
            _socket.BeginSend(data, pos, len, SocketFlags.None, new AsyncCallback(SendCallback), this);
        }
        /// <summary>
        /// Осуществляет отправку строки данных клиенту.
        /// </summary>
        /// <param name="data">Массив данных для отправки.</param>
        public void Send(byte[] data)
        {
            Send(data, 0, data.Length);
        }
        /// <summary>
        /// Осуществляет отправку строки данных клиенту.
        /// </summary>
        /// <param name="data">Строка данных для отправки.</param>
        public void Send(string data)
        {
            Send(Encoding.ASCII.GetBytes(data), 0, data.Length);
        }
        /// <summary>
        /// Осуществляет отправку байта данных клиенту.
        /// </summary>
        /// <param name="data">Байт данных для отправки.</param>
        public void Send(byte data)
        {
            Send(new byte[] { data }, 0, 1);
        }

        /// <summary>
        /// Возвращает информацию о подключенном клиенте.
        /// </summary>
        /// <returns>Информация о подключенном клиенте.</returns>
        public override string ToString()
        {
            if (RemoteEndPoint == null)
                return string.Format("Client \"{0}\".", RemoteEndPoint);
            else
                return string.Format("Client \"{0}\".", "Not initialized");
        }

    }

}