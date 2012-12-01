using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;
using System.Windows.Forms;

namespace ControlsNOBD
{
    public class ComListenServer
    {
        SerialPort MyComListen=new SerialPort();
        public void OpenCom()
        {
            MyComListen.PortName = "COM12";
            MyComListen.BaudRate = 9600;
            MyComListen.DataBits = 8;
            MyComListen.Parity = Parity.None;
            MyComListen.StopBits = StopBits.One;
            MyComListen.Handshake = Handshake.None;
            MyComListen.ReadBufferSize = 1024;
            MyComListen.ReadTimeout = 250;
            MyComListen.WriteBufferSize = 1024;
            MyComListen.WriteTimeout = 250;
            //  MyComLisen.Encoding = Encoding.Default;
            MyComListen.Open();
        }
        public void SendInform(byte[] ByteArray)
        {
            try
            {
                if (!MyComListen.IsOpen)
                {
                    OpenCom();    
                }
                MyComListen.Write(ByteArray, 0, ByteArray.Length);
                Close();
            }
            catch
            {
                MessageBox.Show("Ошибка сообщение не отправлено!" );
            }
         }
        public void Close()
        {
            MyComListen.Close();
        }
    }
}
