using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ControlsNOBD
{
    public class Parss_and_deter_mess
    {
        public void ParssComand(string Comand)
        {
            string[] com = Comand.Split('/');
            for (int i = 0; i < com.Length; i++)
            {
                if (com[i] != "")
                {
                    string[] strs = com[i].Split('.');
                    ExeComand(strs);
                }
            }
        }
        public bool ExeComand(string[] Par)
        {
            string cv = "";
            string comand;
            bool res=false;
            if (Par.Length == 2)
            {
                cv = Program.data_module.FindCurentVal(Par[0], Par[1]);
                if (cv != "Error")
                {
                    comand = Par[0].Replace(" ", "") + "." + Par[1].Replace(" ", "") + "." + cv.Replace(" ", "") + "/";
                    Program.WW.SendInform(comand);
                    res = true;
                }
                else
                {
                    MessageBox.Show("Ошибка");
                    res = false;
                }
            }
            else if (Par.Length == 4)
            {
                int parVal = 256 * Convert.ToInt32(Par[2]) + Convert.ToInt32(Par[3]);
                if ((Convert.ToInt16(Par[2]) <= 255) & (Convert.ToInt16(Par[2]) <= 255))
                {

                    cv = Program.data_module.UpdDeviceVal(Par[0], Par[1], parVal.ToString());
                    if (cv != "Error")
                    {
                        comand = Par[0].Replace(" ", "") + "." + Par[1].Replace(" ", "") + "." + 1 + "/";
                        Program.WW.SendInform(comand);
                        res = true;
                    }
                    else
                    {
                        comand = Par[0].Replace(" ", "") + "." + Par[1].Replace(" ", "") + "." + 0 + "/";
                        Program.WW.SendInform(comand);
                        MessageBox.Show("Ошибка");
                        res = false;
                    }
                }
                else
                {
                    MessageBox.Show("Получен неверный формат команды");
                }
            }
            else
            {
                MessageBox.Show("Получен неверный формат команды");
            }
            return res;
        }
    }
}
