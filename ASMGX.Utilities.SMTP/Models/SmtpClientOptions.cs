namespace ASMGX.Utilities.SMTP.Models
{
    public class SmtpClientOptions
    {
        public string Host { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public bool EnableSsl { get; set; }
        public int Port { get; set; }
    }
}
