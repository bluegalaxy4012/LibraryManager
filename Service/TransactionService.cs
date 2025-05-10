using LibraryManager.Domain;
using LibraryManager.Repository;
using LibraryManager.Validators;
using System;
using System.Collections.Generic;

namespace LibraryManager.Service;

public class TransactionService : ITransactionService
{
    private readonly ITransactionRepository _transactionRepository;
    private readonly IBookService _bookService;



    public TransactionService(ITransactionRepository transactionRepository, IBookService bookService)
    {
        _transactionRepository = transactionRepository;
        _bookService = bookService;
    }

    public Transaction? Add(Transaction transaction)
    {
        TransactionValidator.Validate(transaction);
        return _transactionRepository.Add(transaction);
    }

    public Transaction? FindById(int id)
    {
        if (id <= 0)
            throw new ArgumentException("Invalid transaction ID", nameof(id));
        return _transactionRepository.FindById(id);
    }

    public Transaction? Update(int id, Transaction transaction)
    {
        if (id <= 0)
            throw new ArgumentException("Invalid transaction ID", nameof(id));
        TransactionValidator.Validate(transaction);
        return _transactionRepository.Update(id, transaction);
    }

    public Transaction? Delete(int id)
    {
        if (id <= 0)
            throw new ArgumentException("Invalid transaction ID", nameof(id));
        return _transactionRepository.Delete(id);
    }

    public IEnumerable<Transaction> GetAll()
    {
        return _transactionRepository.GetAll();
    }

    public Transaction? BorrowBook(int bookId, int userId)
    {
        if (bookId <= 0)
            throw new ArgumentException("Invalid book ID", nameof(bookId));
        if (userId <= 0)
            throw new ArgumentException("Invalid user ID", nameof(userId));

        if (!_bookService.BorrowBook(bookId))
            return null;

        var transaction = new Transaction
        {
            BookId = bookId,
            UserId = userId,
            BorrowDate = DateTime.Now,
            DueDate = DateTime.Now.AddDays(30), // am pus sa fie de returnat intr-o luna
            ReturnDate = null
        };


        TransactionValidator.Validate(transaction);
        return _transactionRepository.Add(transaction);

    }



    public Transaction? ReturnBook(int transactionId)
    {

        if (transactionId <= 0)
            throw new ArgumentException("Invalid transaction ID", nameof(transactionId));

        var transaction = _transactionRepository.FindById(transactionId);
        if (transaction == null || transaction.ReturnDate != null)
            return null;

        if (!_bookService.ReturnBook(transaction.BookId))
            return null;

        transaction.ReturnDate = DateTime.Now;
        TransactionValidator.Validate(transaction);
        return _transactionRepository.Update(transactionId, transaction);
    }



    public IEnumerable<Transaction> GetOverdueTransactions(DateTime date)
    {
        return _transactionRepository.GetOverdueTransactions(date);
    }
}