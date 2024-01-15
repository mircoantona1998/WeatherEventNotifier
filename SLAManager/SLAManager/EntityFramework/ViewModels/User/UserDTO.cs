

using Microsoft.AspNetCore.Identity;

namespace Userdata.ViewModels
{
    #region USER
    public class UserCreateDTO
    {
        public string Service { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

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