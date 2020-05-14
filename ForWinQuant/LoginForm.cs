using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ForWinQuant
{
    public partial class LoginForm : Form
    {
        MainForm mainForm = null;
        public LoginForm(MainForm fm)
        {
            this.mainForm = fm;
            InitializeComponent();
        }

        private void button_login_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(textBox_username.Text) || String.IsNullOrEmpty(textBox_password.Text)) {
                MessageBox.Show("错误", "请输入账号密码");
                return;
            }
            QuantApi api = new QuantApi(this.mainForm);
            int result = api.login(textBox_username.Text, textBox_password.Text);
            if (result == 0)
            {
                this.mainForm.loginStatusChanged(textBox_username.Text, true);
                this.Close();
            }
            else if (result == 4012)
            {
                MessageBox.Show("错误", "账号密码错误，请重新登陆。");
            }
            else if (result == -1)
            {
                MessageBox.Show("错误", "服务器连接失败，请重试。");
            }
            else {
                MessageBox.Show("错误", "未知错误，请重试。");
            }
        }
    }
}
