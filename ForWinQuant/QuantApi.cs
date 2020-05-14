using Newtonsoft.Json;
using Refit;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;

namespace ForWinQuant
{
    public class QuantApi
    {
        const string APPSETINGTOKENKEY = "access_token";
        const string BaseLoginUrl = "http://192.168.1.114:8000";
        readonly IRestClient _client;
        string token = ConfigurationManager.AppSettings.Get(APPSETINGTOKENKEY);

        const string BaseEunexUrl = "https://api6c9jg3nfw1s1js.eunex.co/v2/public";
        const string MarketEunexUrl = "https://api6c9jg3nfw1s1js.eunex.co/v2/market";
        const string MembersEunexUrl = "https://api6c9jg3nfw1s1js.eunex.co/v2/member";
        const string OrdersEunexUrl = "https://api6c9jg3nfw1s1js.eunex.co/v2/orders";
        const string API_KEY = "KqtRQAwWjlqEnS8v";
        const string API_SECRET = "07OojLSkmAGdiPwm";

        private MainForm mainForm;
        public QuantApi(MainForm fm)
        {
            mainForm = fm;
            _client = new RestClient(BaseEunexUrl);
            if (String.IsNullOrEmpty(token)) { 
                LoginForm loginForm = new LoginForm(this.mainForm);
                loginForm.Show();
            }
            else
            {
            }
        }

        public IMembersApi membersService()
        {
            return RestService.For<IMembersApi>(MembersEunexUrl);
        }

        public int login(string name,string psword)
        {
            var client = new RestClient(BaseLoginUrl);
            var request = new RestRequest("userLogin", DataFormat.Json);
            request.AddJsonBody(new { username = name, password = psword });
            var response = client.Post(request);
            var resMeta = response.Content;
            var result = new Dictionary<string, object>();
            int code = -1;
            try
            {
                result = JsonConvert.DeserializeObject<Dictionary<string, object>>(resMeta);
                if ((long)result["code"] == 0)
                {
                    code = 0;
                    string auth = result[APPSETINGTOKENKEY].ToString();
                    Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                    ForWinQuant.Properties.Settings.Default.access_token = auth;
                    config.Save(ConfigurationSaveMode.Modified);
                }
                else {
                    code = (int)result["code"];
                }
            }
            catch (JsonReaderException e)
            {
                code = -1;
            }
            return code;
        }

        public T Execute<T>(RestRequest request) where T : new()
        {
            //request.AddParameter("AccountSid", _accountSid, ParameterType.UrlSegment); // used on every request
            var response = _client.Execute<T>(request);

            if (response.ErrorException != null)
            {
                const string message = "Error retrieving response.  Check inner details for more info.";
                var twilioException = new Exception(message, response.ErrorException);
                throw twilioException;
            }
            return response.Data;
        }
    }
}
