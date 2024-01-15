using System.Runtime.Serialization;

namespace SLAManager.Utils
{
    public enum MessageType
    {
        [EnumMember(Value = "Request")]
        Request,
        [EnumMember(Value = "Response")]
        Response
    }  
}
