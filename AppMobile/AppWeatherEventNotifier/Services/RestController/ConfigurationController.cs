using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppWeatherEventNotifier.Services.RestController
{
    internal class ConfigurationController
    {
        public static async Task<bool> add_configuration(string nameConfiguration, float longitude, float latitude, int? idMetric,int? idFrequency,string symbol,float value, DateTime? dateTimeAttivazione)
        {
            string endPoint = "/Configuration/Add";
            var body = new
            {
                NameConfiguration= nameConfiguration,
                Longitude = longitude,
                Latitude = latitude,
                IdMetric = idMetric,
                IdFrequency = idFrequency,
                Value=value,
                Symbol=symbol,
                DateTimeActivation = dateTimeAttivazione
            };
            bool res = await Helper.HttpHelper.HttpPostRequest<bool>(endPoint, body, true);
            if (res == true) { return true; }
            else return false;
        }
        public static async Task<List<Models.Configuration>> get_configurations()
        {
            string endPoint = "";
            endPoint = "/Configuration/Get";
            var res = await Helper.HttpHelper.HttpGetRequest<List<Models.Configuration>>(endPoint, true);
            return res;
        }
        public static async Task<bool> delete_configuration(int? idConfiguration)
        {
            string endPoint = "/Configuration/Delete?IdConfiguration=" + idConfiguration;
            bool res = await Helper.HttpHelper.HttpDeleteRequest<bool>(endPoint, true);
            if (res == true) { return true; }
            else return false;
        }
        public static async Task<bool> patch_configuration(string nameConfiguration,int? idConfiguration, float longitude, float latitude, int? metric, int? frequency,string symbol,float value,DateTime? dateTimeAttivazione,bool? isActive)
        {
            string endPoint = "/Configuration/Patch";
            var body = new
            {
                IdConfiguration = idConfiguration,
                NameConfiguration= nameConfiguration,
                Longitude = longitude,
                Latitude = latitude,
                IdMetric = metric,
                IdFrequency = frequency,
                Symbol=symbol,
                Value=value,
                DateTimeActivation=dateTimeAttivazione,
                IsActive=isActive
            };
            bool res = await Helper.HttpHelper.HttpPatchRequest<bool>(endPoint, body, true);
            if (res == true) { return true; }
            else return false;
        }
    }
}
