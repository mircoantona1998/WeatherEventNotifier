using System.Runtime.Serialization;

namespace ExposeAPI.Utils
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
        DeleteFrequency
        #endregion
    }

}
