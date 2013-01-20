using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;

namespace Server
{
    /// <summary>
    /// Класс принимает команды от компорта "Слушатель" и помещает их в общую очередь
    /// </summary>
    public class ComPortListener
    {
        SerialPort MyComListener=new SerialPort();
        /// <summary>
        /// Метод инициализации COM - порта
        /// </summary>
        public void OpenCom()
        {
            MyComListener.PortName = "COM13";
            MyComListener.BaudRate = 9600;
            MyComListener.DataBits = 8;
            MyComListener.Parity = Parity.None;
            MyComListener.StopBits = StopBits.One;
            MyComListener.Handshake = Handshake.None;
            MyComListener.ReadBufferSize = 1024;
            MyComListener.ReadTimeout = 250;
            MyComListener.WriteBufferSize = 1024;
            MyComListener.WriteTimeout = 250;
            MyComListener.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);
            MyComListener.ErrorReceived += new SerialErrorReceivedEventHandler(DataError);
            MyComListener.Open();   
        }

        /// <summary>
        /// Событие происходящее при получении данных
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void DataReceivedHandler(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort sp = (SerialPort)sender;
            try
            {
                while (sp.WriteBufferSize != 0)
                {
                    int counter = sp.ReadByte();
                    byte[] BytArray = new byte[counter];
                    BytArray[0] = (byte)counter;
                    for (int i = 1; i < counter; i++)
                    {
                        BytArray[i] = (byte)sp.ReadByte();
                    }
                    lock (Storage.QueueList)
                    {
                        Storage.QueueList.Enqueue(BytArray);
                    }
                }
            }
            catch
            {
                lock (Storage.MessegesForUser)
                {
                    Storage.MessegesForUser.Enqueue("Ошибка в работе контролера");
                }
            }
        }
        /// <summary>
        /// Событие происходящее при какой-либо ошибке
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void DataError(object sender,SerialErrorReceivedEventArgs e)
        {
            
        }

        /// <summary>
        /// Метод закрытия COM - порта
        /// </summary>
        public void Close()
        {
            MyComListener.Close();
        }
    }
}
