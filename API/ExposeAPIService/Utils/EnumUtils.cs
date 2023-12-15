using System.Runtime.Serialization;

namespace ExposeAPI.Utils
{
    public static class EnumUtils
    {
        public static string EnumToString<TEnum>(TEnum value) where TEnum : struct
        {
            Type enumType = typeof(TEnum);
            var enumMemberAttribute = (EnumMemberAttribute[])enumType.GetField(value.ToString())
                .GetCustomAttributes(typeof(EnumMemberAttribute), false);

            return enumMemberAttribute.Length > 0 ? enumMemberAttribute[0].Value : value.ToString();
        }
        public static TEnum StringToEnum<TEnum>(string value) where TEnum : struct
        {
            Type enumType = typeof(TEnum);
            foreach (var field in enumType.GetFields())
            {
                var enumMemberAttribute = (EnumMemberAttribute[])field.GetCustomAttributes(typeof(EnumMemberAttribute), false);
                if (enumMemberAttribute.Length > 0 && enumMemberAttribute[0].Value == value)
                {
                    return (TEnum)field.GetValue(null);
                }
            }

            return (TEnum)Enum.Parse(enumType, value, true);
        }
    }
}
