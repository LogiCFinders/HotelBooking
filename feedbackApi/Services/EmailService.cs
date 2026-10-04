using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Extensions.Configuration;
namespace feedbackApi.Services
{

    public class EmailService
    {
        private readonly IConfiguration _config;
        

             
        public EmailService(IConfiguration config) => _config = config;

        /// <summary>Sends an email using the Smtp section of appsettings.json.</summary>
        public async Task SendAsync(string toEmail, string toName, string subject, string body)
        {
            var smtp = _config.GetSection("Smtp");

            var host = smtp["Host"] ?? throw new InvalidOperationException("Smtp:Host is not configured.");
            var port = int.Parse(smtp["Port"] ?? "587");
            var enableSsl = bool.Parse(smtp["EnableSsl"] ?? "true");

            var username = smtp["Username"];
            var password = smtp["Password"];
            var fromEmail = smtp["FromEmail"] ?? username
                ?? throw new InvalidOperationException("Smtp:FromEmail is not configured.");
            var fromName = smtp["FromName"] ?? "ReviewNation";

            var client = new SmtpClient(host, port)
            {
                EnableSsl = enableSsl,
                Timeout = 15000,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(username, password)
            };

            var message = new MailMessage
            {
                From = new MailAddress(fromEmail, fromName),
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            };
            message.To.Add(new MailAddress(toEmail, toName));

            await client.SendMailAsync(message);
        }
        public static async Task SendEmailAsync(string toEmail, string subject, string body)
        {
            using (var client = new SmtpClient("smtp.yourserver.com", 587))
            {
                client.Credentials = new NetworkCredential("yourEmail@domain.com", "password");
                client.EnableSsl = true;

                var mailMessage = new MailMessage("yourEmail@domain.com", toEmail, subject, body);
                await client.SendMailAsync(mailMessage);
            }
        }
        public static async Task SendVerificationEmail(string email, string verifyUrl)
        {
            using (var client = new System.Net.Mail.SmtpClient())
            {
                var mail = new System.Net.Mail.MailMessage("no-reply@yourapp.com", email);
                mail.Subject = "Verify your email";
                mail.Body = $"Welcome! Please verify your email by clicking this link: {verifyUrl}";
                await client.SendMailAsync(mail);
            }
        }
    }
}