namespace ASMGX.Utilities.SMTP.Models
{
    public class SendEmailParams
    {
        public string Subject { get; set; }
        public string Message { get; set; }
        public string FromAddress { get; set; }
        public string? SenderName { get; set; }
        public string ToAddresses { get; set; }
        public string? RecieverName { get; set; }
        public bool IsHtml { get; set; }
        public bool IsView { get; set; }
    }
}
