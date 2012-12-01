using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;
using System.Windows.Forms;


namespace ControlsNOBD
{
    class ExexutableComServer
    {

        SerialPort MyComExecutable=new SerialPort();

        /// <summary>
        /// Метод инициализации COM - порта
        /// </summary>
        public void OpenCom()
        {
            MyComExecutable.PortName = "COM2";
            MyComExecutable.BaudRate = 9600;
            MyComExecutable.DataBits = 8;
            MyComExecutable.Parity = Parity.None;
            MyComExecutable.StopBits = StopBits.One;
            MyComExecutable.Handshake = Handshake.None;
            MyComExecutable.ReadBufferSize = 1024;
            MyComExecutable.ReadTimeout = 250;
            MyComExecutable.WriteBufferSize = 1024;
            MyComExecutable.WriteTimeout = 250;
            MyComExecutable.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);
            MyComExecutable.ErrorReceived += new SerialErrorReceivedEventHandler(DataError);
            MyComExecutable.Open();   
        }

        /// <summary>
        /// Событие происходящее при получении данных
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void DataReceivedHandler(object sender,SerialDataReceivedEventArgs e)
        {
            SerialPort sp = (SerialPort)sender;
            while (sp.BytesToRead != 0)
            {
                int counter = sp.ReadByte();
                byte[] BytArray = new byte[counter - 1];
                for (int i = 0; i < counter-1; i++)
                {
                    BytArray[i] = (byte)sp.ReadByte();
                }

                Program.MyParser.ParssComand(BytArray);
            }
        }

        /// <summary>
        /// Событие происходящее при какой-либо ошибке
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void DataError(object sender,SerialErrorReceivedEventArgs e)
        {
            MessageBox.Show("Ошибка!!!");
        }

        /// <summary>
        /// Метод закрытия COM - порта
        /// </summary>
        public void Close()
        {
            MyComExecutable.Close();
        }

        /// <summary>
        /// Метод отправки данных
        /// </summary>
        /// <param name="ByteArray">Данные для отправки</param>
        public void SendInform(byte[] ByteArray)
        {
            MyComExecutable.Write(ByteArray, 0, ByteArray.Length);
        }
    }
}
