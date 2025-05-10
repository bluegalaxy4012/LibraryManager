using System.Collections.Generic;
using System.Data.Entity;
using LibraryManager.Data;
using LibraryManager.Domain;

namespace LibraryManager.Repository;



public class TransactionRepository : ITransactionRepository
{
    private readonly LibraryManagerDbContext _context;

    public TransactionRepository(LibraryManagerDbContext context)
    {
        _context = context;
    }

    public Transaction? Add(Transaction entity)
    {
        _context.Transactions.Add(entity);
        _context.SaveChanges();
        return entity;
    }


    public Transaction? FindById(int id)
    {
        return _context.Transactions.Find(id);
    }

    public Transaction? Update(int id, Transaction entity)
    {
        var existing = _context.Transactions.Find(id);
        if (existing == null)
            return null;

        existing.BookId = entity.BookId;
        existing.UserId = entity.UserId;
        existing.BorrowDate = entity.BorrowDate;
        existing.DueDate = entity.DueDate;
        existing.ReturnDate = entity.ReturnDate;
        _context.SaveChanges();
        return existing;
    }

    public Transaction? Delete(int id)
    {
        var transaction = _context.Transactions.Find(id);
        if (transaction == null)
            return null;

        _context.Transactions.Remove(transaction);
        _context.SaveChanges();
        return transaction;
    }

    public IEnumerable<Transaction> GetAll()
    {
        return _context.Transactions.AsNoTracking().ToList();
    }

    public IEnumerable<Transaction> GetActiveTransactionsByBook(int bookId)
    {
        return _context.Transactions
            .Where(t => t.BookId == bookId && t.ReturnDate == null)
            .AsNoTracking()
            .ToList();
    }

    public IEnumerable<Transaction> GetOverdueTransactions(DateTime currentDate)
    {
        return _context.Transactions
            .Where(t => t.ReturnDate == null && t.DueDate < currentDate)
            .AsNoTracking()
            .ToList();
    }
}