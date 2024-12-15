using sqlTest.Server.Models;
using System.Threading.Tasks;


namespace sqlTest.Server.Repositories.Interfaces
{
    public interface IBookRepository
    {
        Task AddBookAsync(Book book);
        Task<Book> GetBookByIdAsync(int bookId);
        // 其他必要的方法（如编辑、删除等）
    }
}
