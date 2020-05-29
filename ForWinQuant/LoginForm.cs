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
using System.Reflection;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ForWinQuant
{
    public partial class LoginForm : Form
    {
        //MainForm mainForm = null;


        public LoginForm()
        {
            //this.mainForm = fm;
            InitializeComponent();
            checkBoxFixedPassword.CheckedChanged += CheckBoxFixedPassword_CheckedChanged;
            button_login.Text = "版本检测中";
            this.button_login.Enabled = false;
            getNewVersion();
            

        }

        private void CheckBoxFixedPassword_CheckedChanged(object sender, EventArgs e)
        {

            Properties.Settings.Default.autoLogin = checkBoxFixedPassword.Checked;
            Properties.Settings.Default.Save();
        }

        private async void getNewVersion()
        {
            try
            {
                var api = HttpRestfulService.ForBaseApi<IUpdateApi>();
                var newVersion = await api.GetNewVision();
                
                Version nowVersion = Assembly.GetExecutingAssembly().GetName().Version;
                Version findVersion = new Version(newVersion["ForWinQuant"]["LatestVerison"].ToString());
                if (nowVersion < findVersion && MessageBox.Show(newVersion["ForWinQuant"]["ReleaseNote"].ToString(), "检查到新版本", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string url = newVersion["ForWinQuant"]["Url"].ToString();
                    SaveFileDialog sfd = new SaveFileDialog();
                    sfd.Title = "保存文件";
                    sfd.Filter = "应用程序(*.exe)|*.exe";
                    sfd.FileName = url.Substring(url.LastIndexOf("/") + 1);
                    if (sfd.ShowDialog() == DialogResult.OK) {
                        UpdateForm updateForm = new UpdateForm(url, sfd.FileName);
                        this.DialogResult = updateForm.ShowDialog();
                    }
                    else
                    {
                        this.DialogResult = DialogResult.Cancel;
                    }
                }
                else if (nowVersion < findVersion)
                {
                    this.DialogResult = DialogResult.Cancel;

                }
                else {
                    init();
                    this.button_login.Enabled = true;
                }
            }
            catch
            {

            }
        }
        private void init()
        {
            button_login.Text = "登陆";
            textBox_username.Text = Properties.Settings.Default.username;
            checkBoxFixedPassword.Checked = Properties.Settings.Default.autoLogin;
            if (Properties.Settings.Default.autoLogin)
                textBox_password.Text = Properties.Settings.Default.password;
            
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
                MessageBox.Show("请输入账号密码", "错误");
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

                    Properties.Settings.Default.access_token = auth;
                    
                    Properties.Settings.Default.username = textBox_username.Text;
                    if (checkBoxFixedPassword.Checked)
                        Properties.Settings.Default.password = textBox_password.Text;

                    Properties.Settings.Default.Save(); 
                    this.DialogResult = DialogResult.OK;
                }
                else if (result == 4012)
                {
                    MessageBox.Show("错误", "账号密码错误，请重新登陆。");
                    textBox_username.Focus();
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

        private void LoginForm_FormClosing(object sender, System.Windows.Forms.FormClosingEventArgs e)
        {
            if (this.DialogResult != DialogResult.Cancel && this.DialogResult != DialogResult.OK)
                e.Cancel = true;
        }
    }
}
