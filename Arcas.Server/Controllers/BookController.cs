using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Arcas.Server.Models;    // 包含ApplicationDbContext、ConcreteBook、User等
using Arcas.Server.DTOs;     // 包含BookUploadRequest, BookUploadResponse
using Arcas.Server.Helpers;
using Arcas.Server.Data;  // 包含TokenHelper
using Microsoft.AspNetCore.Cors;
using YourNamespace.DTOs;

namespace Arcas.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [EnableCors("AllowLocalhost5173")]
    public class BookController : ControllerBase
    {
        private readonly ArcasDbContext _context;

        public BookController(ArcasDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// 图书上传接口
        /// POST /api/book/upload
        /// 前端输入: token, userID, ISBN, cover, category
        /// 后端输出: vertify (success 或 fail)
        /// </summary>
        [HttpPost("upload")]
        public async Task<ActionResult<BookUploadResponse>> UploadBook([FromBody] BookUploadRequest request)
        {
            // 验证token
            bool tokenValid = TokenHelper.Check(_context, request.userID, request.token);
            if (!tokenValid)
            {
                return BadRequest(new BookUploadResponse { vertify = "fail" });
            }

            // ISBN是否存在于AbstractBook中，如不存在可根据需求创建或直接记录
            var abstractBook = await _context.AbstractBook.FirstOrDefaultAsync(a => a.ISBN == request.ISBN);
            if (abstractBook == null)
            {
                var newAbstractBook = new AbstractBook
                {
                    ISBN = request.ISBN,
                    title = request.title,
                    category = request.category,
                    author = request.author,
                    publishHouse = request.publishHouse
                };
                _context.AbstractBook.Add(newAbstractBook);
                await _context.SaveChangesAsync();
            }

            // 创建ConcreteBook
            var newConcreteBook = new ConcreteBook
            {
                ISBN = request.ISBN,
                userID = request.userID,
                bookState = 1, // 假设1为“可漂流”或上架状态
                cover = request.cover,
            };
            _context.ConcreteBook.Add(newConcreteBook);

            await _context.SaveChangesAsync();

            return new BookUploadResponse { vertify = "success" };
        }

        [HttpGet("get")]
        public async Task<ActionResult<BookDetailResponse>> GetFromID([FromQuery] int bookID)
        {
            // 从数据库中查询指定bookID的ConcreteBook，并加载相关导航属性
            var book = await _context.ConcreteBook
                .Include(b => b.AbstractBook)
                .Include(b => b.User)
                .Include(b => b.Comments)
                    .ThenInclude(c => c.User)
                .FirstOrDefaultAsync(b => b.bookID == bookID);

            if (book == null)
            {
                return new BookDetailResponse { vertify = "fail" };
            }

            // 将评论列表映射为响应所需格式
            var commentDtos = book.Comments
                .Select(c => new BookDetailCommentDto
                {
                    commentID = c.commentID,
                    likeNum = c.likeNum,
                    nickname = c.User?.nickname,
                    commentContent = c.commentContent
                }).ToList();

            var response = new BookDetailResponse
            {
                vertify = "success",
                title = book.AbstractBook?.title,    // 书名来自AbstractBook
                ISBN = book.ISBN,
                nickname = book.User?.nickname,       // 拥有者的昵称
                bookState = book.bookState,
                cover = book.cover,
                category = book.AbstractBook.category,
                comments = commentDtos
            };

            return response;
        }

        [HttpGet("getall")]
        public async Task<ActionResult<AllBookResponse>> GetAllWithPermition()
        {
            // 查询所有可借阅书籍，假设bookState=1为可借阅状态
            var availableBooks = await _context.ConcreteBook
                .Include(b => b.AbstractBook)
                .Where(b => b.bookState == 1)
                .ToListAsync();

            // 如果没有可借阅的书籍
            if (availableBooks == null || !availableBooks.Any())
            {
                return new AllBookResponse
                {
                    vertify = "fail",
                    bookLists = new List<AllBookItemDto>()
                };
            }

            // 将查询结果映射为DTO
            var bookDtos = availableBooks.Select(b => new AllBookItemDto
            {
                bookID = b.bookID,
                title = b.AbstractBook?.title, // 从 AbstractBook 获取书名
                ISBN = b.ISBN,
                bookState = b.bookState,
                cover = b.cover,
                category = b.AbstractBook.category
            }).ToList();

            // 返回结果
            return new AllBookResponse
            {
                vertify = "success",
                bookLists = bookDtos
            };
        }
        /// <summary>
        /// 获取用户账号下所拥有的图书
        /// GET /api/book/getMyBook?userID=xxx
        /// 后端输出：vertify, bookLists
        /// </summary>
        [HttpGet("getmine")]
        public async Task<ActionResult<BookGetmineResponse>> GetMyBooks([FromQuery] int userID, [FromHeader] string token)
        {
            // 可选：验证token，如果需要的话
            // 假设 TokenHelper.Check(userID, token) 返回 bool
            if (!TokenHelper.Check(_context, userID, token))
            {
                return BadRequest(new BookGetmineResponse
                {
                    vertify = "fail:Token is wrong",
                    bookLists = new List<BookGetmineResponseDTO>()
                });
            }

            // 检查用户是否存在
            var user = await _context.User.FindAsync(userID);
            if (user == null)
            {
                return BadRequest(new BookGetmineResponse
                {
                    vertify = "fail:UserNotExist",
                    bookLists = new List<BookGetmineResponseDTO>()
                });
            }

            // 查询用户的所有图书
            var userBooks = await _context.ConcreteBook
                .Include(b => b.AbstractBook)
                .Where(b => b.userID == userID)
                .ToListAsync();

            // 检查是否有图书
            if (userBooks == null || !userBooks.Any())
            {
                return Ok(new BookGetmineResponse
                {
                    vertify = "fail: book Not Exist",
                    bookLists = new List<BookGetmineResponseDTO>()
                });
            }

            // 映射为DTO
            var bookDtos = userBooks.Select(b => new BookGetmineResponseDTO
            {
                bookID = b.bookID,
                title = b.AbstractBook?.title,  // 从 AbstractBook 获取书名
                ISBN = b.ISBN,
                bookState = b.bookState,
                cover = b.cover,
                category = b.AbstractBook.category
            }).ToList();

            // 返回成功结果
            return Ok(new BookGetmineResponse
                {
                    vertify = "success",
                    bookLists = bookDtos
            });
        }
    }
}
