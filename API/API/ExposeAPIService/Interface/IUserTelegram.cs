namespace ExposeAPI.Interface
{
    public interface IUserTelegram
    {
        public int Id { get; set; }
        public int IdUser { get; set; }
        public string ChatId { get; set; }
        public bool? IsActive { get; set; }
    }
}
