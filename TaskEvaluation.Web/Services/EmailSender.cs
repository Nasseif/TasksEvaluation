using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;
using TaskEvaluation.Core.Consts;

namespace TaskEvaluation.Web.Services
{
    public class EmailSender : IEmailSender
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly MailSettings _smtpSetting;

        public EmailSender(IOptions<MailSettings> smtpSetting, IWebHostEnvironment webHostEnvironment)
        {
            _smtpSetting = smtpSetting.Value;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            if (string.IsNullOrEmpty(email))
            {
                throw new ArgumentNullException(nameof(email), "Email address cannot be null or empty.");
            }

            MailMessage message = new MailMessage
            {
                From = new MailAddress(_smtpSetting.Email!, _smtpSetting.Display),
                Body = htmlMessage,
                Subject = subject,
                IsBodyHtml = true
            };

            message.To.Add(email);

            SmtpClient smtpClient = new(_smtpSetting.Host)
            {
                Port = _smtpSetting.Port,
                Credentials = new NetworkCredential(_smtpSetting.Email, _smtpSetting.Password),
                EnableSsl = true
            };

            await smtpClient.SendMailAsync(message);

            smtpClient.Dispose();
        }

    }
}
