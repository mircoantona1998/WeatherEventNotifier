using System.Runtime.Serialization;

namespace ExposeAPI.Utils
{
    [DataContract]
    public enum MessageTag
    {
        [EnumMember(Value = "AddConfiguration")]
        AddConfiguration,

        [EnumMember(Value = "PatchConfiguration")]
        PatchConfiguration,

        [EnumMember(Value = "GetConfiguration")]
        GetConfiguration,

        [EnumMember(Value = "DeleteConfiguration")]
        DeleteConfiguration
    }

}
