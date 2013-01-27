using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Threading;
using System.Net;

namespace Server
{
     class AsynchronousIoServer
    {
    // метод SetupServerSocket и конструктор - те же,
        // что и в классе ThreadedServer
    private Socket _serverSocket;
    private int _port;

    public AsynchronousIoServer(int port) { _port = port; }

    private class ConnectionInfo
    {
        public Socket Socket;
        public byte[] Buffer;
    }

    private void SetupServerSocket()
    {
        // Получаем информацию о локальном компьютере
        IPHostEntry localMachineInfo =
            Dns.GetHostEntry(Dns.GetHostName());
         IPEndPoint myEndpoint = new IPEndPoint(
           IPAddress.Any, _port);

        // Создаем сокет, привязываем его к адресу
        // и начинаем прослушивание
        _serverSocket = new Socket(
            AddressFamily.InterNetwork,
            SocketType.Stream, ProtocolType.Tcp);
        _serverSocket.Bind(myEndpoint);
        _serverSocket.Listen((int)
            SocketOptionName.MaxConnections);
    }

    private List<ConnectionInfo> _connections =
        new List<ConnectionInfo>();

    public void Start()
    {
        SetupServerSocket();
        for (int i = 0; i < 10; i++)
            _serverSocket.BeginAccept(new
                AsyncCallback(AcceptCallback), _serverSocket);
    }

    private void AcceptCallback(IAsyncResult result)
    {
        ConnectionInfo connection = new ConnectionInfo();
        try
        {
            // Завершение операции Accept
            Socket s = (Socket)result.AsyncState;
            connection.Socket = s.EndAccept(result);
            connection.Buffer = new byte[255];
            lock (_connections) _connections.Add(connection);

            // Начало операции Receive и новой операции Accept
            connection.Socket.BeginReceive(connection.Buffer,
                    0, connection.Buffer.Length, SocketFlags.None,
                    new AsyncCallback(ReceiveCallback),
                    connection);

            _serverSocket.BeginAccept(new AsyncCallback(
                AcceptCallback), result.AsyncState);

        }
        catch (SocketException exc)
        {
            CloseConnection(connection);
            Console.WriteLine("Socket exception: " +
                exc.SocketErrorCode);
        }
        catch (Exception exc)
        {
            CloseConnection(connection);
            Console.WriteLine("Exception: " + exc);
        }
    }

    private void ReceiveCallback(IAsyncResult result)
    {
        ConnectionInfo connection =
         (ConnectionInfo)result.AsyncState;
        string message = "";
        string[] authMess = null;
        bool succesAuth = false;
       
        try
        {
            int bytesRead = connection.Socket.EndReceive(result);
            if (bytesRead > 0)
            {
                message =Crypto.Decrypt(Encoding.ASCII.GetString(connection.Buffer, 0, bytesRead));
                if (message[0] == 'a')
                {
                    authMess = message.Split('.');
                    if (authMess[1] == "adm" && authMess[2] == "123")
                    {
                        connection.Socket.Send(Encoding.ASCII.GetBytes(Crypto.Encrypt("successful authorization")));
                        succesAuth = true;
                    }
                    else
                    {
                        connection.Socket.Send(Encoding.ASCII.GetBytes(Crypto.Encrypt("authorization fails")));
                        CloseConnection(connection);
                        
                    }
                }
                else
                {
                    connection.Socket.Send(Encoding.ASCII.GetBytes(Crypto.Encrypt("Data received")));
                    Console.WriteLine("Клиент {0} написал: {1}", connection.Socket.RemoteEndPoint, message);
                    succesAuth = true;
                }

                if (succesAuth)
                connection.Socket.BeginReceive(
                    connection.Buffer, 0,
                    connection.Buffer.Length, SocketFlags.None,
                    new AsyncCallback(ReceiveCallback),
                    connection);
            }
            else CloseConnection(connection);
        }
        catch (SocketException exc)
        {
            CloseConnection(connection);
            Console.WriteLine("Socket exception: " +
                exc.SocketErrorCode);
        }
        catch (Exception exc)
        {
            CloseConnection(connection);
            Console.WriteLine("Exception: " + exc);
        }
    }

    private void CloseConnection(ConnectionInfo ci)
    {
        ci.Socket.Close();
        lock (_connections) _connections.Remove(ci);
    }

        /// <summary>
        /// Проверяет правильность логина и пароля
        /// </summary>
        public bool ControlAuth(string login, string pass)
        {
            throw new System.NotImplementedException();
        }
    }
}
