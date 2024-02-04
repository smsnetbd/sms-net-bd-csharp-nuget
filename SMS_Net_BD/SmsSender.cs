using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace SMS_Net_BD
{
    public class SmsSender
    {
        //Private string variable
        private string _apiKey = string.Empty;

        /// <summary>
        /// Default Constructor with a Param
        /// </summary>
        /// <param name="apiKey"></param>
        public SmsSender(string apiKey)
        {
            _apiKey = apiKey;
        }

        HttpClient client = new HttpClient();

        /// <summary>
        /// Send SMS using sms.net.bd API
        /// </summary>
        /// <param name="To_Phone_Number"></param>
        /// <param name="Text_Massage"></param>
        /// <returns></returns>
        /// <exception cref="SmsException"></exception>
        public async Task<string> SendSMS(string To_Phone_Number, string Text_Massage)
        {
            try
            {
                //using var client = new HttpClient();

                string API_Url = "https://api.sms.net.bd/sendsms";

                client.BaseAddress = new Uri(API_Url);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var requestData = new
                {
                    api_key = _apiKey,
                    msg = Text_Massage,
                    to = To_Phone_Number
                };

                var content = new StringContent(JsonConvert.SerializeObject(requestData), Encoding.UTF8, "application/json");

                var response = await client.PostAsync(API_Url, content);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    dynamic result = JsonConvert.DeserializeObject(responseContent);

                    if (result.error == "0")
                    {
                        return responseContent;
                    }
                    else
                    {
                        return $"Error: {result.error}, {result.msg}";
                    }
                }
                else
                {
                    throw new SmsException($"Error: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                throw new SmsException($"An error occurred: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Get SMS Report by Id using sms.net.bd API
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        /// <exception cref="SmsException"></exception>
        public async Task<string> ReportSMS(int Id)
        {
            try
            {
                using var client = new HttpClient();

                string API_Url = $"https://api.sms.net.bd/report/request/{Id}/";

                client.BaseAddress = new Uri(API_Url);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client.GetAsync($"?api_key={_apiKey}");

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    dynamic result = JsonConvert.DeserializeObject(responseContent);

                    if (result.error == "0")
                    {
                        return responseContent;
                    }
                    else
                    {
                        throw new SmsException($"Error: {result.error}, {result.msg}");
                    }
                }
                else
                {
                    throw new SmsException($"Error: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                throw new SmsException($"An error occurred: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Check your Current Balance using sms.net.bd API
        /// </summary>
        /// <returns></returns>
        /// <exception cref="SmsException"></exception>
        public async Task<string> Balance()
        {
            try
            {
                using var client = new HttpClient();

                string API_Url = "https://api.sms.net.bd/user/balance/";

                client.BaseAddress = new Uri(API_Url);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client.GetAsync($"?api_key={_apiKey}");

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    dynamic result = JsonConvert.DeserializeObject(responseContent);

                    if (result.error == "0")
                    {
                        return responseContent;
                    }
                    else
                    {
                        throw new SmsException($"Error: {result.error}, {result.msg}");
                    }
                }
                else
                {
                    throw new SmsException($"Error: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                throw new SmsException($"An error occurred: {ex.Message}", ex);
            }
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
