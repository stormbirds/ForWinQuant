// ===============================================================================
// Project Name        :    ForWinQuant.Helper
// Project Description :   
// ===============================================================================
// Class Name          :    EunexHelper
// Class Version       :    v1.0.0.0
// Class Description   :   
// Author              :    baojun
// Create Time         :    2020/5/19 10:16:10
// Update Time         :    2020/5/19 10:16:10
// ===============================================================================
// Copyright © BAOJUN5040 2020 . All rights reserved.
// ===============================================================================

using ForWinQuant.Restful;
using Newtonsoft.Json.Linq;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;

namespace ForWinQuant.Helper
{
    public class EunexHelper
    {
        public static float countOrdersByDay(List<Order> orders, DateTimeOffset date)
        {
            float count = 0;
            foreach(Order order in orders)
            {
                DateTimeOffset orderTime = Utils.ToDateTime(order.createdAt).AddHours(-9);
                TimeSpan sp = orderTime.Subtract(date);
                if (order.orderType == 0 && sp.Days == 0)
                {
                    count += order.quantity;
                }
            }
            return count;
        }

        public static Task<AuthModel> Auth(string username,string password)
        {
            var api = HttpRestfulService.ForBaseApi<IUserApi>();
            var user = new JObject { new JProperty("username", username) };
            user.Add("password", password);
            return api.Auth(user);
        }

        #region 定义基础服务接口部分
        public static Task<Restful<JObject>> GetServerTime()
        {
            var api = HttpRestfulService.ForBaseApi<IBaseApi>();
            return api.GetServerTime();
        }

        public static Task<Restful<JObject>> GetCoins()
        {
            var api = HttpRestfulService.ForBaseApi<IBaseApi>();
            return api.GetCoins();
        }

        public static Task<Restful<JObject>> GetPairs()
        {
            var api = HttpRestfulService.ForBaseApi<IBaseApi>();
            return api.GetPairs();
        }
        #endregion

        #region 定义行情接口
        public static Task<Restful<JObject>> GetDepthDetails(string pair_symbol)
        {
            var api = HttpRestfulService.ForBaseApi<IMarketApi>();
            return api.GetDepthDetails(pair_symbol);
        }

        public static Task<Restful<JObject>> GetPairSymbol(string pair_symbol) {
            var api = HttpRestfulService.ForBaseApi<IMarketApi>();
            return api.GetPairSymbol(pair_symbol);
        }
        #endregion

        #region 定义用户信息接口
        public static Task<Restful<List<UserBalances>>> GetUserBalances()
        {
            var api = HttpRestfulService.ForBaseApi<IMembersApi>();
            return api.GetUserBalances();
        }
        #endregion

        #region 定义订单接口
        public static Task<Restful<JObject>> CreateOrder(string type, [Body]PostOrder postOrder)
        {
            var api = HttpRestfulService.ForBaseApi<IOrdersApi>();
            return api.CreateOrder(type,postOrder);
        }

        public static Task<Restful<OrderContent>> GetOrder(string pairSymbol, int states, int pageNum, int pageSize)
        {
            var api = HttpRestfulService.ForBaseApi<IOrdersApi>();
            return api.GetOrder(pairSymbol,states,pageNum,pageSize);
        }

        public static Task<Restful<OrderContent>> GetOrder(string pairSymbol, int pageNum, int pageSize)
        {
            var api = HttpRestfulService.ForBaseApi<IOrdersApi>();
            return api.GetOrder(pairSymbol, pageNum, pageSize);
        }

        public static Task<Restful<Order>> GetOrder(string order_id)
        {
            var api = HttpRestfulService.ForBaseApi<IOrdersApi>();
            return api.GetOrder(order_id);
        }

        public static Task<Restful<JObject>> CancelOrder(string order_id)
        {
            var api = HttpRestfulService.ForBaseApi<IOrdersApi>();
            return api.CancelOrder(order_id);
        }

        public static Task<Restful<JObject>> GetOrderTrade(string order_id, int pageNum, int pageSize)
        {
            var api = HttpRestfulService.ForBaseApi<IOrdersApi>();
            return api.GetOrderTrade(order_id,pageNum,pageSize);
        }
        #endregion

    }
}
