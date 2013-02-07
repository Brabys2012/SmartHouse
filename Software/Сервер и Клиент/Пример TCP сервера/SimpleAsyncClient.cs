using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace SimpleAsyncServer
{

    /// <summary>
    /// ������������ ����������� ������������ �������, ������������� � �������.
    /// </summary>
    public class SimpleAsyncClient
    {

        /// <summary>
        /// ����� ������������� �������.
        /// </summary>
        private Socket _socket;


        /// <summary>
        /// ������ ��� ���������� ������, � ������� ����������� ���������� ������.
        /// </summary>
        public byte[] Buffer { set; get; }
        /// <summary>
        /// ������ ��� ���������� ����� ���������� �������.
        /// </summary>
        /// <param name="clientSocket">����� �������.</param>
        public Socket ClientSocket
        {
            set
            {
                // ��������� ��������, ���������� ���������� � �������������� �������.
                _socket = value;
                // ���������� ������ ��� ������ ���������� ������
                Buffer = new byte[_socket.ReceiveBufferSize];
                // ������������� �������� �������� RemoteEndPoint
                RemoteEndPoint = _socket.RemoteEndPoint;
                // ��������� ��������� ��������� ������ �� ���������� �������
                _socket.BeginReceive(Buffer, 0, Buffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallback), this);
            }
            get
            {
                return _socket;
            }
        }
        /// <summary>
        /// ���������� �������� ��������� ����� ������������� �������.
        /// </summary>
        public EndPoint RemoteEndPoint { private set; get; }


        /// <summary>
        /// ������� ��������� ��� ��������� ������ �� ���������� �������.
        /// </summary>
        public event IODataDelegate ClientDataReceiveEvent;
        /// <summary>
        /// ������� ��������� ��� ���������� �������.
        /// </summary>
        public event SimpleClientEventDelegate ClientNeedDisconnectEvent;


        /// <summary>
        /// ���������� ����� ��������� ������ SimpleAsyncClient.
        /// </summary>
        public SimpleAsyncClient() { }

        /// <summary>
        /// ����������� �������� ������ ������ �� ���������� �������.
        /// </summary>
        /// <param name="result">��������� ����������� ��������.</param>
        private static void ReceiveCallback(IAsyncResult result)
        {
            // �������� �������� �������
            SimpleAsyncClient _client = (SimpleAsyncClient)result.AsyncState;
            try
            {
                // ���������� ����� ����������� ����
                int _bytesRead = _client.ClientSocket.EndReceive(result);
                // ���� ���� ������
                if (_bytesRead > 0)
                {
                    // ���������� ������ ����������
                    if (_client.ClientDataReceiveEvent != null)
                        _client.ClientDataReceiveEvent(_client.Buffer, 0, _bytesRead);
                }
                // ��������� ��������� ��������� ������ �� ���������� �������
                _client.ClientSocket.BeginReceive(_client.Buffer, 0, _client.Buffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallback), _client);
            }
            catch (Exception)
            {
                // � ������ ����� ������ ������ ���������� ������� �������� ���������� � ���������� �������
                if (_client.ClientNeedDisconnectEvent != null)
                    _client.ClientNeedDisconnectEvent(_client, new SimpleClientEventArgs("��������� ������ ��� ��������� ������ ���������� ������� ����������"));
            }
        }
        /// <summary>
        /// ���������� ���������� � ��������� �� ������� ������, �, ���� ���������� ����, �� ������� � ������� �������� ����� � ����������� ����� ������. ����������� � ���������� ������.
        /// </summary>
        /// <param name="data">������ ��� ������.</param>
        /// <param name="useTimeout">������� ������������� ������������ �������. ���� true - �� ������ ��������� ������ � ������, ���������� � ������� ��������. ���� false, �� ������ ������ ��������� ������.</param>
        /// <returns>����� ���������� ���� ������ � ������� ������� � ����. ������ 0, ���� �� ���� �������� �� ������ �����.</returns>
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
        /// ����������� �������� �������� ������ ���������� �������.
        /// </summary>
        /// <param name="result">��������� ����������� ��������.</param>
        private static void SendCallback(IAsyncResult result)
        {
            // �������� �������� �������
            SimpleAsyncClient _client = (SimpleAsyncClient)result.AsyncState;
            try
            {
                // ���������� ������ ������������� �������
                _client.ClientSocket.EndSend(result);
            }
            catch (Exception)
            {
                // � ������ ����� ������ ������ ���������� ������� �������� ���������� � ���������� �������
                if (_client.ClientNeedDisconnectEvent != null)
                    _client.ClientNeedDisconnectEvent(_client, new SimpleClientEventArgs("��������� ������ ��� �������� ������ ���������� ������� ����������"));
            }
        }
        /// <summary>
        /// ������������ �������� ������� ��������� ������� ������ ��������� �����, ������� � �������� �������.
        /// </summary>
        /// <param name="data">������ ������������ ������.</param>
        /// <param name="pos">������� ������ ������������ ������ � �������.</param>
        /// <param name="len">����� ���� ������������ ������, ������� � ��������� �������.</param>
        public void Send(byte[] data, int pos, int len)
        {
            _socket.BeginSend(data, pos, len, SocketFlags.None, new AsyncCallback(SendCallback), this);
        }
        /// <summary>
        /// ������������ �������� ������ ������ �������.
        /// </summary>
        /// <param name="data">������ ������ ��� ��������.</param>
        public void Send(byte[] data)
        {
            Send(data, 0, data.Length);
        }
        /// <summary>
        /// ������������ �������� ������ ������ �������.
        /// </summary>
        /// <param name="data">������ ������ ��� ��������.</param>
        public void Send(string data)
        {
            Send(Encoding.ASCII.GetBytes(data), 0, data.Length);
        }
        /// <summary>
        /// ������������ �������� ����� ������ �������.
        /// </summary>
        /// <param name="data">���� ������ ��� ��������.</param>
        public void Send(byte data)
        {
            Send(new byte[] { data }, 0, 1);
        }

        /// <summary>
        /// ���������� ���������� � ������������ �������.
        /// </summary>
        /// <returns>���������� � ������������ �������.</returns>
        public override string ToString()
        {
            if (RemoteEndPoint == null)
                return string.Format("Client \"{0}\".", RemoteEndPoint);
            else
                return string.Format("Client \"{0}\".", "Not initialized");
        }

    }

}