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
        public static async Task<string> SendSMS(string API_Key, string To_Phone_Number, string Text_Massage)
        {
            try
            {
                using var client = new HttpClient();

                string API_Url = "https://api.sms.net.bd/sendsms";

                client.BaseAddress = new Uri(API_Url);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client.GetAsync($"?api_key={API_Key}&msg={Text_Massage}&to={To_Phone_Number}");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    dynamic stuff = JsonConvert.DeserializeObject(content);

                    if (stuff.error == "0")
                    {
                        string a = (string)stuff.data;
                        return a;
                    }
                    else
                    {
                        throw new SmsSendingException($"Sms Not Send, {stuff.msg}");
                    }
                }
                else
                {
                    throw new SmsSendingException($"Error: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                throw new SmsSendingException($"An error occurred: {ex.Message}", ex);
            }
        }
    }

    public class SmsSendingException : Exception
    {
        public SmsSendingException(string message) : base(message) { }

        public SmsSendingException(string message, Exception innerException) : base(message, innerException) { }
    }
}
