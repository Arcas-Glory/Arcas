using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using sqlTest.Server.Data;
using sqlTest.Server.Models;
using System.Threading.Tasks;


namespace sqlTest.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[Authorize] // 需要授权才能访问（可根据需求调整）
    public class BooksController : ControllerBase
    {
        private readonly ArcasDbContext _context;

        public BooksController(ArcasDbContext context)
        {
            _context = context;
        }

        // POST: api/Books/upload
        [HttpPost("upload")]
        public async Task<IActionResult> UploadBook([FromBody] Book book)
        {
            // 验证模型状态
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // 检查所有者是否存在
            var owner = await _context.Users.FindAsync(book.OwnerId);
            if (owner == null)
                return BadRequest("所有者ID无效。");

            
            _context.Books.Add(book);
            await _context.SaveChangesAsync();

           
            return CreatedAtAction(nameof(GetBookById), new { id = book.Id }, book);
        }

        // GET: api/Books/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookById(int id)
        {
            var book = await _context.Books
                                     .Include(b => b.OwnerId) 
                                     .FirstOrDefaultAsync(b => b.Id == id);

            if (book == null)
                return NotFound("图书未找到。");

            return Ok(book);
        }

        // PUT: api/Books/edit/{id}
        [HttpPut("edit/{id}")]
        public async Task<IActionResult> EditBook(int id, [FromBody] Book updatedBook)
        {
            if (id != updatedBook.Id)
                return BadRequest("ID不匹配。");

           
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            
            var existingBook = await _context.Books.FindAsync(id);
            if (existingBook == null)
                return NotFound("图书未找到。");

            
            existingBook.Title = updatedBook.Title;
            existingBook.ISBN = updatedBook.ISBN;
            existingBook.OwnerId = updatedBook.OwnerId;
            existingBook.Description = updatedBook.Description;

            var newOwner = await _context.Users.FindAsync(updatedBook.OwnerId);
            if (newOwner == null)
                return BadRequest("新的所有者ID无效。");

            // 标记实体为修改状态
            _context.Entry(existingBook).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookExists(id))
                    return NotFound("图书未找到。");
                else
                    throw;
            }

            return NoContent();
        }

        // DELETE: api/Books/delete/{id}
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null)
                return NotFound("图书未找到。");

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // GET: api/Books
        [HttpGet]
        [AllowAnonymous] 
        public async Task<IActionResult> GetAllBooks()
        {
            var books = await _context.Books
                                      .Include(b => b.OwnerId) 
                                      .ToListAsync();
            return Ok(books);
        }

        // GET: api/Books/search?title=xxx&isbn=yyy
        [HttpGet("search")]
        [AllowAnonymous] 
        public async Task<IActionResult> SearchBooks([FromQuery] string title, [FromQuery] string isbn)
        {
            var query = _context.Books.AsQueryable();

            if (!string.IsNullOrEmpty(title))
                query = query.Where(b => b.Title.Contains(title));

            if (!string.IsNullOrEmpty(isbn))
                query = query.Where(b => b.ISBN.Contains(isbn));

            var results = await query.Include(b => b.OwnerId).ToListAsync();

            return Ok(results);
        }

        // 辅助方法：检查图书是否存在
        private bool BookExists(int id)
        {
           return _context.Books.Any(e => e.Id == id);
       }
    }
}