using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MimeKit;

namespace ServiceCenterWeb.Utils
{
    public class EmailService
    {
        public static async Task SendEmailAsync(string email, string subject, string message)
        {
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress("Администрация сайта", "dave.mayson@mail.ru"));
            emailMessage.To.Add(new MailboxAddress(email, email));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html) {
                Text = message
            };

            using (var client = new SmtpClient()) {
                await client.ConnectAsync("smtp.mail.ru", 25, false);
                await client.AuthenticateAsync("dave.mayson@mail.ru", "MaY_son___1337");
                await client.SendAsync(emailMessage);
                await client.DisconnectAsync(true);
            } // using
        }
    }
}
