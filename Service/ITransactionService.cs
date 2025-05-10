namespace LibraryManager.Service;

using LibraryManager.Domain;


public interface ITransactionService
{
    Transaction? Add(Transaction entity);

    Transaction? Delete(int id);

    Transaction? FindById(int id);

    Transaction? Update(int id, Transaction entity);

    IEnumerable<Transaction> GetAll();


    IEnumerable<Transaction> GetOverdueTransactions(DateTime date);

    Transaction? BorrowBook(int bookId, int userId);

    Transaction? ReturnBook(int transactionId);
}