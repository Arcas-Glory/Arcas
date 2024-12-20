namespace Arcas.Server.DTOs
{
    public class AuthRegisterRequest
    {
        public string username { get; set; }  // char(30)
        public string password { get; set; }  // char(30)
        public string nickname { get; set; }  // char(30)
    }
}
