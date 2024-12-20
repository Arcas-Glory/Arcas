namespace Arcas.Server.DTOs
{
    public class AuthLoginRequest
    {
        public string username { get; set; }  // char(30)
        public string password { get; set; }  // char(30)
    }
}
