using LibraryManager.Domain;

namespace LibraryManager.Validators;

public static class BookValidator
{
    public static void Validate(Book book)
    {
        if(book == null)
        {
            throw new ArgumentNullException(nameof(book), "Book cannot be null");
        }

        if(string.IsNullOrWhiteSpace(book.Title))
        {
            throw new ArgumentException("Title cannot be empty", nameof(book.Title));
        }

        if(string.IsNullOrWhiteSpace(book.Author))
        {
            throw new ArgumentException("Author cannot be empty", nameof(book.Author));
        }

        if(book.TotalCopies < 0)
        {
            throw new ArgumentException(nameof(book.TotalCopies), "Total copies cannot be negative");
        }

        if(book.AvailableCopies < 0)
        {
            throw new ArgumentException(nameof(book.AvailableCopies), "Available copies cannot be negative");
        }
    }
}