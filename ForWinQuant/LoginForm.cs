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

        public LoginForm()
        {

            InitializeComponent();
        }

        private void button_login_Click(object sender, EventArgs e)
        {
            QuantApi api = new QuantApi();
            var result = api.login(textBox_username.Text, textBox_password.Text);
            MessageBox.Show(result.ToString());
        }
    }
}
