using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace ControlsNOBD
{
    static class Program
    {
        static public data_source data_module;
        static public user_settings user_set;
        static public ExexutableComServer WW = new ExexutableComServer();
        static public ComListenServer MCE = new ComListenServer();
        static public Parss_and_deter_mess MyParser;
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            data_module = new data_source();
            user_set = new user_settings();
            WW = new ExexutableComServer();
            MCE = new ComListenServer();
            MyParser = new Parss_and_deter_mess();
            WW.OpenCom();
            Application.Run(new mainForm());
        }
    }
}
