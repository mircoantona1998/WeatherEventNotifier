using AppWeatherEventNotifier.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;


namespace AppWeatherEventNotifier.Helper.Interfaces
{
    public interface IConfigurationInterface
    {
        public void SetShortBaseUrl(string baseurl);
        public string GetShortBaseUrl();

        public int GetNotificationWarning();
        public bool IsChangeCityEnabled();
        public bool IsLocateParkVisible();
        public bool IsAutorizzazioneComunaleEnabled();
        public bool IsOpenHelpPageInApp();
        public string GetDatabasePath();
        public bool IsBleEnabled();
        public bool IsChangeUrlEnabled();
        public bool PushNotificationEnabled();
        public string GetIconPath();
        public List<CertificateModel> ListCertificates { get; }

    }
}
