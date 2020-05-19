using ForWinQuant.Helper;
using ForWinQuant.Restful;
using Newtonsoft.Json.Linq;
using Refit;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
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

        const string DEBUG_USER = "DEBUG用户";
        //单日最大可刷单量上限
        const int MAX_TRADE_COUNT = 240000;
        //今天已刷单量
        private float countTradeToday = 0;
        private float ngrc_amount = 0;
        private float ngrc_frozen = 0;
        private float usdt_amount = 0;
        private float usdt_frozen = 0;
        //ngrc当前价格
        private float ngrc_price = 0;

        private int initStates = -1;

        private List<Order> userOrders = new List<Order>(); 

        public delegate void RefreshChartDelegate(List<int> x, List<int> y, string type);
        delegate void SetTextCallback(string value , string uiName);
        public MainForm()
        {
            InitializeComponent();

            init();
            
        }

        public void init()
        {
            this.dataGridViewUserOrder.DataSource = userOrders;
            userOrders.Add(new Order());
            labelUserName.Text = "账号：";
            label_USDT_value.Text = "可用--，冻结--";
            label_NGRC_value.Text = "可用--，冻结--";


            eunexAction();
#if DEBUG
            loginStatusChanged(DEBUG_USER, true);
#endif
        }

        private async void eunexAction()
        {
            
            try
            {
                ///获取用户资产
                Restful<List<UserBalances>> userBalances = await EunexHelper.GetUserBalances();
                foreach (UserBalances balances in userBalances.data)
                {
                    if (balances.symbol.Equals("USDT"))
                    {
                        usdt_amount = balances.amounts;
                        usdt_frozen = balances.frozen;
                    }

                    if (balances.symbol.Equals("NGRC"))
                    {
                        ngrc_amount = balances.amounts;
                        ngrc_frozen = balances.frozen;
                    }

                }
                updateUI(string.Format("可用：{0:f2}，冻结：{0:f2}", usdt_amount, usdt_frozen), "label_USDT_value");
                updateUI(string.Format("可用：{0:f2}，冻结：{0:f2}", ngrc_amount, ngrc_frozen), "label_NGRC_value");


                await Task.Delay(TimeSpan.FromSeconds(5));
                /// 获取NGRC报价
                var resJson = await EunexHelper.GetDepthDetails("NGRC_USDT");
                var labelCurrentPriceText = float.TryParse(resJson.data["asks"][0][0].ToString(), out ngrc_price) ? ngrc_price.ToString() : "获取失败";
                updateUI(labelCurrentPriceText, "labelCurrentPrice");

                await Task.Delay(TimeSpan.FromSeconds(5));
                ///获取并检查用户订单状况
                var orders = await EunexHelper.GetOrder("NGRC_USDT", 20, 0, 50);
                countTradeToday = EunexHelper.countOrdersByDay(orders.data.content, new DateTimeOffset().AddDays(-1));
                updateUI(countTradeToday.ToString(), "labelCountTradeToday");

                if (countTradeToday >= MAX_TRADE_COUNT) {
                    stopAction();
                    return; 
                }
                //NGRC有可卖资产
                if (ngrc_amount > ngrc_frozen)
                {
                    var ask_quantity = (ngrc_amount - ngrc_frozen) > (MAX_TRADE_COUNT - countTradeToday) ? (MAX_TRADE_COUNT - countTradeToday) : (ngrc_amount - ngrc_frozen);

                    var askResult = await EunexHelper.CreateOrder("asks", new PostOrder { pairSymbol = "7xjyKr", price = ngrc_price, quantity = ask_quantity });
                    if (askResult.code == 0) countTradeToday += ask_quantity;
                }
                await Task.Delay(TimeSpan.FromSeconds(5));
                if (usdt_amount > usdt_frozen)
                {
                    var bid_quantity = (usdt_amount - usdt_frozen) / ngrc_price > (MAX_TRADE_COUNT - countTradeToday) ? (MAX_TRADE_COUNT - countTradeToday) : ((usdt_amount - usdt_frozen) / ngrc_price);
                    var bidResult = await EunexHelper.CreateOrder("bids", new PostOrder { pairSymbol = "7xjyKr", price = ngrc_price, quantity = bid_quantity });
                    if (bidResult.code == 0) countTradeToday += bid_quantity;
                }
                await Task.Delay(TimeSpan.FromSeconds(5));
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message,"错误");
            }
           

        }

        /// <summary>
        /// 停止刷单
        /// </summary>
        private void stopAction()
        {
            this.timerAction.Enabled = false;
            updateUI("开始刷单", "buttonMine");
        }

        private async void getUserBalances(string key, string secret)
        {
            HttpRestfulService.API_KEY = key;
            HttpRestfulService.API_SECRET = secret;

            var api = HttpRestfulService.ForBaseApi<IMembersApi>();
            try
            {

                //var user = new JObject { new JProperty("username", "test1") };
                //user.Add("password", "000000");
                //var resJson = await api.Auth(user);
                var resJson = await api.GetUserBalances();

                //label5.Text = resJson.code.ToString();
                //label6.Text = resJson.data[0].ToString();
                foreach (UserBalances balances in resJson.data)
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
                var resJson =await api.GetDepthDetails( "NGRC_USDT");
                
                labelCurrentPrice.Text = float.TryParse(resJson.data["asks"][0][0].ToString(),out ngrc_price)? ngrc_price.ToString():"获取失败";
                await Task.Delay(TimeSpan.FromSeconds(5));
                getTodayTrading(key, secret);
            }
            catch
            {

            }
        }

        private async void getTodayTrading(string key, string secret)
        {
            HttpRestfulService.API_KEY = key;
            HttpRestfulService.API_SECRET = secret;
            var api = HttpRestfulService.ForBaseApi<IOrdersApi>();
            try
            {
                var resJson = await api.GetOrder("NGRC_USDT",20,0,50);
                resJson.data.ToString();
                countTradeToday = EunexHelper.countOrdersByDay(resJson.data.content,new DateTimeOffset().AddDays(-1));
                labelCountTradeToday.Text = countTradeToday.ToString();
            }
            catch
            {

            }
        }

        private void buttonMine_Click(object sender, EventArgs e)
        {
            if (buttonMine.Text == "登录")
            {
                LoginForm loginForm = new LoginForm(this);
                loginForm.Show();
            }
            else if (buttonMine.Text == "开始刷单")
            {
                if (countTradeToday >= MAX_TRADE_COUNT)
                {
                    timerAction.Enabled = false;
                    MessageBox.Show("你今日的刷单总量已达单日最大刷单量（240000），请于明日再刷单。", "提示");
                    return;
                }
                timerAction.Enabled = true;
            }
            else if (buttonMine.Text == "初始化") {
                MessageBox.Show("正在初始化用户数据，请稍等数据初始化完成。", "提示");
            }
        }

        /// <summary>
        /// 登录状态变更
        /// </summary>
        /// <param name="username"></param>
        /// <param name="logged"></param>
        public void loginStatusChanged(string username, bool logged)
        {
            toolStripStatusLabelLogin.Text = logged ? username + " 已登录 " : username + "登录失败";
            toolStripStatusLabelLogin.ForeColor = logged ? Color.Green : Color.Red;
            if (logged)
            {
                //getUserList();
                buttonMine.Text = "初始化";
            }
        }

        private async void getUserList()
        {
#if DEBUG
            try {
                labelUserName.Text = string.Format("账户：{0}", DEBUG_USER);
                await Task.Delay(TimeSpan.FromSeconds(3));
                getUserBalances(HttpRestfulService.API_KEY, HttpRestfulService.API_SECRET);
#else
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
#endif
            }
            catch(Exception e)
            {
                Console.WriteLine("{}错误:"+e.Message, "getUserList");
            }
        }

        public async void debugTest()
        {
            await Task.Delay(TimeSpan.FromSeconds(3));
            updateUI( new Random().Next(99999).ToString(), "labelUserName");
        }

        private void updateUI(string value, string uiName)
        {
            switch (uiName)
            {
                case "labelUserName":
                    if (this.labelUserName.InvokeRequired)
                    {
                        if (this.labelUserName.Disposing || this.labelUserName.IsDisposed)
                            return;
                        SetTextCallback callback = new SetTextCallback(updateUI);
                        this.labelUserName.Invoke(callback, new object[] { value, uiName });
                    }
                    else
                    {
                        labelUserName.Text = value;
                    }
                    break;
                case "labelCurrentPrice"://NGRC当前价格
                    if (this.labelCurrentPrice.InvokeRequired)
                    {
                        if (this.labelCurrentPrice.Disposing || this.labelCurrentPrice.IsDisposed)
                            return;
                        SetTextCallback callback = new SetTextCallback(updateUI);
                        this.labelCurrentPrice.Invoke(callback, new object[] { value, uiName });
                    }
                    else
                    {
                        labelCurrentPrice.Text = value;
                    }
                    break;
                case "labelTodayRound": // 今日轮次
                    if (this.labelTodayRound.InvokeRequired)
                    {
                        if (this.labelTodayRound.Disposing || this.labelTodayRound.IsDisposed)
                            return;
                        SetTextCallback callback = new SetTextCallback(updateUI);
                        this.labelTodayRound.Invoke(callback, new object[] { value, uiName });
                    }
                    else
                    {
                        labelTodayRound.Text = value;
                    }
                    break;
                case "labelCountTradeToday"://今日已刷单数
                    if (this.labelCountTradeToday.InvokeRequired)
                    {
                        if (this.labelCountTradeToday.Disposing || this.labelCountTradeToday.IsDisposed)
                            return;
                        SetTextCallback callback = new SetTextCallback(updateUI);
                        this.labelCountTradeToday.Invoke(callback, new object[] { value, uiName });
                    }
                    else
                    {
                        labelCountTradeToday.Text = value;
                    }
                    break;
                case "label_USDT_value": //账户USDT资产
                    if (this.label_USDT_value.InvokeRequired)
                    {
                        if (this.label_USDT_value.Disposing || this.label_USDT_value.IsDisposed)
                            return;
                        SetTextCallback callback = new SetTextCallback(updateUI);
                        this.label_USDT_value.Invoke(callback, new object[] { value, uiName });
                    }
                    else
                    {
                        label_USDT_value.Text = value;
                    }
                    break;
                case "label_NGRC_value"://账户NGRC资产
                    if (this.label_NGRC_value.InvokeRequired)
                    {
                        if (this.label_NGRC_value.Disposing || this.label_NGRC_value.IsDisposed)
                            return;
                        SetTextCallback callback = new SetTextCallback(updateUI);
                        this.label_NGRC_value.Invoke(callback, new object[] { value, uiName });
                    }
                    else
                    {
                        label_NGRC_value.Text = value;
                    }
                    break;
                case "buttonMine"://登陆按钮
                    if (this.buttonMine.InvokeRequired)
                    {
                        if (this.buttonMine.Disposing || this.buttonMine.IsDisposed)
                            return;
                        SetTextCallback callback = new SetTextCallback(updateUI);
                        this.buttonMine.Invoke(callback, new object[] { value, uiName });
                    }
                    else
                    {
                        buttonMine.Text = value;
                    }
                    break;
                case "2":
                    break;
                case "3":
                    break;
                case "4":
                    break;
                default:
                    break;

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

        private void timerChart_Tick(object sender, EventArgs e)
        {
            new Thread(new ThreadStart(RefreshData)).Start();
        }

        private void timerAction_Tick(object sender, EventArgs e)
        {
            new Thread(new ThreadStart(eunexAction)).Start();
        }

        private void timerDebug_Tick(object sender, EventArgs e)
        {
            new Thread(new ThreadStart(debugTest)).Start();
        }
    }
}
