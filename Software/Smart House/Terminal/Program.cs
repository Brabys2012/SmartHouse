using System;
using System.Windows.Forms;

namespace RUS_Project.TerminalConfigurator
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmModemCfg(args));
        }
    }
}
