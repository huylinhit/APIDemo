using System;
namespace APIDemo.Services
{
      public class CloudMailService : IMailService
    {
        private string _mailFrom = string.Empty;
        private string _mailTo = string.Empty;

        public CloudMailService(IConfiguration configuration)
        {
            _mailFrom = configuration["MailSetting:MailFrom"];
            _mailTo = configuration["MailSetting:MailTo"];
        }
        public void Send(string subject, string message)
            {
                Console.WriteLine($"The Mail from {_mailFrom} to {_mailTo} with " +
                    $"{nameof(CloudMailService)}");
                Console.WriteLine($"Subject: {subject}");
                Console.WriteLine($"Message: {message}");
            }
        }
    
	
}

