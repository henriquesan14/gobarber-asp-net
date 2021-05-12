using ApiGoBarber.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace ApiGoBarber.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;
        private SmtpClient client;
        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void SendMail(string email, string subject, string body)
        {
            client = new SmtpClient(_configuration["EmailSettings:Host"], Int32.Parse(_configuration["EmailSettings:Port"]))
            {
                Credentials = new NetworkCredential(_configuration["EmailSettings:User"], _configuration["EmailSettings:Password"]),
                EnableSsl = true
            };
            MailMessage message = new MailMessage(new MailAddress(_configuration["EmailSettings:From"]), new MailAddress(email));
            message.IsBodyHtml = true;
            message.BodyEncoding = System.Text.Encoding.UTF8;
            message.Subject = subject;
            message.Body = body;
            client.Send(message);
        }
    }
}
