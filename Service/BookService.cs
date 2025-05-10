namespace LibraryManager.Service;

using System.Collections.Generic;
using LibraryManager.Domain;
using LibraryManager.Repository;
using LibraryManager.Validators;

public class BookService : IBookService
{
    private readonly IBookRepository _bookRepository;
    private readonly ITransactionRepository _transactionRepository;

    public BookService(IBookRepository bookRepository, ITransactionRepository transactionRepository)
    {
        _transactionRepository = transactionRepository;
        _bookRepository = bookRepository;
    }

    public Book? Add(Book entity)
    {
        BookValidator.Validate(entity);
        return _bookRepository.Add(entity);
    }

    public Book? Delete(int id)
    {
        if(id <= 0)
        {
            throw new ArgumentException("Id must be greater than 0");
        }

        var b = _bookRepository.FindById(id);
        if (b == null)
            return null;
        
        var activeTransactions = _transactionRepository.GetActiveTransactionsByBook(id);
        if(activeTransactions.Any())
            throw new Exception("Cannot delete book with active borrowing transactions");


        return _bookRepository.Delete(id);

    }

    public Book? FindById(int id)
    {
        if(id <= 0)
        {
            throw new ArgumentException("Id must be greater than 0");
        }

        return _bookRepository.FindById(id);
    }

    public IEnumerable<Book> GetAll()
    {
        return _bookRepository.GetAll();
    }

    public Book? Update(int id, Book entity)
    {
        if(id <= 0)
        {
            throw new ArgumentException("Id must be greater than 0");
        }

        BookValidator.Validate(entity);
        return _bookRepository.Update(id, entity);
    }


    public bool BorrowBook(int bookId)
    {
        if (bookId <= 0)
            throw new ArgumentException("Invalid book ID", nameof(bookId));
        var book = _bookRepository.FindById(bookId);
        if (book == null || book.AvailableCopies <= 0)
            return false;

        book.AvailableCopies--;
        _bookRepository.Update(bookId, book);
        return true;
    }


    public bool ReturnBook(int bookId)
    {
        if (bookId <= 0)
            throw new ArgumentException("Invalid book ID", nameof(bookId));
        var book = _bookRepository.FindById(bookId);
        if (book == null || book.AvailableCopies >= book.TotalCopies)
            return false;

        book.AvailableCopies++;
        _bookRepository.Update(bookId, book);
        return true;
    }



    public IEnumerable<Book> SearchBooks(string searchWhat)
    {
        if(string.IsNullOrWhiteSpace(searchWhat))
            throw new ArgumentException("Search query cannot be empty ", nameof(searchWhat));
        return _bookRepository.SearchBooks(searchWhat);
        
    }

}