namespace Arcas.Server.DTOs
{
    public class BookUploadRequest
    {
        public string token { get; set; }       // char(30)
        public int userID { get; set; }         // 用户ID
        public string title { get; set; }
        public string ISBN { get; set; }        // char(13)
        public string author { get; set; }
        public string publishHouse { get; set; }
        public string cover { get; set; }       // 可为string类型，存储封面URL
        public int category { get; set; }       // 图书类别，int类型(1-现实，2-浪漫，3-历史，4-奇幻，5-悬疑)
    }
}
