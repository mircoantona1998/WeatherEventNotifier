using System.Runtime.Serialization;

namespace UserdataService.Utils
{
    public enum MessageType
    {
        [EnumMember(Value = "Request")]
        Request,
        [EnumMember(Value = "Response")]
        Response
    }  
}
