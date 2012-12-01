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
        private static void DataReceivedHandler(object sender,SerialDataReceivedEventArgs e)
        {
            SerialPort sp = (SerialPort)sender;
            int i = 0;
            byte[] BytArray = new byte[sp.BytesToRead];
            while (sp.BytesToRead != 0)
            {
                BytArray[i] = (byte)sp.ReadByte();
                i++;
            }
            Program.MyParser.ParssComand(BytArray);
        }
        private static void DataError(object sender,SerialErrorReceivedEventArgs e)
        {
            MessageBox.Show("Ошибка!!!");
        }
        public void Close()
        {
            MyComExecutable.Close();
        }
        public void SendInform(byte[] ByteArray)
        {
            MyComExecutable.Write(ByteArray, 0, ByteArray.Length);
        }
    }
}
