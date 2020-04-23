using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SendGrid;
using SendGrid.Helpers.Mail;
namespace LoveCraft.Kshub
{
    public class Program
    {
        static async Task Execute()
        {
            var apiKey = "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";
            // Environment.GetEnvironmentVariable("EmailSender");
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("2016231075@qq.com", "Example User");
            var subject = "Sending with SendGrid is Fun";
            var to = new EmailAddress("xieyuschen@gmail.com", "Example User");
            var plainTextContent = "and easy to do anywhere, even with C#";
            var htmlContent = "<strong>and easy to do anywhere, even with C#</strong>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);
        }
        public static void Main(string[] args)
        {
            Execute().Wait();
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
