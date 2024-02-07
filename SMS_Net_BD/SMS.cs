using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace SMS_Net_BD
{
    public class SMS
    {
        //Private string variable
        private SmsService _smsService;

        /// <summary>
        /// Default Constructor with a Param
        /// </summary>
        /// <param name="apiKey"></param>
        public SMS(string apiKey)
        {
            _smsService = new SmsService(apiKey);
        }

        //HttpClient client = new HttpClient();

        //private SmsService smsService = new SmsService(apiKey);


        /// <summary>
        /// Send SMS with Sender Id using sms.net.bd API
        /// </summary>
        /// <param name="to_Phone_Number"></param>
        /// <param name="text_Massage"></param>
        /// <param name="sender_id"></param>
        /// <returns></returns>
        /// <exception cref="SmsException"></exception>
        public async Task<string> SendSMS(string to_Phone_Number, string text_Massage, string sender_Id = "")
        {
            var response = await _smsService.PostRequest(to_Phone_Number, text_Massage, sender_Id);

            return await _smsService.Response(response);
        }

        /// <summary>
        /// Send Schedule SMS using sms.net.bd API
        /// </summary>
        /// <param name="to_Phone_Number"></param>
        /// <param name="text_Massage"></param>
        /// <param name="scheduleTime"></param>
        /// <returns></returns>
        /// <exception cref="SmsException"></exception>
        public async Task<string> ScheduleSMS(string to_Phone_Number, string text_Massage, string scheduleTime)
        {
            var response = await _smsService.PostRequest(to_Phone_Number, text_Massage, "", scheduleTime);

            return await _smsService.Response(response);
        }


        /// <summary>
        /// Get SMS Report by Id using sms.net.bd API
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="SmsException"></exception>
        public async Task<string> GetReport(int id)
        {
            var response = await _smsService.GetRequest(id);

            return await _smsService.Response(response);
        }

        /// <summary>
        /// Check your Current Balance using sms.net.bd API
        /// </summary>
        /// <returns></returns>
        /// <exception cref="SmsException"></exception>
        public async Task<string> GetBalance()
        {
            var response = await _smsService.GetRequest();

            return await _smsService.Response(response);
        }

    }
}