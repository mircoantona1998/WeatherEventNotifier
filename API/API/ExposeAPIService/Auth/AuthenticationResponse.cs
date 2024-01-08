namespace ExposeAPI.Auth
{
    public class AuthenticationResponse
    {
        public int httpcode { get; set; }
        public bool success { get; set; }
        public TokenCustom token { get; set; }
        public string message { get; set; }
        public bool changeMe { get; set; }
        public int ErrorCode { get; set; }
    }

}
