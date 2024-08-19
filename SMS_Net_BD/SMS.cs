using Newtonsoft.Json;
using SMS_Net_BD.DTOJson;
using SMS_Net_BD.Exceptions;
using SMS_Net_BD.Services;

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

        /// <summary>
        /// Check your Current Balance using sms.net.bd API
        /// </summary>
        /// <returns></returns>
        /// <exception cref="SmsException"></exception>
        public async Task<string> GetBalance()
        {
            var response = await _smsService.GetRequest(Enum.RequestTypeEnum.Balance);

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
            var response = await _smsService.GetRequest(Enum.RequestTypeEnum.Report);

            return await _smsService.Response(response);
        }

        /// <summary>
        /// Get All Approved Campaign Content List
        /// </summary>
        /// <returns></returns>
        public async Task<string> GetCampaigns()
        {
            var response = await _smsService.GetRequest(Enum.RequestTypeEnum.Campaign);

            return await _smsService.Response(response);
        }

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
            var response = await _smsService.PostRequest(to_Phone_Number, text_Massage, string.Empty, scheduleTime);

            return await _smsService.Response(response);
        }

        /// <summary>
        /// Send Campaign SMS using sms.net.bd API
        /// </summary>
        /// <param name="to_Phone_Number"></param>
        /// <param name="content_id"></param>
        /// <param name="scheduleTime"></param>
        /// <returns></returns>
        public async Task<string> CampaignSMS(string to_Phone_Number, string content_id)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            var getCampaign = await _smsService.GetRequest(Enum.RequestTypeEnum.Campaign);
            var containString = await getCampaign.Content.ReadAsStringAsync();

            JsonRoot resultList = JsonConvert.DeserializeObject<JsonRoot>(containString);
            var matchedItem = resultList.Data.Items.FirstOrDefault(x => x.Id == int.Parse(content_id));
            if (matchedItem != null)
                response = await _smsService.PostRequest(to_Phone_Number, matchedItem.Text, string.Empty, string.Empty, content_id);

            return await _smsService.Response(response);
        }
    }
}