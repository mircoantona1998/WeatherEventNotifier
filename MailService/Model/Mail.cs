namespace EmailService.Model
{
    public class Mail
    {
        public int Id { get; set; }
        public string? Destinatario { get; set; }
        public string? Oggetto { get; set; }
        public string? Testo { get; set; }
        public string? Result { get; set; }
        public bool? WasSent { get; set; }
        public DateTime? dateSent { get; set; }
    }
    public partial class MailConfiguration
    {
        public int id { get; set; } 
        public string? mail { get; set; }
        public string? password { get; set; }
        public string? name { get; set; }
    }
    public class MailComplete : Mail
    {
        public string? mail { get; set; }
        public string? password { get; set; }
        public string? name { get; set; }
    }
}
