using HadisIelts.Server.Models.Entities;
using MimeKit;

namespace HadisIelts.Server.Services.Email
{
    public class EmailMessage
    {
        public MailboxAddress Reciever { get; set; }
        public string? Subject { get; set; }
        public string? Content { get; set; }
        public List<CorrectedWritingFile>? AttachmentFiles { get; set; }
        public EmailMessage(string reciever)
        {
            Reciever = new MailboxAddress("userMailBox", reciever);
        }
    }
}
