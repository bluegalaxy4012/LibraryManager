using LibraryManager.Domain;

namespace LibraryManager.Repository;

public interface ITransactionRepository : IRepository<Transaction>
{
    IEnumerable<Transaction> GetActiveTransactionsByBook(int bookId);
    IEnumerable<Transaction> GetOverdueTransactions(DateTime date);
}