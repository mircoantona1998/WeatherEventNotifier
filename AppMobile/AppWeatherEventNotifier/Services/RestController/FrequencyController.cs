using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppWeatherEventNotifier.Services.RestController
{
    internal class FrequencyController
    {      
        public static async Task<List<Models.Frequency>> get_frequencys()
        {
            string endPoint = "/Frequency/Get";
            var res = await Helper.HttpHelper.HttpGetRequest<List<Models.Frequency>>(endPoint, true);
            return res;
        }
    
    }
}
