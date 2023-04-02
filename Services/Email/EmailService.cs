using System.Net;
using System.Net.Mail;

namespace ConstradeApi.Services.Email
{
    public class EmailService
    {
        private readonly IConfiguration configuration;
        private string EmailPassword { get; set; }
        public EmailService()
        {
            this.configuration = new ConfigurationBuilder().AddUserSecrets<EmailService>().Build();

            EmailPassword = configuration["EmailPassword"]!;
        }

        public static async Task SendOtpEmail(string recipient, string code)
        {
            EmailService service = new EmailService();

            var message = new MailMessage();
            message.To.Add(recipient);
            message.Subject = "OTP CODE";
            message.From = new MailAddress("constradeapplication22@gmail.com");
            message.Body = $"Your OTP Code is {code}. \n\nThis is a automatic mail. \n\nDont Reply!";

            using (var client = new SmtpClient("smtp.gmail.com", 587))
            {
                client.EnableSsl = true;
                client.Credentials = new NetworkCredential("constradeapplication22@gmail.com", service.EmailPassword);
                
                await client.SendMailAsync(message);
            }

        }
    }
}
