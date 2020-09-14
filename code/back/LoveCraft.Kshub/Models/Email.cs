using LimFx.Business.Services;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoveCraft.Kshub.Models
{
    public class Email : Entity, IEmail, ISearchAble
    {
        //Sender Name
        public string Sender { get; set; }

        public DateTime ExpectSendTime { get; set; }
        public List<string> Receivers { get; set; }
        public string Subject { get; set; }
        public string RazorTemplate { get; set; }
        public string SearchAbleString { get; set; }
        public string Requester { get; set; }
        public string Url { get; set; }
    }
    public class EmailProperty : IEmailProperty
    {
        public string Url { get; set; }
        public List<string> Receivers { get; set; }
        public string RazorTemplatePath { get; set; } = "Index.cshtml";
        public string Subject { get; set; } = "default test";
    }
    public interface IEmailProperty
    {
        public string Url { get; set; }
        public List<string> Receivers { get; set; }
        public string RazorTemplatePath { get; set; }
        public string Subject { get; set; }
    }
}
