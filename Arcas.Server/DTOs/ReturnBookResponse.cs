namespace Arcas.Server.DTOs
{
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

    public class ReturnBookResponse
    {
        public string vertify { get; set; }
    }
}
