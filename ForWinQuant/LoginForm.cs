using ForWinQuant.Restful;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
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
            init();
        }

        private void init()
        {
            button_login.Text = "登陆";
        }
        private void button_login_Click(object sender, EventArgs e)
        {

            //QuantApi api = new QuantApi(this.mainForm);
            //int result = api.login(textBox_username.Text, textBox_password.Text);
            login();
        }

        private void changeStatesOnLogin()
        {
            button_login.Text = "登陆中···";

        }
    
        private async void login()
        {
            changeStatesOnLogin();
            var api = HttpRestfulService.ForBaseApi<IUserApi>();
            int result = -1;

            if (String.IsNullOrEmpty(textBox_username.Text) || String.IsNullOrEmpty(textBox_password.Text))
            {
                MessageBox.Show("错误", "请输入账号密码");
                init();
                return;
            }

            try
            {
                var user = new JObject { new JProperty("username", textBox_username.Text) };
                user.Add("password", textBox_password.Text);
                var resJson = await api.Auth(user);
                result = resJson.code;
                if (result == 0)
                {
                    string auth = resJson.access_token;
                    Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                    Properties.Settings.Default.access_token = auth;
                    config.Save(ConfigurationSaveMode.Modified);
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
                else
                {
                    MessageBox.Show("错误", "未知错误，请重试。");
                }
            }
            catch
            {

            }
            finally
            {
                init();
            }

        }
    }
}
