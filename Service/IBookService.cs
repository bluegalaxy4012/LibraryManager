namespace LibraryManager.Service;

using LibraryManager.Domain;

public interface IBookService
{
    Book? Add(Book entity);

    Book? Delete(int id);

    Book? FindById(int id);

    Book? Update(int id, Book entity);

    IEnumerable<Book> GetAll();

    bool BorrowBook(int bookId);

    bool ReturnBook(int bookId);

    IEnumerable<Book> SearchBooks(string searchWhat);

}