
using LibraryManager.Domain;
namespace LibraryManager.Repository;

public interface IBookRepository : IRepository<Book>
{
    public IEnumerable<Book> SearchBooks(string searchWhat);
}