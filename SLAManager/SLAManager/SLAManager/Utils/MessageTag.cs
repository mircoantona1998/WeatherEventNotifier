using System.Runtime.Serialization;

namespace SLAManager.Utils
{
    [DataContract]
    public enum MessageTag
    {
        #region Forecast
        [EnumMember(Value = "GetForecast")]
        GetForecast,
        #endregion
    }

}
