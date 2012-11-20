using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;
using System.Windows.Forms;


namespace ControlsNOBD
{
    class WorkWithCom
    {
        SerialPort MyComLisen=new SerialPort();
        public void OpenCom()
        {
            MyComLisen.PortName = "COM2";
            MyComLisen.BaudRate = 9600;
            MyComLisen.DataBits = 8;
            MyComLisen.Parity = Parity.None;
            MyComLisen.StopBits = StopBits.One;
            MyComLisen.Handshake = Handshake.None;
            MyComLisen.ReadBufferSize = 1024;
            MyComLisen.ReadTimeout = 250;
            MyComLisen.WriteBufferSize = 1024;
            MyComLisen.WriteTimeout = 250;
          //  MyComLisen.Encoding = Encoding.Default;
            MyComLisen.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);
            MyComLisen.ErrorReceived += new SerialErrorReceivedEventHandler(DataError);
            MyComLisen.Open();   
        }
        private static void DataReceivedHandler(object sender,SerialDataReceivedEventArgs e)
        {
            SerialPort sp = (SerialPort)sender;
            string indata = sp.ReadExisting();
            sp.DiscardInBuffer();
            MessageBox.Show(indata);
            Controls1.PaDm.ParssComand(indata);
        }
        private static void DataError(object sender,SerialErrorReceivedEventArgs e)
        {
            MessageBox.Show("Ошибка!!!");
        }
        public void Close()
        {
            MyComLisen.Close();
        }
        public void SendInform(string Text)
        {
            MyComLisen.Write(Text);
        }
    }
}
