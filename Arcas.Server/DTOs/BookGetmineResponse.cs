using System.Collections.Generic;

namespace YourNamespace.DTOs
{
    public class BookGetmineResponseDTO
    {
        public int bookID { get; set; }
        public string title { get; set; }
        public string ISBN { get; set; }
        public int bookState { get; set; }
        public string cover { get; set; }
        public int category { get; set; }
    }

    public class BookGetmineResponse
    {
        public string vertify { get; set; }   // "success" 或 "fail"
        public List<BookGetmineResponseDTO> bookLists { get; set; }
    }
}
