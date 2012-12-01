using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

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
            if (Comand.Length == 5)
            {
                ExeComand(Comand);
            }
            else if (Comand.Length == 2)
            {
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
            UInt64 Values =(UInt64) (Par[2] * Math.Pow(256, 2) + Par[3] * 256 + Par[4]);
            byte[] Ansewr = new byte[3];
            Ansewr[0] = Par[0];
            Ansewr[1] = Par[1];
            bool result = Program.data_module.UpdDeviceVal(Par[0].ToString(), Par[1].ToString(), Values.ToString());
            if (result)
            {
                Ansewr[2] = 1;
            }
            else
            {
                Ansewr[2] = 0;
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
            {
                byte[] QueryAnsewr = new byte[3];
                QueryAnsewr[0] = Par[0];
                QueryAnsewr[1] = Par[1];
                QueryAnsewr[2] = 0;
                Program.WW.SendInform(QueryAnsewr);
            }
            else
            {
                int A = Convert.ToInt32(Ansewr);
                byte[] QueryAnsewr = new byte[5];
                QueryAnsewr[0] = Par[0];
                QueryAnsewr[1] = Par[1];
                QueryAnsewr[2] = Convert.ToByte(A/(Math.Pow(256,2)));
                A=Convert.ToInt32((A) % (Math.Pow(256,2)));
                QueryAnsewr[3] = Convert.ToByte(A / 256);
                A = Convert.ToInt32((A) % (256));
                QueryAnsewr[4] = Convert.ToByte(A);
                Program.WW.SendInform(QueryAnsewr);
            }
        }
    }
}
