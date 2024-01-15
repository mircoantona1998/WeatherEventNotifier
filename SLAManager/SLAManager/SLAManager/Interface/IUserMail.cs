namespace SLAManager.Interface
{
    public interface IUserMail
    {
        public int Id { get; set; }
        public int IdUser { get; set; }
        public string Mail { get; set; }
        public bool? IsActive { get; set; }
    }
}
