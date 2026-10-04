using HotelBooking.Model;
using HotelBooking.Model.Messaging;
using HotelBooking.Model.Report;
using HotelBooking.Repository.Implementation;
using HotelBooking.Repository.Interface;
using iTextSharp.text.pdf.codec.wmf;
using Newtonsoft.Json;
using Org.BouncyCastle.Asn1.Crmf;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Mvc;

namespace HotelBooking.Controllers.MessageApi
{
    public class EzyERPMessagingController : ApiController
    {

        private readonly IMessaging _msg;

        public EzyERPMessagingController()
        {
            _msg = new MessageRepository();
        }
        [System.Web.Http.Route("api/Messaging/SendMessage")]
        [System.Web.Http.HttpPost]
        public bool SendMessage(MessageRequest MR)
        {
            bool rtnVal = false;
            MessageAPIConfig _msgConfig = _msg.GetMessageConfiguration(MR.HotelId, MR.MessageProduct, MR.MessageType);

            if (MR.MessageProduct == "W")
            {
                MessageTemplate msgTemp = _msg.GetMessageTemplate(MR.HotelId, MR.MessageProduct, MR.MessageType);
                Task.Run(async () =>
                {
                    string result = await SendWAMessageAsync(_msgConfig, MR, msgTemp);
                    Console.WriteLine($"Result from async method (Task.Run): {result}");
                });

            }
            if (MR.MessageProduct == "S")
            {
                MessageTemplate msgTemp = _msg.GetMessageTemplate(MR.HotelId, MR.MessageProduct, MR.MessageType);
                Task.Run(async () =>
                {
                    string result = await SendSMSAsync(_msgConfig, MR, msgTemp);
                    Console.WriteLine($"Result from async method (Task.Run): {result}");
                });

            }
            if (MR.MessageProduct == "E")
            {
                MessageTemplate msgTemp = _msg.GetMessageTemplate(MR.HotelId, MR.MessageProduct, MR.MessageType);
                Task.Run(async () =>
                {
                    string result = await SendEmailAsync(_msgConfig, MR, msgTemp);
                    Console.WriteLine($"Result from async method (Task.Run): {result}");
                });

            }

            //Log Message History
            LoggMessageHistory(
                            new Messages()
                            {
                                HotelID = MR.HotelId,
                                MsgDate = DateTime.Today,
                                Bookingref=MR.BookingREF,
                                MsgProduct=MR.MessageProduct,
                                SentFrom=_msgConfig.MSGSender,
                                SentTo=MR.PhoneNumber
               

                            }
            );
            
            return rtnVal;
        }



        private async Task<string> SendWAMessageAsync(MessageAPIConfig cfg, MessageRequest req, MessageTemplate msgTemp)
        {
            using (var httpClient = new HttpClient())
            {
                string strMessage = msgTemp.TempMSG.Replace("@@Name", req.LeadPaxName).Replace("@RoomName", req.RoomTypeName).Replace("@CheckourDate", req.CheckinDate).Replace("@BookingRef", req.BookingREF);
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", cfg.APIKey);
                var requestBody = new
                {
                    messaging_product = "whatsapp",
                    to = req.PhoneNumber,
                    type = "text",
                    text = new { body = strMessage }
                };

                string jsonBody = Newtonsoft.Json.JsonConvert.SerializeObject(requestBody);
                var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

                try
                {
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11;
                    HttpResponseMessage response = await httpClient.PostAsync(cfg.APiUrl, content);
                    response.EnsureSuccessStatusCode(); // Throw exception for bad status codes.
                    return await response.Content.ReadAsStringAsync();
                }
                catch (HttpRequestException e)
                {
                    return $"Error sending message: {e.Message}";
                }
            }
        }

        private async Task<string> SendSMSAsync(MessageAPIConfig cfg, MessageRequest req, MessageTemplate msgTemp)
        {
            using (var httpClient = new HttpClient())
            {
                string strMessage = msgTemp.TempMSG.Replace("@@Name", req.LeadPaxName).Replace("@RoomName", req.RoomTypeName).Replace("@CheckourDate", req.CheckinDate).Replace("@BookingRef", req.BookingREF);
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", cfg.APIKey);
                var requestBody = new
                {
                    messaging_product = "whatsapp",
                    to = req.PhoneNumber,
                    type = "text",
                    text = new { body = strMessage }
                };

                string jsonBody = Newtonsoft.Json.JsonConvert.SerializeObject(requestBody);
                var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

                try
                {
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11;
                    HttpResponseMessage response = await httpClient.PostAsync(cfg.APiUrl, content);
                    response.EnsureSuccessStatusCode(); // Throw exception for bad status codes.
                    return await response.Content.ReadAsStringAsync();
                }
                catch (HttpRequestException e)
                {
                    return $"Error sending message: {e.Message}";
                }
            }
        }
        private async Task<string> SendEmailAsync(MessageAPIConfig cfg, MessageRequest req, MessageTemplate msgTemp)
        {
            using (var httpClient = new HttpClient())
            {
                string strMessage = msgTemp.TempMSG.Replace("@@Name", req.LeadPaxName).Replace("@RoomName", req.RoomTypeName).Replace("@CheckourDate", req.CheckinDate).Replace("@BookingRef", req.BookingREF);
                try
                {
                    var smtpClient = new SmtpClient("smtpout.secureserver.net")
                    {
                        Port = 465,
                        Credentials = new NetworkCredential("info@logicfinders.com", "yourPassword"),
                        EnableSsl = true,
                    };

                    var mailMessage = new MailMessage
                    {
                        From = new MailAddress("info@logicfinders.com"),
                        Subject = "Test Email from WebAPI",
                        Body = "This is a test email sent using SMTP from Web API.",
                        IsBodyHtml = false,
                    };
                    mailMessage.To.Add("sanjay.sanyash@gmail.com");

                    var userToken = "EmailJob_001";

                    // Attach event handler to track completion
                    smtpClient.SendCompleted += (s, e) =>
                    {
                        if (e.Cancelled)
                            Console.WriteLine($"Send canceled. User token: {e.UserState}");
                        else if (e.Error != null)
                            Console.WriteLine($"Send failed. Error: {e.Error.Message}. Token: {e.UserState}");
                        else
                            Console.WriteLine($"Email sent successfully. Token: {e.UserState}");
                    };

                    smtpClient.SendAsync(mailMessage, userToken);
                    await Task.Delay(10);
                    return  "Email is being sent asynchronously.";

                }
                catch (HttpRequestException e)
                {
                    return $"Error sending message: {e.Message}";
                }
            }



        }
        private bool LoggMessageHistory(Messages msgLog)
        {
            var rtnVal = false;
            try
            {
                _msg.LogMessageHistory(msgLog);
                rtnVal = true;
            }
            catch (Exception e)
            {

                rtnVal = false;
            }
            
            return rtnVal;
        }
    }
}

