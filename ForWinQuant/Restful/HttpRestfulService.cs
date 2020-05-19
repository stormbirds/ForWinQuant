// ===============================================================================
// Project Name        :    ForWinQuant
// Project Description :   
// ===============================================================================
// Class Name          :    HttpRestfulService
// Class Version       :    v1.0.0.0
// Class Description   :   
// Author              :    baojun
// Create Time         :    2020/5/14 16:54:26
// Update Time         :    2020/5/14 16:54:26
// ===============================================================================
// Copyright © BAOJUN5040 2020 . All rights reserved.
// ===============================================================================

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Refit;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Policy;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ForWinQuant
{
    public static class HttpRestfulService
    {
        static readonly JsonSerializerSettings jsonSettings = new JsonSerializerSettings()
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver()
        };

        const string BaseEunexUrl = "https://api6c9jg3nfw1s1js.eunex.co/v2/public";
        const string MarketEunexUrl = "https://api6c9jg3nfw1s1js.eunex.co/v2/market";
        const string MembersEunexUrl = "https://api6c9jg3nfw1s1js.eunex.co/v2/members";
        const string OrdersEunexUrl = "https://api6c9jg3nfw1s1js.eunex.co/v2/orders";
        const string TestServerUrl = "http://192.168.1.114:8000";
        public static string API_KEY = "KqtRQAwWjlqEnS8v";
        public static string API_SECRET = "07OojLSkmAGdiPwm";

        public static string Access_Token = "";

        static readonly TimeSpan RequestTimeout = TimeSpan.FromDays(1);

        public static T ForMembersApi<T>()
        {
            //return RestService.For<T>(MembersEunexUrl);
            return RestService.For<T>(
              new HttpClient(new MembersHttpClientHandler())
              {
                  BaseAddress = new Uri(MembersEunexUrl),
                  Timeout = RequestTimeout
              },
              new RefitSettings()
              {
                  ContentSerializer = new NewtonsoftJsonContentSerializer(jsonSettings)
              });
        }

        public static T ForBaseApi<T>()
        {
            string base_url = "";

            switch (typeof(T).Name)
            {
                case "IMembersApi":
                    base_url = MembersEunexUrl;
                    break;
                case "IBaseApi":
                    base_url = BaseEunexUrl;
                    break;
                case "IMarketApi":
                    base_url = MarketEunexUrl;
                    break;
                case "IOrdersApi":
                    base_url = OrdersEunexUrl;
                    break;
                case "IUserApi":
                    base_url = TestServerUrl;
                    return RestService.For<T>(
                      new HttpClient(new UserHttpClientHandler())
                      {
                          BaseAddress = new Uri(base_url),
                          Timeout = RequestTimeout
                      },
                      new RefitSettings()
                      {
                          ContentSerializer = new NewtonsoftJsonContentSerializer(jsonSettings)
                      });

                default:
                    base_url = TestServerUrl;
                    break;
            }

            return RestService.For<T>(
              new HttpClient(new MembersHttpClientHandler())
              {
                  BaseAddress = new Uri(base_url),
                  Timeout = RequestTimeout
              },
              new RefitSettings()
              {
                  ContentSerializer = new NewtonsoftJsonContentSerializer(jsonSettings)
              });
        }


        public static string getSign(string query)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            query = query.Substring(1, query.Length - 1);
            string[] kv = query.Split(new char[] { '&' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string item in kv)
            {
                string[] kv2 = item.Split(new char[] { '=' }, StringSplitOptions.RemoveEmptyEntries);
                dic.Add(kv2[0], kv2[1]);
            }

            IDictionary<string, string> sortedParams = new SortedDictionary<string, string>(dic);
            StringBuilder basestring = new StringBuilder();
            for (int i = 0; i < sortedParams.Count; i++)
            {
                if (i < sortedParams.Count - 1)
                    basestring.Append(sortedParams.ToList()[i].Key).Append("=").Append(sortedParams.ToList()[i].Value).Append("&");
                else
                    basestring.Append(sortedParams.ToList()[i].Key).Append("=").Append(sortedParams.ToList()[i].Value);
            }
            basestring.Append("&secret_key=").Append(API_SECRET);
            string result = basestring.ToString();
            result = Utils.Sha1Sign(result);
            return result;
        }

        public static string getSign(Dictionary<string, string> dic)
        {
            IDictionary<string, string> sortedParams = new SortedDictionary<string, string>(dic);
            StringBuilder basestring = new StringBuilder();
            for (int i = 0; i < sortedParams.Count; i++)
            {
                if (i < sortedParams.Count - 1)
                    basestring.Append(sortedParams.ToList()[i].Key).Append("=").Append(sortedParams.ToList()[i].Value).Append("&");
                else
                    basestring.Append(sortedParams.ToList()[i].Key).Append("=").Append(sortedParams.ToList()[i].Value);
            }
            basestring.Append("&secret_key=").Append(API_SECRET);
            string result = basestring.ToString();
            result = Utils.Sha1Sign(result);
            return result;
        }

        public static async Task<HttpResponseMessage> HandleHttpIO(HttpRequestMessage request, HttpResponseMessage response)
        {
            Console.Write("request url: " + request.RequestUri.ToString());
            var res = "";
            res = await response.Content.ReadAsStringAsync();
            Console.Write("response data: " + res);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                Console.Write("响应错误 {res}");                
            }
            return response;
        }

    }

    class UserHttpClientHandler : HttpClientHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var access_token = ForWinQuant.Properties.Settings.Default.access_token;
            if (access_token != null && !string.IsNullOrWhiteSpace(access_token))
                request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(access_token);
            var response = await base.SendAsync(request, cancellationToken);
            var message = await HttpRestfulService.HandleHttpIO(request, response);
            return message;
        }
    }
    class MembersHttpClientHandler : HttpClientHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                return null;
            }
            var timestamp = Utils.GetTimeStampMilliseconds().ToString();
            var originUri = request.RequestUri.AbsoluteUri;

            originUri = string.IsNullOrWhiteSpace( request.RequestUri.Query) ? (originUri + "?") : (originUri + "&");
            var custemUri = new Uri(string.Format("{0}api_id={1}&timestamp={2}", originUri, HttpRestfulService.API_KEY, timestamp));

            request.RequestUri = new Uri(custemUri.AbsoluteUri + "&sign=" + HttpRestfulService.getSign(custemUri.Query));

            var response = await base.SendAsync(request, cancellationToken);
            var message = await HttpRestfulService.HandleHttpIO(request, response);
            return message;
        }
    }


}
