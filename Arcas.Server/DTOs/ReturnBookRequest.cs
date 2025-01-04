namespace Arcas.Server.DTOs
{
    //        API: 使图书继续漂流（归还图书）
    //调用完此API后，后端数据库图书状态改为自由可借阅
    //•        HTTP 请求方法: POST
    //•        请求路径: /api/email/return
    //•        前端输入：
    //o token：(char(30))
    //•        userID：
    //•        bookID: 书籍ID(int)
    //•        commentContent: 评论内容
    //•        后端输出：
    //•        vertify: 验证结果(string)
    public class ReturnBookRequest
    {
        public string token { get; set; }
        public int userID { get; set; }
        public int bookID { get; set; }
        public string commentContent { get; set; }
    }
}
