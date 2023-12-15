﻿using Newtonsoft.Json;
using System.Text.Json;

namespace UserdataService.Utils
{
    public static class Json
    {
        public static string ConvertObjectToJson(object obj)
        {
            var jsonOptions = new JsonSerializerOptions
            {
                WriteIndented = true
            };
            string jsonString = System.Text.Json.JsonSerializer.Serialize(obj, jsonOptions);
            return jsonString;
        }
        public static T ConvertJsonToObject<T>(string jsonString)
        {
            return JsonConvert.DeserializeObject<T>(jsonString);
        }
        public static string PackagingMessage(object element, MessageType type, MessageTag tag, int? IdOffsetResponse)
        {
            var data = ConvertObjectToJson(element);
            string json = JsonConvert.SerializeObject(new
            {
                IdOffsetResponse = IdOffsetResponse,
                Type = EnumUtils.EnumToString(type),
                Tag = EnumUtils.EnumToString(tag),
                Data = data
            }, Formatting.Indented);
            return json;
        }

    }
}
