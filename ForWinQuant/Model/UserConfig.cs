using ForWinQuant.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ForWinQuant.Model
{
    public enum ActionStates { 
        ActionStart,
        ActionMaxTrade,
        ActionNoneInit,
        ActionIsRunning,
        ActionGetUserAssetsFailure,
        ActionGetTodayOrderFailure,
        ActionGetNGRCPriceFailure,
        ActionEnd
    }
    class UserConfig:EunexUser
    {
        //单日最大可刷单量上限
        const int MAX_TRADE_COUNT = 240000;
        public MainForm mainForm;
        /// <summary>
        /// 用户当前状态：0正在初始化数据，1初始化完成，2正在刷单
        /// </summary>
        public int currentStates = 0;

        public BindingList<Order> ordersOnline;

        public UserConfig(EunexUser user, MainForm mainForm =null)
        {
            this.id = user.id;
            this.eunex_name = user.eunex_name;
            this.api_key = user.api_key;
            this.api_secret = user.api_secret;
            this.user_id = user.user_id;
            this.auth_date = user.auth_date;
            this.is_working = user.is_working;
            this.day_total = user.day_total;
            if (mainForm != null)
                this.mainForm = mainForm;
            orderToday = new List<Order>();
            DateTimeOffset now = DateTimeOffset.Now;
            this.lateHighPrice = new LateHighPrice
            {
                isChecked = false,
                startTime = new DateTimeOffset(now.Year,now.Month,now.Day,19,0,0,TimeSpan.Zero),
                endTime = new DateTimeOffset(now.Year, now.Month, now.Day, 20, 59, 0, TimeSpan.Zero)
            };
            this.earlyHighPrice = new EarlyHighPrice
            {
                isChecked = false,
                startTime = new DateTimeOffset(now.Year, now.Month, now.Day, 7, 0, 0, TimeSpan.Zero),
                endTime = new DateTimeOffset(now.Year, now.Month, now.Day, 8, 59, 0, TimeSpan.Zero)
            };
            this.twoHigherPrices = new TwoHigherPrices
            {
                isChecked = false,
                startTime = new DateTimeOffset(now.Year, now.Month, now.Day, 18, 0, 0, TimeSpan.Zero),
                endTime = new DateTimeOffset(now.Year, now.Month, now.Day, 8, 59, 0, TimeSpan.Zero)
            };
            this.DealPrompt = false;
            this.OfflinePrompt = false;
            this.retainedAmount = 0.00F;
            this.ordersOnline = new BindingList<Order>();

        }

        /// <summary>
        /// 晚高价选项
        /// </summary>
        public LateHighPrice lateHighPrice { get; set; }

        /// <summary>
        /// 早高价选项
        /// </summary>
        public EarlyHighPrice earlyHighPrice { get; set; }

        /// <summary>
        /// 高二价选项
        /// </summary>
        public TwoHigherPrices twoHigherPrices { get; set; }

        /// <summary>
        /// 成交提示音
        /// </summary>
        public bool DealPrompt { get; set; }

        /// <summary>
        /// 掉线提示音
        /// </summary>
        public bool OfflinePrompt { get; set; }

        /// <summary>
        /// 保留金额USDT
        /// </summary>
        public float retainedAmount = 0.00F;

        /// <summary>
        /// 今日轮次
        /// </summary>
        public int roundToday { get; set; }

        public UserBalances balancesUSDT { get; set; }
        public UserBalances balancesNGRC { get; set; }
        public List<Order> orderToday { get; set; }

        /// <summary>
        /// 当前价格（NGRC）
        /// </summary>
        public float currentPrice = 0;

        public float countTradeToday { get; set; }
        public async Task<bool> initStatus()
        {
            bool initSuccess = false;

            this.mainForm.toolStripStatusLabelStatus.Text = " 状态 查询用户资产";
            do
            {
                initSuccess = await getUserAssets();
                if (!initSuccess && this.mainForm.treeViewSubUsers.SelectedNode.Name == this.eunex_name)
                    await Task.Delay(TimeSpan.FromSeconds(10));
                else if (this.mainForm.treeViewSubUsers.SelectedNode.Name != this.eunex_name)
                    return initSuccess;
            } while (!initSuccess);                
            Log.Info("获取用户资产成功。");

            this.mainForm.toolStripStatusLabelStatus.Text = " 状态 查询当前报价";
            do
            {
                initSuccess = await getNGRCPrice();
                if (!initSuccess && this.mainForm.treeViewSubUsers.SelectedNode.Name == this.eunex_name)
                    await Task.Delay(TimeSpan.FromSeconds(10));
                else if (this.mainForm.treeViewSubUsers.SelectedNode.Name != this.eunex_name)
                    return initSuccess;
            } while (!initSuccess);
            Log.Info("获取当前报价成功。");


            this.mainForm.toolStripStatusLabelStatus.Text = " 状态 查询用户今日订单量";
            do
            {
                initSuccess = await getTodayOrder();
                if (!initSuccess && this.mainForm.treeViewSubUsers.SelectedNode.Name == this.eunex_name)
                    await Task.Delay(TimeSpan.FromSeconds(10));
                else if (this.mainForm.treeViewSubUsers.SelectedNode.Name != this.eunex_name)
                    return initSuccess;
            } while (!initSuccess);


            this.mainForm.toolStripStatusLabelStatus.Text = " 状态 查询用户委托列表";
            do
            {
                initSuccess = await getDelegateList();
                if (!initSuccess && this.mainForm.treeViewSubUsers.SelectedNode.Name == this.eunex_name)
                    await Task.Delay(TimeSpan.FromSeconds(10));
                else if (this.mainForm.treeViewSubUsers.SelectedNode.Name != this.eunex_name)
                    return initSuccess;
            } while (!initSuccess);
            Log.Info("获取委托列表成功。");

            if (this.currentStates == 0) {
                this.currentStates = 1;
                this.mainForm.buttonMine.Text = Properties.Resources.StartAction;
            }
            return initSuccess;
        }

        public async Task<ActionStates> startAction()
        {
            if (this.currentStates == 2)
            {
                return ActionStates.ActionIsRunning;
            }
            else if (this.currentStates == 0)
            {
                return ActionStates.ActionNoneInit;
            }
            this.currentStates = 2;

            var actionInit = await updateAssets();
            if (actionInit != ActionStates.ActionStart) return actionInit;

            if (countTradeToday >= MAX_TRADE_COUNT)
            {
                return ActionStates.ActionMaxTrade;
            }

            bool needUpdateAssets = false;
            if (balancesNGRC.amounts > balancesNGRC.frozen)
            {
                float ask_quantity = (balancesNGRC.amounts - balancesNGRC.frozen) > (MAX_TRADE_COUNT - countTradeToday) ? 
                    (MAX_TRADE_COUNT - countTradeToday) : (balancesNGRC.amounts - balancesNGRC.frozen);
                PostOrder order = new PostOrder { pairSymbol = "7xjyKr", price = this.currentPrice, 
                    quantity = (float)Math.Floor(ask_quantity * 10000) / 10000 };
                var askResult = await EunexHelper.CreateOrder(this.api_key, this.api_secret, "asks", order);
                if (askResult.code == 0)
                {
                    Log.Info("订单(卖出、数量：{0:F4})创建成功，正在请求资产数据更新", ask_quantity);
                    needUpdateAssets = true;
                }
            }

            if (balancesUSDT.amounts > balancesUSDT.frozen) {
                var bid_quantity = (balancesUSDT.amounts - balancesUSDT.frozen) / this.currentPrice > (MAX_TRADE_COUNT - countTradeToday) ? 
                    (MAX_TRADE_COUNT - countTradeToday) : ((balancesUSDT.amounts - balancesUSDT.frozen) / this.currentPrice);
                var bidResult = await EunexHelper.CreateOrder(this.api_key, this.api_secret, "bids", 
                    new PostOrder { pairSymbol = "7xjyKr", price = this.currentPrice, 
                        quantity = (float)Math.Floor(bid_quantity * 10000) / 10000 });
                if (bidResult.code == 0)
                {
                    countTradeToday += bid_quantity;
                    Log.Info("订单(买入、数量：{0:F4})创建成功，正在请求资产数据更新", bid_quantity);
                    needUpdateAssets = true;                   
                }
            }

            if (needUpdateAssets) { var actionCheckDelegate = await updateAssets(); }
            this.currentStates = 1;
            return ActionStates.ActionEnd;
        }

        private async Task<ActionStates> updateAssets()
        {
            bool actionInitSuccess = false;

            this.mainForm.toolStripStatusLabelStatus.Text = " 状态 查询用户资产";

            actionInitSuccess = await getUserAssets();
            if (!actionInitSuccess)
                return ActionStates.ActionGetUserAssetsFailure;


            this.mainForm.toolStripStatusLabelStatus.Text = " 状态 查询当前报价";

            actionInitSuccess = await getNGRCPrice();
            if (!actionInitSuccess) return ActionStates.ActionGetNGRCPriceFailure;



            this.mainForm.toolStripStatusLabelStatus.Text = " 状态 查询用户今日订单量";

            actionInitSuccess = await getTodayOrder();
            if (!actionInitSuccess) return ActionStates.ActionGetTodayOrderFailure;


            this.mainForm.toolStripStatusLabelStatus.Text = " 状态 查询用户委托列表";

            actionInitSuccess = await getDelegateList();
            if (!actionInitSuccess && this.mainForm.treeViewSubUsers.SelectedNode.Name == this.eunex_name)
                Log.Info("获取委托列表失败。");
            return ActionStates.ActionStart;
        }

        /// <summary>
        /// 获取用户资产
        /// </summary>
        /// <returns>是否成功</returns>
        private async Task<bool> getUserAssets()
        {
            try
            {
                ///获取用户资产
                Restful<List<UserBalances>> userBalances = await EunexHelper.GetUserBalances(this.api_key, this.api_secret);
                foreach (UserBalances balances in userBalances.data)
                {
                    if (balances.symbol.Equals("USDT"))
                    {
                        balancesUSDT = balances;
                        if (this.mainForm.treeViewSubUsers.SelectedNode.Name == this.eunex_name)
                            this.mainForm.updateUI(string.Format("可用：{0:f2}，冻结：{1:f2}", balancesUSDT.amounts, balancesUSDT.frozen), "label_USDT_value");
                    }

                    if (balances.symbol.Equals("NGRC"))
                    {
                        balancesNGRC = balances;
                        if (this.mainForm.treeViewSubUsers.SelectedNode.Name == this.eunex_name)
                            this.mainForm.updateUI(string.Format("可用：{0:f2}，冻结：{1:f2}",
                                balancesNGRC.amounts, balancesNGRC.frozen), "label_NGRC_value");
                    }

                }
                this.mainForm.updateUI(
                    string.Format("资产数据最后更新时间：{0:HH:mm:ss}", DateTimeOffset.Now),
                    "toolStripStatusLabelAssetUpdateTime"
                    );
                return true;
        }
            catch(Exception e)
            {
                Log.Error("获取用户{0}资产失败。错误原因：{1}",this.eunex_name,e.Message);
            }
            finally
            {

            }
            return false;
        }

        private async Task<bool> getNGRCPrice()
        {
            try
            {
                /// 获取NGRC报价
                var resJson = await EunexHelper.GetDepthDetails(this.api_key, this.api_secret, "NGRC_USDT");
                float.TryParse(resJson.data["asks"][0][0].ToString(), out currentPrice);
                if (this.mainForm.treeViewSubUsers.SelectedNode.Name == this.eunex_name)
                    this.mainForm.updateUI(currentPrice.ToString(), "labelCurrentPrice");
                return true;

            }catch(Exception e)
            {
                Log.Error("获取用户{0}NGRC报价失败。错误原因：{1}", this.eunex_name, e.Message);
            }
            finally
            {

            }
            return false;
        }

        private async Task<bool> getTodayOrder()
        {
            try
            {
                ///获取并检查用户今日已完成订单量
                var orders = await EunexHelper.GetOrder(this.api_key, this.api_secret, "NGRC_USDT", 20, 0, 50);
                foreach (Order order in orders.data.content)
                {
                    if (!orderToday.Contains(order)) orderToday.Add(order);
                }
                this.countTradeToday = EunexHelper.countOrdersByDay(orders.data.content, DateTimeOffset.Now);
                if (this.mainForm.treeViewSubUsers.SelectedNode.Name == this.eunex_name)
                    this.mainForm.updateUI(countTradeToday.ToString(), "labelCountTradeToday");
                return true;
        }
            catch(Exception e)
            {
                Log.Error("获取用户{0}今日已完成订单量失败。错误原因：{1}", this.eunex_name, e.Message);
            }
            finally
            {

            }
            return false;
        }

        private async Task<bool> getDelegateList()
        {
            try
            {
                var ordersOnLineFind = await EunexHelper.GetOrder(this.api_key, this.api_secret, "NGRC_USDT", 0, 0, 50);
                foreach (Order order in ordersOnLineFind.data.content)
                {
                    if (!ordersOnline.Contains(order)) ordersOnline.Add(order);
                    else { 
                        ordersOnline.Remove(order);
                        ordersOnline.Add(order);
                    }
                }
                
                return true;
        }
            catch(Exception e)
            {
                Log.Error("获取用户{0}委托列表失败。错误原因：{1}", this.eunex_name, e.Message);
            }
            finally
            {

            }
            return false;
        }

        public async void cancelOrder(string orderId)
        {
            try
            {
                var revertOrderResult = await EunexHelper.CancelOrder(this.api_key, this.api_secret, orderId);
                if (revertOrderResult.code == 0)
                {
                    Log.Info(revertOrderResult.data.ToString() + " 撤单成功");
                }
                else
                {
                    Log.Info(revertOrderResult.data.ToString() + " 撤单失败");
                }
            }
            catch(Exception e)
            {
                Log.Error( "撤单失败:{0}，原因：{1}",orderId,e.Message);
            }
            
        }

    }
    class Period
    {
        public DateTimeOffset startTime { get; set; }
        public DateTimeOffset endTime { get; set; }
    }
    class LateHighPrice:Period
    {
        public bool isChecked { get; set; }
    }
    class EarlyHighPrice : Period
    {
        public bool isChecked { get; set; }
    }
    class TwoHigherPrices : Period
    {
        public bool isChecked { get; set; }
    }
}
