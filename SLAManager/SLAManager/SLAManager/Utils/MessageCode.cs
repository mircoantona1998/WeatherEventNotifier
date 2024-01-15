using System.Runtime.Serialization;

namespace SLAManager.Utils
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
