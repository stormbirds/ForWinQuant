// ===============================================================================
// Project Name        :    ForWinQuant
// Project Description :   
// ===============================================================================
// Class Name          :    IOrdersApi
// Class Version       :    v1.0.0.0
// Class Description   :   
// Author              :    baojun
// Create Time         :    2020/5/15 15:47:29
// Update Time         :    2020/5/15 15:47:29
// ===============================================================================
// Copyright © BAOJUN5040 2020 . All rights reserved.
// ===============================================================================

using Newtonsoft.Json.Linq;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForWinQuant
{
    public interface IOrdersApi
    {
        /// <summary>
        /// 创建订单
        /// </summary>
        /// <param name="api_id"></param>
        /// <param name="postOrder"></param>
        /// <returns></returns>
        [Post("/{type}")]
        Task<Restful<JObject>> CreateOrder(string api_id, string secret_key,string type, [Body(BodySerializationMethod.Serialized)]PostOrder postOrder);

        /// <summary>
        /// 查询我的订单列表
        /// </summary>
        /// <param name="api_id"></param>
        /// <param name="pairSymbol">可用交易对</param>
        /// <param name="states">状态</param>
        /// <param name="pageNum">从第几⻚</param>
        /// <param name="pageSize">分⻚</param>
        /// <returns></returns>
        [Get("")]
        Task<Restful<OrderContent>> GetOrder(string api_id, string secret_key,string pairSymbol, int states, int pageNum, int pageSize);

        /// <summary>
        /// 查询我的所有订单列表
        /// </summary>
        /// <param name="pairSymbol"></param>
        /// <param name="pageNum"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [Get("")]
        Task<Restful<OrderContent>> GetOrder(string api_id, string secret_key,string pairSymbol, int pageNum, int pageSize);

        /// <summary>
        /// 查询指定订单
        /// </summary>
        /// <param name="api_id"></param>
        /// <param name="order_id"></param>
        /// <returns></returns>
        [Get("/{order_id}")]
        Task<Restful<Order>> GetOrder(string api_id, string secret_key,string order_id);

        /// <summary>
        /// 申请撤销订单
        /// </summary>
        /// <param name="api_id"></param>
        /// <param name="order_id"></param>
        /// <returns></returns>
        [Post("/{order_id}/cancellation")]
        Task<Restful<JObject>> CancelOrder(string api_id, string secret_key,string order_id);

        /// <summary>
        /// 查询指定订单的成交记录
        /// </summary>
        /// <param name="api_id"></param>
        /// <param name="order_id"></param>
        /// <param name="pageNum"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [Get("/{order_id}/trade-histories")]
        Task<Restful<JObject>> GetOrderTrade(string api_id, string secret_key,string order_id, int pageNum, int pageSize);
    }

    public class PostOrder
    {
        public string pairSymbol { set; get; }
        public float price { set; get; }
        public float quantity { set; get; }
    }

    public class OrderContent
    {
        public List<Order> content { get; set; }
    }

    public class Order
    {
        public long id { set; get; }
        public string orderId { set; get; }
        public string orderKey { set; get; }
        public long memberId { set; get; }
        public string memberKey { set; get; }
        public string usr { set; get; }
        public string clientId { set; get; }
        public int orderType { set; get; }
        public string pairCode { set; get; }
        public string tradeCoinKey { set; get; }
        public string priceCoinKey { set; get; }
        public int priceType { set; get; }
        public decimal price { set; get; }
        public float quantity { set; get; }
        public float lastQuantity { set; get; }
        public int orderStatus { set; get; }
        public bool matched { set; get; }
        public long createdAt { set; get; }
        public string icon { get; set; }
        public float fee { get; set; }
        public float amounts { get; set; }
        public float avgPrice { get; set; }
        public long lastModifiedAt { set; get; }
        public long version { set; get; }

        public override bool Equals(object obj)
        {
            return obj is Order order &&
                   id == order.id;
        }

        public override int GetHashCode()
        {
            return 1877310944 + id.GetHashCode();
        }
    }
}
