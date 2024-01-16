

using Microsoft.AspNetCore.Identity;

namespace SLAManagerdata.ViewModels
{
    #region USER
    public class ServiceCreateDTO
    {
        public string Service { get; set; } = string.Empty;
        public string Servicename { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

    }
    public class ServiceInsertDTO : ServiceCreateDTO
    {
        public int Partition { get; set; }
    }
    public class LoginDTO
    {
        public string Servicename { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
    #endregion
}