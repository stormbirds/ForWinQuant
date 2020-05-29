using ForWinQuant.Restful;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ForWinQuant
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            LoginForm loginForm = new LoginForm();
            DialogResult result = loginForm.ShowDialog();
            if (result == DialogResult.OK)
            {
                AppUtils.mainForm = new MainForm(Properties.Settings.Default.username, Properties.Settings.Default.access_token);
                Application.Run(AppUtils.mainForm);
            }
            else
            {
                Application.Exit();
            }
        }
    }
    public static class AppUtils
    {
        public static MainForm mainForm { get; set; }
    }
}
