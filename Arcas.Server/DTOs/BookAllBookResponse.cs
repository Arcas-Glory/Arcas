namespace Arcas.Server.DTOs
{
    public class AllBookItemDto
    {
        public int bookID { get; set; }
        public string title { get; set; }    // 与前端要求保持一致
        public string ISBN { get; set; }
        public int bookState { get; set; }
        public string cover { get; set; }
        public int category { get; set; }
    }

    public class AllBookResponse
    {
        public string vertify { get; set; }   // "success" 或 "fail"
        public List<AllBookItemDto> bookLists { get; set; }
    }
}
