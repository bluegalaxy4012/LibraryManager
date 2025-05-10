using LibraryManager.Domain;

namespace LibraryManager.Validators;


public static class TransactionValidator
{
    public static void Validate(Transaction transaction)
    {
        
        if (transaction == null)
            throw new ArgumentNullException(nameof(transaction));

        if (transaction.BookId <= 0)
            throw new ArgumentException("Book ID must be greater than 0", nameof(transaction.BookId));

        if (transaction.UserId <= 0)
            throw new ArgumentException("User ID must be greater than 0", nameof(transaction.UserId));

        if (transaction.BorrowDate == default)
            throw new ArgumentException("Borrow date must be set.", nameof(transaction.BorrowDate));

        if (transaction.DueDate == default || transaction.DueDate <= transaction.BorrowDate)
            throw new ArgumentException("Due date must be after borrow date.", nameof(transaction.DueDate));

        if (transaction.ReturnDate.HasValue && transaction.ReturnDate < transaction.BorrowDate)
            throw new ArgumentException("Return date cannot be before borrow date.", nameof(transaction.ReturnDate));
    }
}   