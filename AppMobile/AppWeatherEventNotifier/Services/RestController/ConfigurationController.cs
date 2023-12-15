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
        public static async Task<bool> add_configuration(int idUser, float longitude, float latitude, string metric, string frequency)
        {
            string endPoint = "/Configuration/Add";
            var body = new
            {
                IdUser = idUser,
                Longitude = longitude,
                Latitude = latitude,
                Metric = metric,
                Frequency = frequency
            };
            bool res = await Helper.HttpHelper.HttpPostRequest<bool>(endPoint, body, true);
            if (res == true) { return true; }
            else return false;
        }
        public static async Task<List<Models.Configuration>> get_configurations(int idUser)
        {
            string endPoint = "";
            endPoint = "/Configuration/Get?IdUser=" +idUser;
            var res = await Helper.HttpHelper.HttpGetRequest<List<Models.Configuration>>(endPoint, true);
            return res;
        }
        public static async Task<bool> delete_configuration(int idUser,int idConfiguration)
        {
            string body = "";
            if (idUser != null) body = body + "IdUser=" + idUser;
            if (idConfiguration != null)
            { 
                if (body != "")
                    body = body + "&";
                body = body + "IdConfiguration=" + idConfiguration; 
            }
            string endPoint = "/Configuration/Delete?"+body;
            bool res = await Helper.HttpHelper.HttpDeleteRequest<bool>(endPoint, true);
            if (res == true) { return true; }
            else return false;
        }
        public static async Task<bool> patch_configuration(int idConfiguration,int idUser, float longitude, float latitude, string metric, string frequency)
        {
            string endPoint = "/Configuration/Patch";
            var body = new
            {
                IdConfiguration = idConfiguration,
                IdUser = idUser,
                Longitude = longitude,
                Latitude = latitude,
                Metric = metric,
                Frequency = frequency
            };
            bool res = await Helper.HttpHelper.HttpPatchRequest<bool>(endPoint, body, true);
            if (res == true) { return true; }
            else return false;
        }
    }
}
