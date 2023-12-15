using System.Runtime.Serialization;

namespace UserdataService.Utils
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
