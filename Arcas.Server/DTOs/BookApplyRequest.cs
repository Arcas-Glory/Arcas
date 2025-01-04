using Arcas.Server.Models;

namespace Arcas.Server.DTOs
{
//    HTTP 请求方法: POST
//•        请求路径: /api/email/apply
//•        前端输入：
//o token：(char(30))
//•        userID：
//•        bookID: 书籍ID(int)
//•        content: 收货地址
//•        detailsA: 申请内容
//•        后端输出：
//•        vertify: 验证结果，表示是否发送申请(string)
    public class BookApplyRequest
    {
        public string token { get; set; }
        public int userID { get; set; }
        public int bookID { get; set; }
        public string content { get; set; }
        public string detailsA { get; set; }

       // public string detailsB { get; set; }

    }
}
