using ForWinQuant.Restful;
using Newtonsoft.Json.Linq;
using Refit;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace ForWinQuant
{
    public partial class MainForm : Form
    {
        


        public delegate void RefreshChartDelegate(List<int> x, List<int> y, string type);

        public MainForm()
        {
            InitializeComponent();

            init();
            
        }

        public void init()
        {
            labelUserName.Text = "账号：";
            label_USDT_value.Text = "可用--，冻结--";
            label_NGRC_value.Text = "可用--，冻结--";
        }

        private async void getUserBalances(string key,string secret)
        {
            HttpRestfulService.API_KEY = key;
            HttpRestfulService.API_SECRET = secret;
            
            var api = HttpRestfulService.ForBaseApi<IMembersApi>();
            try
            {

                //var user = new JObject { new JProperty("username", "test1") };
                //user.Add("password", "000000");
                //var resJson = await api.Auth(user);
                var resJson = await api.GetUserBalances(key);

                //label5.Text = resJson.code.ToString();
                //label6.Text = resJson.data[0].ToString();
                foreach(UserBalances balances in resJson.data)
                {
                    if (balances.symbol.Equals("USDT"))
                        label_USDT_value.Text = string.Format("可用：{0:f2}，冻结：{0:f2}", balances.amounts, balances.frozen);
                    if (balances.symbol.Equals("NGRC"))
                        label_NGRC_value.Text = string.Format("可用：{0:f2}，冻结：{0:f2}", balances.amounts, balances.frozen);
                }
                await Task.Delay(TimeSpan.FromSeconds(5));
                getPrice(key, secret);
            }
            catch (ApiException e)
            {
                MessageBox.Show(e.Message);
            }
        }

        /// <summary>
        /// 获取NGRC报价
        /// </summary>
        /// <param name="key"></param>
        /// <param name="secret"></param>
        private async void getPrice(string key, string secret)
        {
            HttpRestfulService.API_KEY = key;
            HttpRestfulService.API_SECRET = secret;
            //MessageBox.Show(string.Format("key : {0} secret : {1}", key, secret));
            var api = HttpRestfulService.ForBaseApi<IMarketApi>();
            try
            {
                var resJson =await api.GetDepthDetails(key, "NGRC_USDT");
                float ngrcPrice;
                
                labelCurrentPrice.Text = float.TryParse(resJson.data["asks"][0][0].ToString(),out ngrcPrice)?ngrcPrice.ToString():"获取失败";

            }
            catch
            {

            }
        }

        private void buttonMine_Click(object sender, EventArgs e)
        {
            LoginForm loginForm = new LoginForm(this);
            loginForm.Show();
        }

        public void loginStatusChanged(string username, bool logged)
        {
            toolStripStatusLabelLogin.Text = logged ? username + " 已登录 " : username + "登录失败";
            toolStripStatusLabelLogin.ForeColor = logged ? Color.Green : Color.Red;
            if (logged)
            {
                getUserList();
                buttonMine.Text = "开始刷单";
            }
        }

        private async void getUserList()
        {
            var api = HttpRestfulService.ForBaseApi<IUserApi>();
            try
            {
                var resJson = await api.GetEunexUsers();
                var listUser = resJson.data;
                Console.WriteLine(listUser.Count);
                HttpRestfulService.API_KEY = listUser[0].api_key;
                HttpRestfulService.API_SECRET = listUser[0].api_secret;
                labelUserName.Text = string.Format("账户：{0}",listUser[0].eunex_name) ;
                getUserBalances(listUser[0].api_key, listUser[0].api_secret);
            }
            catch(Exception e)
            {
                MessageBox.Show("错误:"+e.Message, "getUserList错误");
            }
        }
        public void RefreshData()
        {
            List<int> x1 = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };
            List<int> y1 = new List<int>();
            Random ra = new Random();
            y1 = new List<int>() {
                ra.Next(1, 10),
                ra.Next(1, 10),
                ra.Next(1, 10),
                ra.Next(1, 10),
                ra.Next(1, 10),
                ra.Next(1, 10),
                ra.Next(1, 10),
                ra.Next(1, 10),
                ra.Next(1, 10),
                ra.Next(1, 10),
                ra.Next(1, 10),
                ra.Next(1, 10)
            };
            RefreshChart(x1, y1, "chart1");
            RefreshChart(x1, y1, "chart2");
            RefreshChart(x1, y1, "chart3");
            RefreshChart(x1, y1, "chart4");
        }

        public void RefreshChart(List<int> x, List<int> y, string type)
        {
            if (type == "chart1")
            {
                if (this.chart1.InvokeRequired)
                {
                    RefreshChartDelegate stcb = new RefreshChartDelegate(RefreshChart);
                    this.Invoke(stcb, new object[] { x, y, type });
                }
                else
                {
                    chart1.Series[0].Points.DataBindXY(x, y);
                    chart1.Series[1].Points.DataBindXY(x, y);
                }
            }
            else if (type == "chart2")
            {
                if (this.chart2.InvokeRequired)
                {
                    RefreshChartDelegate stcb = new RefreshChartDelegate(RefreshChart);
                    this.Invoke(stcb, new object[] { x, y, type });
                }
                else
                {
                    chart2.Series[0].Points.DataBindXY(x, y);
                    List<Color> colors = new List<Color>() {
                        Color.Red,
                        Color.DarkRed,
                        Color.IndianRed,
                        Color.MediumVioletRed,
                        Color.OrangeRed,
                        Color.PaleVioletRed,
                        Color.Purple,
                        Color.DarkOrange,
                        Color.Maroon,
                        Color.LightCoral,
                        Color.LightPink,
                        Color.Magenta
                    };
                    DataPointCollection points = chart2.Series[0].Points;
                    for (int i = 0; i < points.Count; i++)
                    {
                        points[i].Color = colors[i];
                    }
                }
            }
            else if (type == "chart3")
            {
                if (this.chart3.InvokeRequired)
                {
                    RefreshChartDelegate stcb = new RefreshChartDelegate(RefreshChart);
                    this.Invoke(stcb, new object[] { x, y, type });
                }
                else
                {
                    chart3.Series[0].Points.DataBindXY(x, y);
                }
            }
            else if (type == "chart4")
            {
                if (this.chart4.InvokeRequired)
                {
                    RefreshChartDelegate stcb = new RefreshChartDelegate(RefreshChart);
                    this.Invoke(stcb, new object[] { x, y, type });
                }
                else
                {
                    chart4.Series[0].Points.DataBindXY(x, y);
                    List<Color> colors = new List<Color>() {
                        Color.Red,
                        Color.DarkRed,
                        Color.IndianRed,
                        Color.MediumVioletRed,
                        Color.OrangeRed,
                        Color.PaleVioletRed,
                        Color.Purple,
                        Color.DarkOrange,
                        Color.Maroon,
                        Color.LightCoral,
                        Color.LightPink,
                        Color.Magenta
                    };
                    DataPointCollection points = chart4.Series[0].Points;
                    for (int i = 0; i < points.Count; i++)
                    {
                        points[i].Color = colors[i];
                    }
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            new Thread(new ThreadStart(RefreshData)).Start();
        }
    }
}
