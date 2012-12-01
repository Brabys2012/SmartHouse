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
        /// <summary>
        /// Метод, определения пришел запрос или управляющая команда
        /// </summary>
        /// <param name="Comand">Присланная команда</param>
        public void ParssComand(byte[] Comand)
        {
            if (Comand.Length >= 5)
            {//Выполняется если пришла управляющая команда
                ExeComand(Comand);
            }
            else if (Comand.Length == 2)
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
        public void ExeComand(byte[] Par)
        {
            int tmpcount = 0;
            UInt64 Value = 0;
            int masCounter = Par.Length;
            for (int i = 2; i < masCounter; i++)
            {
                Value = (UInt64)(Value + Par[i] * Math.Pow(256, masCounter - tmpcount));
                tmpcount++;
            }

            byte[] Ansewr = new byte[4];
            Ansewr[0] = 4;
            Ansewr[1] = Par[0];
            Ansewr[2] = Par[1];
            bool result = Program.data_module.UpdDeviceVal(Par[0].ToString(), Par[1].ToString(), Value.ToString());
            if (result)
            {
                Ansewr[3] = 1;
            }
            else
            {
                Ansewr[3] = 0;
            }

            Program.WW.SendInform(Ansewr);
        }

        /// <summary>
        /// Метод, который исполняет запрос
        /// </summary>
        /// <param name="Par">Запрос</param>
        /// <returns></returns>
        public void Query(byte[] Par)
        {
          
            string Ansewr = Program.data_module.FindCurentVal(Par[0].ToString(), Par[1].ToString());

            if (Ansewr == "Error")
            {//Выполняется если запрос завершился ошибкой
                byte[] QueryAnsewr = new byte[4];
                QueryAnsewr[0] = 4;
                QueryAnsewr[1] = Par[0];
                QueryAnsewr[2] = Par[1];
                QueryAnsewr[3] = 0;
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
                byte[] QueryAnsewr = new byte[st.Count+3];
                QueryAnsewr[0] = Convert.ToByte(st.Count + 3);
                QueryAnsewr[1] = Par[0];
                QueryAnsewr[2] = Par[1];
                int Count = st.Count+3;
                for (int i = 3; i < Count; i++)
                {
                    QueryAnsewr[i] = Convert.ToByte(st.Pop());
                }
                Program.WW.SendInform(QueryAnsewr);
            }
        }
    }
}
