using LibraryManagementSystemUsingMVC.Models;

namespace LibraryManagementSystemUsingMVC.Repository
{
    public interface IBookRepo : ICommonRepo<Book>
    {
        bool BookExists(string title);
        IEnumerable<Book> GetBooksByGenre(string? genre);
    }
}
