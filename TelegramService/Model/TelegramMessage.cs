
public class TelegramMessage
{
    public int Id { get; set; }
    public string? IdChat { get; set; }
    public string? Testo { get; set; }
    public string? Result { get; set; }
    public bool? WasSent { get; set; }
    public DateTime? dateSent { get; set; }
}
public partial class TelegramConfiguration
{
    public int id { get; set; } 
    public string? bot{ get; set; }
    public string? token { get; set; }
}

