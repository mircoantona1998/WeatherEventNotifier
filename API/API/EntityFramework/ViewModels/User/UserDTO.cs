

using Microsoft.AspNetCore.Identity;

namespace Userdata.ViewModels
{
    #region USER
    public class UserCreateDTO
    {
   
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string? Address { get; set; }
        public string? Cap { get; set; }
        public string? City { get; set; }
        public string? Cognome { get; set; }
        public string? Nome { get; set; }

    }
    public class UserInsertDTO:UserCreateDTO
    {
        public int Partition { get; set; }
    }
    public class LoginDTO
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
    #endregion
}