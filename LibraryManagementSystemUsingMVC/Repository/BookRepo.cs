using LibraryManagementSystemUsingMVC.Data;
using LibraryManagementSystemUsingMVC.Models;

namespace LibraryManagementSystemUsingMVC.Repository
{
    public class BookRepo :CommonRepo<Book>, IBookRepo
    {
        private readonly ApplicationDbContext _context;
        public BookRepo(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public bool BookExists(string title)
        {
            return _context.BookData.Any(b => b.BookTitle == title);
        }
        public IEnumerable<Book> GetBooksByGenre(string? genre)
        {
            var query = _context.BookData.AsQueryable();
            if (!string.IsNullOrEmpty(genre))
            {
                query = query.Where(b => b.Genre == genre);
            }
            return (IEnumerable<Book>)query.ToList();
        }

    }
}
