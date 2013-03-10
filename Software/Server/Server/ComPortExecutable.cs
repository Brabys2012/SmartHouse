using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;
using System.Threading;
using System.Collections;

namespace Server
{
    /// <summary>
    /// Класс, отвечает за посылку сообщений по Исполнительному Ком - порту и принимает ответ от контролера
    /// </summary>
    public class ComPortExecutable
    {
        SerialPort MyComExecutable=new SerialPort();
        CProtocol ProtocolForEx = new CProtocol();
        bool IsFindCommand = false;
        ArrayList Command;
        DevCommand answer;
        /// <summary>
        /// Метод инициализации COM - порта
        /// </summary>
        public void OpenCom()
        {
            MyComExecutable.PortName = "COM4";
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
        public void DataReceivedHandler(object sender, SerialDataReceivedEventArgs e)
        {

            // TODO: Мы получаем одну команду или набор?

            Command = ProtocolForEx.Unpack(((SerialPort)sender).BaseStream);
            
			// TODO: А куда деваем команды 1, 2 и другие, если они есть?
            //Если мы получаем более одной команды, то это уже противоречит 
            //Логике работы нашего умного дома (одна команда - один ответ)
            if (Command != null)
            {
                answer = (DevCommand)Command[0];
                IsFindCommand = false;
            }
            

            /*SerialPort sp = (SerialPort)sender;

            int counter = sp.ReadByte();
            byte[] BytArray = new byte[counter];
            BytArray[0] = (byte)counter;
            for (int i = 1; i < counter; i++)
            {
                BytArray[i] = (byte)sp.ReadByte();
            }
            // Program.MyParser.ParssComand(BytArray);
            string Pank = sp.ReadExisting();
            answer = BytArray;*/
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
            MyComExecutable.Close();
        }

        /// <summary>
        /// Метод отправки данных
        /// </summary>
        /// <param name="ByteArray">Данные для отправки</param>
        public DevCommand SendInform(byte[] ByteArray)
        {
            OpenCom();
            MyComExecutable.Write(ByteArray, 0, ByteArray.Length);
            DevCommand comand = WaitAnswer();
            Close();
            return comand;
        }

        /// <summary>
        /// Ждет ответа от контролера и когда ответ приходит он возвращает его.
        /// </summary>
        public DevCommand WaitAnswer()
        {
            while (!IsFindCommand)
            {
                Thread.Sleep(300);
            }
            DevCommand result = answer;
            return result;
        }        
    }
}
