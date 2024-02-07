using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace SMS_Net_BD
{
    internal class SmsService
    {
        private string _apiKey = string.Empty;

        HttpClient client = new HttpClient();

        public SmsService(string apiKey)
        {
            _apiKey = apiKey;
        }

        internal async Task<HttpResponseMessage> PostRequest(string to_Phone_Number, string text_Massage, string sender_Id = "", string scheduleTime = "")
        {
            string API_Url = "https://api.sms.net.bd/sendsms";

            client.BaseAddress = new Uri(API_Url);

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var requestData = new
            {
                api_key = _apiKey,
                msg = text_Massage,
                to = to_Phone_Number,
                schedule = scheduleTime,
                sender = sender_Id
            };

            var content = new StringContent(JsonConvert.SerializeObject(requestData), Encoding.UTF8, "application/json");

            var response = await client.PostAsync(API_Url, content);

            return response;
        }

        internal async Task<HttpResponseMessage> GetRequest(int Id = 0)
        {

            string API_Url = Id > 0 ? $"https://api.sms.net.bd/report/request/{Id}/" : "https://api.sms.net.bd/user/balance/";

            client.BaseAddress = new Uri(API_Url);

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await client.GetAsync($"?api_key={_apiKey}");

            return response;
        }

        internal async Task<string> Response(HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();

                dynamic result = JsonConvert.DeserializeObject(responseContent);

                if (result.error == "0")
                    return responseContent;
                else
                    throw new SmsException($"Error: {result.error}, {result.msg}");
            }
            else
                throw new SmsException($"Error: {response.StatusCode}");
        }
    }

    /// <summary>
    /// SMS Exception
    /// </summary>
    public class SmsException : Exception
    {
        public SmsException(string message) : base(message) { }

        public SmsException(string message, Exception innerException) : base(message, innerException) { }
    }

}
