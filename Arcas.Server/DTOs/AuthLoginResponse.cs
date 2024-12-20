namespace Arcas.Server.DTOs
{
    public class AuthLoginResponse
    {
        public string vertify { get; set; }    // 验证结果
        public string token { get; set; }      // char(30)
        public string nickname { get; set; }   // 用户昵称
        public int userID { get; set; }        // 用户ID
    }
}
