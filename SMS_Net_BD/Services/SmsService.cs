using Newtonsoft.Json;
using System.Text;
using System.Net.Http.Headers;
using SMS_Net_BD.Enum;
using SMS_Net_BD.Exceptions;

namespace SMS_Net_BD.Services
{
    internal class SmsService
    {
        private string _apiKey = string.Empty;

        public SmsService(string apiKey)
        {
            _apiKey = apiKey;
        }

        internal async Task<HttpResponseMessage> PostRequest(string to_Phone_Number, string text_Massage, string sender_Id = "", string scheduleTime = "", string content_Id = "")
        {
            HttpClient client = new HttpClient();
            string API_Url = "https://api.sms.net.bd/sendsms";

            client.BaseAddress = new Uri(API_Url);

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var requestData = new
            {
                api_key = _apiKey,
                msg = text_Massage,
                to = to_Phone_Number,
                schedule = scheduleTime,
                sender_id = sender_Id,
                content_id = content_Id
            };

            var content = new StringContent(JsonConvert.SerializeObject(requestData), Encoding.UTF8, "application/json");

            var response = await client.PostAsync(API_Url, content);

            return response;
        }

        internal async Task<HttpResponseMessage> GetRequest(RequestTypeEnum type, int Id = 0)
        {
            HttpClient client = new HttpClient();
            string API_Url = string.Empty;

            switch (type)
            {
                case RequestTypeEnum.Balance:
                    API_Url = "https://api.sms.net.bd/user/balance/";
                    break;
                case RequestTypeEnum.Report:
                    API_Url = $"https://api.sms.net.bd/report/request/{Id}/";
                    break;
                case RequestTypeEnum.Campaign:
                    API_Url = "https://api.sms.net.bd/config/Campaigncontent";
                    break;
            }

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
}
