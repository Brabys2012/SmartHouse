using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;

namespace ControlsNOBD
{
    public class MyCom
    {
        SerialPort MyComEx=new SerialPort();
        public void OpenCom()
        {
            MyComEx.PortName = "COM12";
            MyComEx.BaudRate = 9600;
            MyComEx.DataBits = 8;
            MyComEx.Parity = Parity.None;
            MyComEx.StopBits = StopBits.One;
            MyComEx.Handshake = Handshake.None;
            MyComEx.ReadBufferSize = 1024;
            MyComEx.ReadTimeout = 250;
            MyComEx.WriteBufferSize = 1024;
            MyComEx.WriteTimeout = 250;
            //  MyComLisen.Encoding = Encoding.Default;
            MyComEx.Open();
        }
        public void SendInform(string Text)
        {
            OpenCom();
            MyComEx.Write(Text);
            Close();
         }
        public void Close()
        {
            MyComEx.Close();
        }
    }
}
