using Microsoft.AspNetCore.Mvc;
using Arcas.Server.Data;
using Arcas.Server.Models;
using Microsoft.EntityFrameworkCore;
using Arcas.Server.DTOs;
using System.Net;
using static System.Net.WebRequestMethods;

namespace Arcas.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmailController : Controller
    {
        private readonly ArcasDbContext _context;

        public EmailController(ArcasDbContext context)
        {
            _context = context;
        }

        

        [HttpGet("get")]
        public async Task<IActionResult> GetEmails([FromQuery] string username, [FromQuery] string token, [FromQuery] string userID)
        {
            // 验证用户名和Token是否有效
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(token))
            {
                return BadRequest(new { message = "用户名和Token不能为空" });
            }


            var emails = await GetExchangeDetailFromDatabase(username);

       
            // 返回结果
            var response = new EmailGetResponse
            {
                vertify = "Verification successful",
                emails = emails
            };

            return Ok(response);
        }
        private async Task<List<ExchangeDetail>> GetExchangeDetailFromDatabase(string username)
        {
            
            var user = await _context.User.FirstOrDefaultAsync(u => u.username == username);
            if (user == null)
            {
                return new List<ExchangeDetail>();
            }
            var emails = await _context.ExchangeDetail
                .Where(e => e.ida == user.userID || e.idb == user.userID)
                .ToListAsync();
            return emails;

          
           
        }


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
        //                 
    


        [HttpPost("apply")]
        
        public async Task<IActionResult> Apply([FromBody] BookApplyRequest request)
        {
            if (request == null)
            {
                return BadRequest(new { message = "请求数据不能为空" });
            }

            // 验证token和userID是否有效
            var isValid = ValidateUser(request.token, request.userID);
            if (!isValid)
            {
                return Unauthorized(new BookApplyResponse { vertify = "无效的Token或UserID" });
            }
            var acptID = await _context.ConcreteBook
                      .Where(a => a.bookID == request.bookID)
                      .Select(a => a.userID)
                      .FirstOrDefaultAsync();
            

            // 模拟保存申请到数据库或处理申请的逻辑
            var application = new ExchangeDetail
            {
                ida = request.userID,
                idb = acptID ,//通过bookID在数据库中寻找对应的用户ID
                bookID = request.bookID,
                content = request.content,
                detailsA = request.detailsA,
                isApproved = false,
               //detailsB可以为空

            };

           
            _context.ExchangeDetail.Add(application);
            await _context.SaveChangesAsync();

            // 返回验证结果
            return Ok(new BookApplyResponse { vertify = "申请成功，已提交" });
        }

        // 检查token和userID
        private bool ValidateUser(string token, int userID)
        {
            var user = _context.User.FirstOrDefault(u => u.userID == userID && u.token == token);
            return user != null;
        }



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


        [HttpPost("return")]
        public async Task<IActionResult> ReturnBook([FromBody] ReturnBookRequest request)
        {
            //  验证输入
            if (request == null || string.IsNullOrEmpty(request.token) || request.userID<=0|| request.bookID <= 0)
            {
                return BadRequest(new ReturnBookResponse { vertify = "输入参数无效" });
            }

            //  验证 token 和 userID
            var isValidUser = ValidateUser(request.token, request.userID);
            if (!isValidUser)
            {
                return Unauthorized(new ReturnBookResponse { vertify = "无效的 token 或 userID" });
            }

            // 检查图书是否存在
            var book = await _context.ConcreteBook.FirstOrDefaultAsync(b => b.bookID == request.bookID);
            if (book == null)
            {
                return NotFound(new ReturnBookResponse { vertify = "图书不存在" });
            }

            
            book.bookState =1; //1代表自由可借阅
            _context.ConcreteBook.Update(book);


            var ISBNcode = await _context.ConcreteBook
                .Where(a => a.bookID == request.bookID)
                .Select(a => a.ISBN)
                .FirstOrDefaultAsync();

            
            var comment = new Comment
            {
                commentatorID = request.userID,
                bookID = request.bookID,
                commentContent = request.commentContent,
                ISBN=ISBNcode
               
            };
            _context.Comment.Add(comment);

            
            await _context.SaveChangesAsync();

            return Ok(new ReturnBookResponse { vertify = "图书归还成功，状态已更新" });
        }

       
  //    API: 回复漂流图书申请 
//    HTTP 请求方法: POST
//    请求路径: /api/email/reply
//    前端输入：
//    o token: (char(30)) 用户身份验证的 Token，用于验证用户身份
//    o userID: (int) 用户 ID，发起回复的用户唯一标识
//    o exchangeID: (int) 交换记录 ID，表示需要回复的借书请求
//    o detailsB: (string) 回复内容
//                - 如果同意借书请求，那么需要回复支付运费的途径
//                - 如果不同意，则回复内容可以自由发挥，例如拒绝理由
//    o isAgree: (bool) 是否同意借书请求
//                - true: 同意申请
//                - false: 拒绝申请
//    后端输出：
//    o vertify: (string) 验证结果，表示是否成功回复申请
//               - "成功回复申请": 回复成功
//               - 其他: 回复失败（例如权限验证失败或交换记录不存在）
[HttpPost("reply")]
public async Task<IActionResult> ReplyToExchangeRequest([FromBody] ReplyExchangeRequest request)
{
    // 1. 验证输入参数是否有效
    if (request == null || string.IsNullOrEmpty(request.token) || request.userID <= 0 || request.exchangeID <= 0)
    {
        return BadRequest(new { vertify = "输入参数无效" });
    }

    // 2. 验证 token 和 userID
    var isValidUser = ValidateUser(request.token, request.userID);
    if (!isValidUser)
    {
        return Unauthorized(new { vertify = "无效的 token 或 userID" });
    }

    // 3. 检查 exchangeID 是否存在
    var exchange = await _context.ExchangeDetail.FirstOrDefaultAsync(e => e.exchangeID == request.exchangeID);
    if (exchange == null)
    {
        return NotFound(new { vertify = "交换请求不存在" });
    }

    // 4. 验证用户是否有权限回复
    if (exchange.idb != request.userID)
    {
        return Forbid(new { vertify = "用户无权限回复该申请" });
    }

    // 5. 根据 isAgree 更新申请状态和回复内容
    if (request.isAgree)
    {
        // 同意申请，更新 detailsB 和 isApproved
        exchange.isApproved = true;
        exchange.detailsB = request.detailsB; // 回复支付路径或其他同意内容
    }
    else
    {
        // 拒绝申请，更新 detailsB
        exchange.isApproved = false;
        exchange.detailsB = request.detailsB; // 回复拒绝的理由
    }

    // 6. 保存修改到数据库
    _context.ExchangeDetail.Update(exchange);
    await _context.SaveChangesAsync();

    // 7. 返回操作结果
    return Ok(new { vertify = "成功回复申请" });
}




    }
}
