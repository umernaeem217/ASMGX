using ASMGX.Utilities.SMTP.Models;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;

namespace ASMGX.Utilities.SMTP
{
    public static class EmailSender
    {
        private static SmtpClient GetClient(SmtpClientOptions options)
        {
            return new SmtpClient()
            {
                Host = options.Host,
                EnableSsl = options.EnableSsl,
                Port = options.Port,
                Timeout = 180000,
                Credentials = new NetworkCredential(
                    userName: options.UserName,
                    password: options.Password)
            };
        }
        
        public static async Task SendEmail(SmtpClientOptions options, SendEmailParams sendEmailParams)
        {
            try
            {
                using var client = GetClient(options);

                using var message = new MailMessage();
                message.From = new MailAddress(
                    sendEmailParams.FromAddress,
                    sendEmailParams.SenderName
                );
                message.To.Add(new MailAddress(
                    sendEmailParams.ToAddresses,
                    sendEmailParams.RecieverName
                ));

                message.Subject = sendEmailParams.Subject;

                if (sendEmailParams.IsView)
                {
                    if (!sendEmailParams.IsHtml)
                    {
                        message.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(
                        sendEmailParams.Message, null, MediaTypeNames.Text.Plain));
                    }

                    if (sendEmailParams.IsHtml)
                    {
                        message.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(
                        sendEmailParams.Message, null, MediaTypeNames.Text.Html));

                    }

                }
                else
                {
                    message.Body = sendEmailParams.Message;
                    message.IsBodyHtml = sendEmailParams.IsHtml;
                }

                await client.SendMailAsync(message);
            }
            catch (Exception)
            {
                throw;
            }

        }
            
    }
}
