

namespace Userdata.ViewModels
{
    #region UserMail
    public class UserMailCreateDTO
    {
        public string Mail { get; set; }
    }
    public class MailCreateRequestKafka : UserMailCreateDTO
    {
        public int IdUser { get; set; }
        public static MailCreateRequestKafka ConvertMailCreateToRequestKafka(UserMailCreateDTO dto, int idUser)
        {
            return new MailCreateRequestKafka
            {
                Mail = dto.Mail,
                IdUser =idUser,
            };
        }
    }
    public class UserMailPatchDTO
    {
        public string Mail { get; set; }
        public bool? IsActive { get; set; }
    }
    public class MailPatchRequestKafka : UserMailPatchDTO
    {
        public int IdUser { get; set; }
        public static MailPatchRequestKafka ConvertMailPatchToRequestKafka(UserMailPatchDTO dto, int idUser)
        {
            return new MailPatchRequestKafka
            {
                Mail = dto.Mail,
                IdUser = idUser,
                IsActive = dto.IsActive
            };
        }
    }
    #endregion
}