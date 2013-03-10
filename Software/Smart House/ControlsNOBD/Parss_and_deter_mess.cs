using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace ControlsNOBD
{
    /// <summary>
    /// Клас который разбирает присланные команды и исполняет их
    /// </summary>
    public class Parss_and_deter_mess
    {
        CProtocol ProtocolForParser = new CProtocol(); 
        /// <summary>
        /// Метод, определения пришел запрос или управляющая команда
        /// </summary>
        /// <param name="Comand">Присланная команда</param>
        public void ParssComand (DevCommand Comand)
        {
            if (Comand.command.Count() >= 1)
            {//Выполняется если пришла управляющая команда
                ExeComand(Comand);
            }
            else if ((Comand.command.Count() == 0) || (Comand.command == null))
            {//Выполняется если пришёл запрос
                Query(Comand);
            }
            else
            {
                MessageBox.Show("Неверный формат команды");
            }
        }

        /// <summary>
        /// Метод, который исполняет управляющие команды
        /// </summary>
        /// <param name="Par">Управляющая команда</param>
        /// <returns></returns>
        public void ExeComand(DevCommand Par)
        {
            int tmpcount = 0;
            UInt64 Value = 0;
            int masCounter = Par.command.Count();
            for (int i = 0; i < masCounter; i++)
            {
                Value = (UInt64)(Value + Par.command[i] * Math.Pow(256, masCounter - 1 - tmpcount));
                tmpcount++;
            }

            DevCommand Ansewr = new DevCommand();
            Ansewr.port = Par.port;
            Ansewr.device = Par.device;
            Ansewr.command = new byte[1];
            bool result = Program.data_module.UpdDeviceVal(Par.port.ToString(), Par.device.ToString(), Value.ToString());
            if (result)
            {
                Ansewr.command[0] = 1;
            }
            else
            {
                Ansewr.command[0] = 0;
            }
            byte[] PackAnswer = ProtocolForParser.Pack(Ansewr.port, Ansewr.device, Ansewr.command);
            Program.WW.SendInform(PackAnswer);
        }

        /// <summary>
        /// Метод, который исполняет запрос
        /// </summary>
        /// <param name="Par">Запрос</param>
        /// <returns></returns>
        public void Query(DevCommand Par)
        {
            byte[] PackComand;          
            string Ansewr = Program.data_module.FindCurentVal(Par.device.ToString(), Par.port.ToString());

            if (Ansewr == "Error")
            {//Выполняется если запрос завершился ошибкой
                byte[] QueryAnsewr = new byte[1];
                QueryAnsewr[0] = 0;
                PackComand = ProtocolForParser.Pack(Par.port, Par.device, QueryAnsewr);
                    
                Program.WW.SendInform(QueryAnsewr);
            }
            else
            {//Выполняется если запрос не завершился ошибкой
                int A = Convert.ToInt32(Ansewr);
                Stack st = new Stack();
                while (A > 255)
                {
                    st.Push(A % 256);
                    A = A / 256;
                }
                if (st.Count == 0)
                {
                    st.Push(A);
                }
                else if (A>0)
                {
                    st.Push(A);
                }
                byte[] QueryAnsewr = new byte[st.Count];
                int Count = st.Count;
                for (int i = 0; i < Count; i++)
                {
                    QueryAnsewr[i] = Convert.ToByte(st.Pop());
                }
                QueryAnsewr = ProtocolForParser.Pack(Par.port, Par.device, QueryAnsewr);
                Program.WW.SendInform(QueryAnsewr);
            }
        }
    }
}
