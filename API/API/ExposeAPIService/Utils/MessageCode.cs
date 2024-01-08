using System.Runtime.Serialization;

namespace ExposeAPI.Utils
{
    [DataContract]
    public enum MessageCode
    {
        [EnumMember(Value = "Ok")]
        Ok,

        [EnumMember(Value = "Error")]
        Error,
    }

}
