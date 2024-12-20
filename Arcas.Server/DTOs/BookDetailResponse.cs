namespace Arcas.Server.DTOs
{
    public class BookDetailCommentDto
    {
        public int commentID { get; set; }
        public int likeNum { get; set; }
        public string nickname { get; set; }
        public string commentContent { get; set; }
    }
    public class BookDetailResponse
    {
        public string vertify { get; set; }
        public string title { get; set; }  // 按照要求保留原字段名“tittle”
        public string ISBN { get; set; }
        public string nickname { get; set; }
        public int bookState { get; set; }
        public string cover { get; set; }
        public int category { get; set; }
        public List<BookDetailCommentDto> comments { get; set; }
    }
}
