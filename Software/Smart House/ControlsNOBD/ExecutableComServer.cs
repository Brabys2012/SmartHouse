using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;
using System.Windows.Forms;
using System.Collections;


namespace ControlsNOBD
{
    class ExexutableComServer
    {
        CProtocol ProtocolForExCom = new CProtocol();
        Parss_and_deter_mess ParsExe = new Parss_and_deter_mess();
        SerialPort MyComExecutable=new SerialPort();
        ArrayList Command = new ArrayList();

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
        private void DataReceivedHandler(object sender,SerialDataReceivedEventArgs e)
        {
            Command = null;
            Command = ProtocolForExCom.Unpack(((SerialPort)sender).BaseStream);
            if (Command != null) 
              ParsExe.ParssComand((DevCommand) Command[0]);

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
