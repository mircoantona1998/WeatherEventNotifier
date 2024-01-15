using System.Runtime.Serialization;

namespace SLAManager.Utils
{
    [DataContract]
    public enum MessageTag
    {
        #region Configuration
        [EnumMember(Value = "AddConfiguration")]
        AddConfiguration,

        [EnumMember(Value = "PatchConfiguration")]
        PatchConfiguration,

        [EnumMember(Value = "GetConfiguration")]
        GetConfiguration,

        [EnumMember(Value = "DeleteConfiguration")]
        DeleteConfiguration,
        #endregion

        #region Metric
        [EnumMember(Value = "AddMetric")]
        AddMetric,

        [EnumMember(Value = "PatchMetric")]
        PatchMetric,

        [EnumMember(Value = "GetMetric")]
        GetMetric,

        [EnumMember(Value = "DeleteMetric")]
        DeleteMetric,
        #endregion

        #region Frequency
        [EnumMember(Value = "AddFrequency")]
        AddFrequency,

        [EnumMember(Value = "PatchFrequency")]
        PatchFrequency,

        [EnumMember(Value = "GetFrequency")]
        GetFrequency,

        [EnumMember(Value = "DeleteFrequency")]
        DeleteFrequency,
        #endregion

        #region Notifier
        [EnumMember(Value = "AddNotify")]
        AddNotify,

        [EnumMember(Value = "PatchNotify")]
        PatchNotify,

        [EnumMember(Value = "GetNotify")]
        GetNotify,

        [EnumMember(Value = "DeleteNotify")]
        DeleteNotify,
        #endregion

        #region Scheduler

        [EnumMember(Value = "GetSchedulation")]
        GetSchedulation,

        #endregion

        #region TelegramSent

        [EnumMember(Value = "GetTelegramSent")]
        GetTelegramSent,

        #endregion

        #region MailSent

        [EnumMember(Value = "GetMailSent")]
        GetMailSent,

        #endregion

        #region UserTelegram
        [EnumMember(Value = "AddUserTelegram")]
        AddUserTelegram,

        [EnumMember(Value = "PatchUserTelegram")]
        PatchUserTelegram,

        [EnumMember(Value = "GetUserTelegram")]
        GetUserTelegram,

        [EnumMember(Value = "DeleteUserTelegram")]
        DeleteUserTelegram,
        #endregion

        #region UserMail
        [EnumMember(Value = "AddUserMail")]
        AddUserMail,

        [EnumMember(Value = "PatchUserMail")]
        PatchUserMail,

        [EnumMember(Value = "GetUserMail")]
        GetUserMail,

        [EnumMember(Value = "DeleteUserMail")]
        DeleteUserMail,
        #endregion
    }

}
