using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace AppWeatherEventNotifier.Helper.Interfaces
{
    public interface ISecurityInterface
    {
        public DelegatingHandler GetHandler();
    }
}
