

namespace Userdata.ViewModels
{
    #region UserTelegram
    public class UserTelegramCreateDTO
    {
        public string ChatId { get; set; }
    }
    public class TelegramCreateRequestKafka : UserTelegramCreateDTO
    {
        public int IdUser { get; set; }
        public static TelegramCreateRequestKafka ConvertTelegramCreateToRequestKafka(UserTelegramCreateDTO dto, int idUser)
        {
            return new TelegramCreateRequestKafka
            {
                ChatId = dto.ChatId,
                IdUser = idUser,
            };
        }
    }
    public class UserTelegramPatchDTO
    {
        public string ChatId { get; set; }
        public bool? IsActive { get; set; }
    }
    public class TelegramPatchRequestKafka : UserTelegramPatchDTO
    {
        public int IdUser { get; set; }
        public static TelegramPatchRequestKafka ConvertTelegramPatchToRequestKafka(UserTelegramPatchDTO dto, int idUser)
        {
            return new TelegramPatchRequestKafka
            {
                ChatId = dto.ChatId,
                IdUser = idUser,
                IsActive = dto.IsActive
            };
        }
    }
    #endregion
}