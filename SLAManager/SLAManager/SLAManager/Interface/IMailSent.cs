namespace SLAManager.Interface
{
    public interface IMailSent
    {
        public int Id { get; set; }

        public string Mittente { get; set; }

        public string Destinatario { get; set; }

        public string Oggetto { get; set; }

        public string Testo { get; set; }

        public bool? Allegati { get; set; }

        public DateTime? DateCreate { get; set; }

        public bool? WasSent { get; set; }

        public string Result { get; set; }

        public int IdUser { get; set; }
    }
}
