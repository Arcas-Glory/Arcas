using Microsoft.EntityFrameworkCore;
using sqlTest.Server.Data;
using sqlTest.Server.Models;
using sqlTest.Server.Repositories.Interfaces;
using System.Threading.Tasks;


namespace sqlTest.Server.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly ArcasDbContext _context;

        public BookRepository(ArcasDbContext context)
        {
            _context = context;
        }

        public async Task AddBookAsync(Book book)
        {
            _context.Books.Add(book);
            await _context.SaveChangesAsync();
        }

        public async Task<Book> GetBookByIdAsync(int bookId)
        {
            return await _context.Books
                                 .Include(b => b.OwnerId) 
                                 .SingleOrDefaultAsync(b => b.Id == bookId);
        }

        // 实现其他方法（如编辑、删除）
    }
}
