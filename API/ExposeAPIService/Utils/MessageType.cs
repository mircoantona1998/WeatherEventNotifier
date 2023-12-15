using System.Runtime.Serialization;

namespace ExposeAPI.Utils
{
    public enum MessageType
    {
        [EnumMember(Value = "Request")]
        Request,
        [EnumMember(Value = "Response")]
        Response
    }  
}
