using ForWinQuant.Helper;
using ForWinQuant.Model;
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
using System.Resources;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace ForWinQuant
{
    
    public partial class MainForm : Form
    {
        private Dictionary<string, UserConfig> userConfigs;
        const string DEBUG_USER = "DEBUG用户";
        private string currentUser = "";
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

        //private List<>
        /// <summary>
        /// 初始化用户状态：-1未初始化，0正在初始化，1初始化完成
        /// </summary>
        private int initStates = -1;
        /// <summary>
        /// 刷单状态：-1关闭，0准备刷单中，1刷单进行中
        /// </summary>
        private int actionStates = -1;

        enum ActivityStatus
        {
            IsRunning,IsStop
        }
        private Dictionary<string, ActivityStatus> usersActionStates;

        /// <summary>
        /// 用户订单列表，用于图表展示
        /// </summary>
        private BindingList<Order> userOrders ;

        public delegate void RefreshChartDelegate(List<int> x, List<int> y, string type);
        delegate void SetTextCallback(string value , string uiName);
        public MainForm(string user,string accessToken)
        {

            this.currentUser = user;
            HttpRestfulService.Access_Token = accessToken;
            InitializeComponent();
            this.notifyIcon1.BalloonTipClosed += (sender, e) => {
                var thisIcon = (NotifyIcon)sender;
                thisIcon.Visible = false;
                thisIcon.Dispose();
            };

            this.Text = Application.ProductName + " " + Application.ProductVersion;

            this.buttonMine.TextChanged += ButtonMine_TextChanged;
            this.treeViewSubUsers.AfterSelect += TreeViewSubUsers_AfterSelect;
            this.buttonMine.Click += buttonMine_Click;
            this.dataGridViewUserOrder.CellFormatting += DataGridViewUserOrder_CellFormatting;
            Log.actionLog = this.textBoxLog;

            toolStripStatusLabelAssetUpdateTime.Alignment = ToolStripItemAlignment.Right;
            this.toolStripStatusLabelAssetUpdateTime.Text = string.Format("资产数据最后更新时间：{0:HH:mm:ss}", DateTimeOffset.Now);

            usersActionStates = new Dictionary<string, ActivityStatus>();
            initUser();
            //InitializeChromium();
        }

        private void ButtonMine_TextChanged(object sender, EventArgs e)
        {
            switch (buttonMine.Text)
            {
                case "开始刷单":
                    buttonMine.BackColor = Color.FromArgb(95, 185, 95);
                    break;
                case "停止刷单":
                    buttonMine.BackColor = Color.FromArgb(250, 90, 105);
                    break;
                default:
                    buttonMine.BackColor = Color.FromArgb(192, 192, 192);
                    break;
            }
        }

        private void TreeViewSubUsers_AfterSelect(object sender, TreeViewEventArgs e)
        {
            labelUserName.Text = string.Format("欧联账户：{0}", e.Node.Name);
            Log.Info(string.Format("切换到欧联账号：{0}", e.Node.Name));
            var userConfig = userConfigs[e.Node.Name];
            if (userConfig.currentStates == 0) { 
                userConfig.initStatus();
                labelCurrentPrice.Text = "-";
                labelTodayRound.Text = "-";
                labelCountTradeToday.Text = "-";
                label_USDT_value.Text = "可用--，冻结--";
                label_NGRC_value.Text = "可用--，冻结--";
                buttonMine.Text = "初始化";
            }
            else
            {
                labelCurrentPrice.Text = userConfig.currentPrice.ToString();
                labelCountTradeToday.Text = userConfig.countTradeToday.ToString();
                label_USDT_value.Text = string.Format("可用：{0:f2}，冻结：{1:f2}", 
                    userConfig.balancesUSDT.amounts, userConfig.balancesUSDT.frozen);
                label_NGRC_value.Text = string.Format("可用：{0:f2}，冻结：{1:f2}",
                                userConfig.balancesNGRC.amounts, userConfig.balancesNGRC.frozen);
                buttonMine.Text = usersActionStates[e.Node.Name] == ActivityStatus.IsStop ? 
                    Properties.Resources.StartAction : Properties.Resources.StopAction; 
            }
            checkBoxLateHighPrice.Checked = userConfig.lateHighPrice.isChecked;
            dateTimePickerLateHighPriceStart.Value = userConfig.lateHighPrice.startTime.DateTime;
            dateTimePickerLateHighPriceEnd.Value = userConfig.lateHighPrice.endTime.DateTime;

            checkBoxEarlyHighPrice.Checked = userConfig.earlyHighPrice.isChecked;
            dateTimePickerEarlyHighPriceStart.Value = userConfig.earlyHighPrice.startTime.DateTime;
            dateTimePickerEarlyHighPriceEnd.Value = userConfig.earlyHighPrice.endTime.DateTime;

            checkBoxTwoHigherPrices.Checked = userConfig.twoHigherPrices.isChecked;
            dateTimePickerTwoHigherPriceStart.Value = userConfig.twoHigherPrices.startTime.DateTime;
            dateTimePickerTwoHigherPriceEnd.Value = userConfig.twoHigherPrices.endTime.DateTime;


            checkBoxDealPrompt.Checked = userConfig.DealPrompt;
            checkBoxOfflinePrompt.Checked = userConfig.OfflinePrompt;
            numericUpDownRetainedAmount.Value = Convert.ToDecimal( userConfig.retainedAmount);

            this.dataGridViewUserOrder.DataSource = userConfig.ordersOnline;

        }

        private void initUser()
        {
            userConfigs = new Dictionary<string, UserConfig>();
            this.buttonMine.Text = "初始化";
            toolStripStatusLabelLogin.Text = this.currentUser + " 已登录 " ;
            toolStripStatusLabelLogin.ForeColor =  Color.Green;
            Log.Info(string.Format( "欢迎{0}", this.currentUser));
            Log.Info("软件正在初始化，请等待初始化完毕再开始刷单...");
            getUserList();
        }
        public void init()
        {

            userOrders = new BindingList<Order>();
            
            this.dataGridViewUserOrder.DataSource = userOrders ;
            labelUserName.Text = "账号：";
            label_USDT_value.Text = "可用--，冻结--";
            label_NGRC_value.Text = "可用--，冻结--";
            this.initStates = -1;

#if DEBUG
            loginStatusChanged(DEBUG_USER, true);
#endif
        }



        private void DataGridViewUserOrder_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;
            if (dgv.Columns[e.ColumnIndex].Name == "orderType" &&
                e.RowIndex >= 0 &&
                dgv["orderType", e.RowIndex].Value is int)
            {
                switch ((int)dgv["orderType", e.RowIndex].Value)
                {
                    case 0:
                        e.Value = "买入";
                        e.FormattingApplied = true;
                        break;
                    case 1:
                        e.Value = "卖出";
                        e.FormattingApplied = true;
                        break;
                }
            }
            if (dgv.Columns[e.ColumnIndex].Name == "createdAt" &&
                e.RowIndex >= 0 &&
                dgv["createdAt", e.RowIndex].Value is long)
            {
                e.Value = Utils.ToDateTime((long)dgv["createdAt", e.RowIndex].Value).ToLocalTime().ToString();
                e.FormattingApplied = true;
            }
        }

        //private async void eunexAction()
        //{

        //    try
        //    {
                
        //        ///获取用户资产
        //        Restful<List<UserBalances>> userBalances = await EunexHelper.GetUserBalances();

        //        foreach (UserBalances balances in userBalances.data)
        //        {
        //            if (balances.symbol.Equals("USDT"))
        //            {
        //                usdt_amount = balances.amounts;
        //                usdt_frozen = balances.frozen;
        //            }

        //            if (balances.symbol.Equals("NGRC"))
        //            {
        //                ngrc_amount = balances.amounts;
        //                ngrc_frozen = balances.frozen;
        //            }

        //        }
        //        updateUI(string.Format("可用：{0:f2}，冻结：{1:f2}", usdt_amount, usdt_frozen), "label_USDT_value");
        //        updateUI(string.Format("可用：{0:f2}，冻结：{1:f2}", ngrc_amount, ngrc_frozen), "label_NGRC_value");

        //        /// 获取NGRC报价
        //        var resJson = await EunexHelper.GetDepthDetails("NGRC_USDT");
        //        var labelCurrentPriceText = float.TryParse(resJson.data["asks"][0][0].ToString(), out ngrc_price) ? ngrc_price.ToString() : "获取失败";
        //        updateUI(labelCurrentPriceText, "labelCurrentPrice");

        //        ///获取并检查用户订单状况
        //        var orders = await EunexHelper.GetOrder("NGRC_USDT" ,20 , 0, 50);
        //        foreach (Order order in orders.data.content) 
        //        { 
        //            if(!userOrders.Contains(order)) userOrders.Add(order);
        //        }
                        
        //        countTradeToday = EunexHelper.countOrdersByDay(orders.data.content, DateTimeOffset.Now);
        //        updateUI(countTradeToday.ToString(), "labelCountTradeToday");

        //        if (countTradeToday >= MAX_TRADE_COUNT) {
        //            stopAction();
        //            return; 
        //        }

        //        if (this.initStates == 0) 
        //        {
        //            updateUI(Properties.Resources.StartAction, "buttonMine");
        //            this.initStates = 1;
        //            this.timerInit.Stop();
        //            return;
        //        }

        //        if (actionStates >= 0)
        //        {
        //            actionStates = 1;
        //            //NGRC有可卖资产
        //            if (ngrc_amount > ngrc_frozen)
        //            {
        //                float ask_quantity = (ngrc_amount - ngrc_frozen) > (MAX_TRADE_COUNT - countTradeToday) ? (MAX_TRADE_COUNT - countTradeToday) : (ngrc_amount - ngrc_frozen) ;
                        
        //                PostOrder order = new PostOrder { pairSymbol = "7xjyKr", price = ngrc_price, quantity = (float)Math.Floor(ask_quantity * 10000) / 10000 };
        //                var askResult = await EunexHelper.CreateOrder("asks", order);
        //                if (askResult.code == 0) 
        //                { 
        //                    var askOrderResult = await EunexHelper.GetOrder(askResult.data["orderId"].ToString());
        //                    if (askOrderResult.code == 0)
        //                    {
        //                        if (!userOrders.Contains(askOrderResult.data)) userOrders.Add(askOrderResult.data);
        //                    }
        //                }
        //            }
                    
        //            if (usdt_amount > usdt_frozen)
        //            {
        //                var bid_quantity = (usdt_amount - usdt_frozen) / ngrc_price > (MAX_TRADE_COUNT - countTradeToday) ? (MAX_TRADE_COUNT - countTradeToday) : ((usdt_amount - usdt_frozen) / ngrc_price);

        //                var bidResult = await EunexHelper.CreateOrder("bids", new PostOrder { pairSymbol = "7xjyKr", price = ngrc_price, quantity = (float)Math.Floor(bid_quantity * 10000) / 10000 });

        //                if (bidResult.code == 0) 
        //                { 
        //                    countTradeToday += bid_quantity;

        //                    var bidOrderResult = await EunexHelper.GetOrder(bidResult.data["orderId"].ToString());

        //                    if (bidOrderResult.code == 0)
        //                    {
        //                        if (!userOrders.Contains(bidOrderResult.data)) userOrders.Add(bidOrderResult.data);
        //                    }
        //                }
        //            }
                    

        //        }
        //    }
        //    catch(Exception e)
        //    {
        //        MessageBox.Show(e.Message,"错误");
        //    }
        //    finally
        //    {

        //    }
           

        //}

        /// <summary>
        /// 停止刷单
        /// </summary>
        private void stopAction()
        {
            this.timerAction.Enabled = false;
            actionStates = -1;
            updateUI(Properties.Resources.StartAction, "buttonMine");
        }

        
        
        private void buttonMine_Click(object sender, EventArgs e)
        {
            if (buttonMine.Text == Properties.Resources.StartAction)
            {
                
                buttonMine.Text = "停止刷单";
                usersActionStates[this.treeViewSubUsers.SelectedNode.Name] = ActivityStatus.IsRunning;
                    
                timerAction.Enabled = true;
                Log.Info("用户 {0} 开始自动刷单",this.treeViewSubUsers.SelectedNode.Name);
            }
            else if (buttonMine.Text == "初始化") 
            {
                MessageBox.Show("正在初始化用户数据，请稍等数据初始化完成。", "提示");
            }
            else if(buttonMine.Text == "停止刷单")
            {
                usersActionStates[this.treeViewSubUsers.SelectedNode.Name] = ActivityStatus.IsStop;
                
                buttonMine.Text = Properties.Resources.StartAction;
                Log.Info("用户 {0} 停止了自动刷单", this.treeViewSubUsers.SelectedNode.Name);
            }
        }

        /// <summary>
        /// 登录状态变更
        /// </summary>
        /// <param name="username"></param>
        /// <param name="logged"></param>
        public void loginStatusChanged(string username, bool logged)
        {
            toolStripStatusLabelLogin.Text = logged ? username + " 已登录 " : username + "退出登录";
            labelUserName.Text = string.Format("账户：{0}", username);
            toolStripStatusLabelLogin.ForeColor = logged ? Color.Green : Color.Red;
            if (logged)
            {
                buttonMine.Text = "初始化";
                this.initStates = 0;
                
                this.timerInit.Start();
                getUserList();
            }
        }

        /// <summary>
        /// 获取欧联账号列表
        /// </summary>
        private async void getUserList()
        {
            Log.Info("获取欧联子账户");

            var api = HttpRestfulService.ForBaseApi<IUserApi>();
            try
            {
                var resJson = await api.GetEunexUsers();
                var listUser = resJson.data;
                if (listUser.Count > 0)
                {
                    
                    foreach (EunexUser user in listUser)
                    {
                        userConfigs.Add(user.eunex_name, new UserConfig(user,this));
                        TreeNode tn = new TreeNode();
                        tn.Name = user.eunex_name;
                        tn.Text = user.eunex_name;
                        treeViewSubUsers.Nodes.Add(tn);
                        usersActionStates.Add(user.eunex_name, ActivityStatus.IsStop);
                    }
                    Log.Info("获取欧联子账户成功");

                    treeViewSubUsers.ExpandAll();
                    treeViewSubUsers.SelectedNode=treeViewSubUsers.Nodes[0];
                    
                }
                else {
                    Log.Info("该账户下没有欧联账户，请先新建欧联账户后再重试。");
                }

            }
            catch(Exception e)
            {
                Log.Error("{0}错误:"+e.Message, "getUserList");
            }
        }


        public async void debugTest()
        {
            await Task.Delay(TimeSpan.FromSeconds(3));
            updateUI( new Random().Next(99999).ToString(), "labelUserName");
        }

        public void updateUI(string value, string uiName)
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
                case "toolStripStatusLabelAssetUpdateTime":
                    if (InvokeRequired)
                    {
                        if (this.toolStripStatusLabelAssetUpdateTime.IsDisposed)
                            return;
                        SetTextCallback callback = new SetTextCallback(updateUI);
                        this.BeginInvoke(callback, new object[] { value, uiName });
                    }
                    else
                    {
                        this.toolStripStatusLabelAssetUpdateTime.Text = value;
                    }
                    break;
                case "toolStripStatusLabelStatus":
                    if (InvokeRequired)
                    {
                        if (this.toolStripStatusLabelStatus.IsDisposed)
                            return;
                        SetTextCallback callback = new SetTextCallback(updateUI);
                        this.BeginInvoke(callback, new object[] { value, uiName });
                    }
                    else
                    {
                        if (this.toolStripStatusLabelStatus.IsDisposed)
                            return;
                        this.toolStripStatusLabelStatus.Text = value;
                    }
                    break;
                case "requestStart":
                    if (InvokeRequired)
                    {
                        if (this.toolStripProgressBarRequest.IsDisposed)
                            return;
                        SetTextCallback callback = new SetTextCallback(updateUI);
                        this.BeginInvoke(callback, new object[] { value, uiName });
                    }
                    else
                    {
                        if (this.toolStripProgressBarRequest.IsDisposed)
                            return;
                        this.toolStripProgressBarRequest.Visible=true;
                        
                    }
                    break;
                case "requestEnd":
                    if (InvokeRequired)
                    {
                        if (this.toolStripProgressBarRequest.IsDisposed)
                            return;
                        SetTextCallback callback = new SetTextCallback(updateUI);
                        this.BeginInvoke(callback, new object[] { value, uiName });
                    }
                    else
                    {
                        if (this.toolStripProgressBarRequest.IsDisposed)
                            return;
                        this.toolStripProgressBarRequest.Visible = false;

                    }
                    break;
                default:
                    break;

            }

        }
        //public void RefreshData()
        //{
        //    List<int> x1 = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };
        //    List<int> y1 = new List<int>();
        //    Random ra = new Random();
        //    y1 = new List<int>() {
        //        ra.Next(1, 10),
        //        ra.Next(1, 10),
        //        ra.Next(1, 10),
        //        ra.Next(1, 10),
        //        ra.Next(1, 10),
        //        ra.Next(1, 10),
        //        ra.Next(1, 10),
        //        ra.Next(1, 10),
        //        ra.Next(1, 10),
        //        ra.Next(1, 10),
        //        ra.Next(1, 10),
        //        ra.Next(1, 10)
        //    };
        //    RefreshChart(x1, y1, "chart1");
        //    RefreshChart(x1, y1, "chart2");
        //    RefreshChart(x1, y1, "chart3");
        //    RefreshChart(x1, y1, "chart4");
        //}

        //public void RefreshChart(List<int> x, List<int> y, string type)
        //{
        //    if (type == "chart1")
        //    {
        //        if (this.chart1.InvokeRequired)
        //        {
        //            RefreshChartDelegate stcb = new RefreshChartDelegate(RefreshChart);
        //            this.Invoke(stcb, new object[] { x, y, type });
        //        }
        //        else
        //        {
        //            chart1.Series[0].Points.DataBindXY(x, y);
        //            chart1.Series[1].Points.DataBindXY(x, y);
        //        }
        //    }
        //    else if (type == "chart2")
        //    {
        //        if (this.chart2.InvokeRequired)
        //        {
        //            RefreshChartDelegate stcb = new RefreshChartDelegate(RefreshChart);
        //            this.Invoke(stcb, new object[] { x, y, type });
        //        }
        //        else
        //        {
        //            chart2.Series[0].Points.DataBindXY(x, y);
        //            List<Color> colors = new List<Color>() {
        //                Color.Red,
        //                Color.DarkRed,
        //                Color.IndianRed,
        //                Color.MediumVioletRed,
        //                Color.OrangeRed,
        //                Color.PaleVioletRed,
        //                Color.Purple,
        //                Color.DarkOrange,
        //                Color.Maroon,
        //                Color.LightCoral,
        //                Color.LightPink,
        //                Color.Magenta
        //            };
        //            DataPointCollection points = chart2.Series[0].Points;
        //            for (int i = 0; i < points.Count; i++)
        //            {
        //                points[i].Color = colors[i];
        //            }
        //        }
        //    }
        //    else if (type == "chart3")
        //    {
        //        if (this.chart3.InvokeRequired)
        //        {
        //            RefreshChartDelegate stcb = new RefreshChartDelegate(RefreshChart);
        //            this.Invoke(stcb, new object[] { x, y, type });
        //        }
        //        else
        //        {
        //            chart3.Series[0].Points.DataBindXY(x, y);
        //        }
        //    }
        //    else if (type == "chart4")
        //    {
        //        if (this.chart4.InvokeRequired)
        //        {
        //            RefreshChartDelegate stcb = new RefreshChartDelegate(RefreshChart);
        //            this.Invoke(stcb, new object[] { x, y, type });
        //        }
        //        else
        //        {
        //            chart4.Series[0].Points.DataBindXY(x, y);
        //            List<Color> colors = new List<Color>() {
        //                Color.Red,
        //                Color.DarkRed,
        //                Color.IndianRed,
        //                Color.MediumVioletRed,
        //                Color.OrangeRed,
        //                Color.PaleVioletRed,
        //                Color.Purple,
        //                Color.DarkOrange,
        //                Color.Maroon,
        //                Color.LightCoral,
        //                Color.LightPink,
        //                Color.Magenta
        //            };
        //            DataPointCollection points = chart4.Series[0].Points;
        //            for (int i = 0; i < points.Count; i++)
        //            {
        //                points[i].Color = colors[i];
        //            }
        //        }
        //    }
        //}


        //private void timerChart_Tick(object sender, EventArgs e)
        //{
        //    new Thread(new ThreadStart(RefreshData)).Start();
        //}

        private async void timerAction_Tick(object sender, EventArgs e)
        {
            foreach(string userName in usersActionStates.Keys)
            {
                if (usersActionStates[userName] == ActivityStatus.IsRunning)
                {
                    var actionResult = await userConfigs[this.treeViewSubUsers.SelectedNode.Name].startAction();
                    switch (actionResult)
                    {
                        case ActionStates.ActionEnd:
                            Log.Info("用户 {0} 自动刷单循环完成", userName);
                            this.toolStripStatusLabelStatus.Text = " 状态 准备就绪";
                            break;
                        case ActionStates.ActionStart:
                            Log.Info("用户 {0} 自动刷单循环开始", userName);
                            this.toolStripStatusLabelStatus.Text = " 状态 准备就绪";
                            break;
                        case ActionStates.ActionMaxTrade:
                            Log.Info("用户 {0} 自动刷单已超出当日最大交易量，请于明天继续",userName);
                            this.toolStripStatusLabelStatus.Text = " 状态 准备就绪";
                            break;
                        case ActionStates.ActionNoneInit:
                            //Log.Info("未完成用户初始化，请等待");
                            this.toolStripStatusLabelStatus.Text = " 状态 准备就绪";
                            break;
                        case ActionStates.ActionIsRunning:
                            //Log.Info("自动刷单已在进行中");
                            this.toolStripStatusLabelStatus.Text = " 状态 准备就绪";
                            break;
                        case ActionStates.ActionGetUserAssetsFailure:
                            //Log.Info("获取用户资产失败");
                            this.toolStripStatusLabelStatus.Text = " 状态 准备就绪";
                            break;
                        case ActionStates.ActionGetTodayOrderFailure:
                            //Log.Info("获取今日订单量失败");
                            this.toolStripStatusLabelStatus.Text = " 状态 准备就绪";
                            break;
                        case ActionStates.ActionGetNGRCPriceFailure:
                            //Log.Info("获取报价失败");
                            this.toolStripStatusLabelStatus.Text = " 状态 准备就绪";
                            break;
                        default:
                            //Log.Info("自动刷单失败，等待下次循环");
                            this.toolStripStatusLabelStatus.Text = " 状态 准备就绪";
                            break;
                    }
                }
            }
            
            
        }

        private void timerInit_Tick(object sender, EventArgs e)
        {
            //new Thread(new ThreadStart(eunexAction)).Start();

        }

        private void dataGridViewUserOrder_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridViewUserOrder.Columns[e.ColumnIndex].Name == "orderRevert" && e.RowIndex >= 0)
            {
                var row = dataGridViewUserOrder.Rows[e.RowIndex];
                var cellOrderId = row.Cells["orderId"].Value.ToString();
                userConfigs[treeViewSubUsers.SelectedNode.Name].cancelOrder(cellOrderId);
                
            }
        }


        /// <summary>
        /// 同步设置到用户
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SynchronizeAdvancedSettings_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var userConfig = userConfigs[this.treeViewSubUsers.SelectedNode.Name];
            userConfig.lateHighPrice = new LateHighPrice
            {
                isChecked = checkBoxLateHighPrice.Checked,
                startTime = dateTimePickerLateHighPriceStart.Value,
                endTime = dateTimePickerLateHighPriceEnd.Value
            };
            userConfig.earlyHighPrice = new EarlyHighPrice
            {
                isChecked = checkBoxEarlyHighPrice.Checked,
                startTime = dateTimePickerEarlyHighPriceStart.Value,
                endTime = dateTimePickerEarlyHighPriceEnd.Value
            };
            userConfig.twoHigherPrices = new TwoHigherPrices
            {
                isChecked = checkBoxTwoHigherPrices.Checked,
                startTime = dateTimePickerTwoHigherPriceStart.Value,
                endTime = dateTimePickerTwoHigherPriceEnd.Value
            };
            userConfig.DealPrompt = checkBoxDealPrompt.Checked;
            userConfig.OfflinePrompt = checkBoxOfflinePrompt.Checked;
            float.TryParse(numericUpDownRetainedAmount.Value.ToString(), out userConfig.retainedAmount);
            Log.Info("同步设置到用户：{0}", userConfig.eunex_name);
        }

        private void buttonMine_MouseMove(object sender, MouseEventArgs e)
        {
            switch (buttonMine.Text)
            {
                case "开始刷单":
                    buttonMine.BackColor = Color.FromArgb(60,145,65);
                    break;
                case "停止刷单":
                    buttonMine.BackColor = Color.FromArgb(255, 115, 125);
                    break;
                default:
                    buttonMine.BackColor = Color.FromArgb(200,200,200);
                    break;
            }
        }

        private void buttonMine_MouseLeave(object sender, EventArgs e)
        {
            switch (buttonMine.Text)
            {
                case "开始刷单":
                    buttonMine.BackColor = Color.FromArgb(95, 185, 95);
                    break;
                case "停止刷单":
                    buttonMine.BackColor = Color.FromArgb(250, 90, 105);
                    break;
                default:
                    buttonMine.BackColor = Color.FromArgb(192, 192, 192);
                    break;
            }
        }

        
    }
}
