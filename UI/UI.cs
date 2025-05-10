using LibraryManager.Domain;
using LibraryManager.Service;


namespace LibraryManager.UI;


public class UI
{
    private readonly IBookService _bookService;
    private readonly ITransactionService _transactionService;



    public UI(IBookService bookService, ITransactionService transactionService)
    {
        _bookService = bookService;
        _transactionService = transactionService;
    }




    public void Run()
    {
        while (true)
        {
            Console.WriteLine("\n\nLibrary Management System");

            Console.WriteLine("1) Add Book");
            Console.WriteLine("2) Delete Book");
            Console.WriteLine("3) Update Book");
            Console.WriteLine("4) Get All Books");
            Console.WriteLine("5) Find Book By Id");
            Console.WriteLine("6) Search Books");
            Console.WriteLine("7) Borrow Book");
            Console.WriteLine("8) Return Book");
            Console.WriteLine("9) View overdue Books");
            Console.WriteLine("10) View active Transactions for Book");
            Console.WriteLine("11) Exit");
            Console.Write("Enter choice (1-11): ");



            string? choice = Console.ReadLine();
            try
            {
                switch (choice)
                {
                    case "1":
                        AddBook();
                        break;
                    case "2":
                        DeleteBook();
                        break;
                    case "3":
                        UpdateBook();
                        break;
                    case "4":
                        GetAllBooks();
                        break;
                    case "5":
                        FindBookById();
                        break;
                    case "6":
                        SearchBooks();
                        break;
                    case "7":
                        BorrowBook();
                        break;
                    case "8":
                        ReturnBook();
                        break;
                    case "9":
                        ViewOverdueBooks();
                        break;
                    case "10":
                        ViewActiveTransactionsForBook();
                        break;
                    case "11":
                        return;
                    default:
                        Console.WriteLine("Invalid choice");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

        }
    }



    private void AddBook()
    {

        Console.Write("Enter book title: ");
        string? title = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(title))
        {
            Console.WriteLine("Title cannot be empty");
            return;
        }

        Console.Write("Enter book author: ");
        string? author = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(author))
        {
            Console.WriteLine("Author cannot be empty");
            return;
        }

        Console.Write("Enter total copies: ");
        if (!int.TryParse(Console.ReadLine(), out int totalCopies) || totalCopies < 0)
        {
            Console.WriteLine("Invalid total copies, should be a positive number");
            return;
        }

        Console.Write("Enter available copies: ");
        if (!int.TryParse(Console.ReadLine(), out int availableCopies) || availableCopies < 0)
        {
            Console.WriteLine("Invalid available copies, should be a positive number");
            return;
        }



        var b = new Book
        {
            Title = title,
            Author = author,
            TotalCopies = totalCopies,
            AvailableCopies = availableCopies
        };


        var added = _bookService.Add(b);

        if (added != null)
            Console.WriteLine($"Book added successfully with ID: {added.Id}");
        else
            Console.WriteLine("Failed to add book");

    }






    private void DeleteBook()
    {
        Console.Write("Enter book ID to delete: ");
        if (!int.TryParse(Console.ReadLine(), out int id) || id <= 0)
        {
            Console.WriteLine("Invalid ID. Must be a positive number");
            return;
        }


        var deleted = _bookService.Delete(id);

        if (deleted != null)
            Console.WriteLine($"Book {id} deleted successfully");
        else
            Console.WriteLine("Book not found or cannot be deleted");
    }




    private void UpdateBook()
    {

        Console.Write("Enter book ID to update: ");
        if (!int.TryParse(Console.ReadLine(), out int id) || id <= 0)
        {
            Console.WriteLine("Invalid ID, should be a positive number");
            return;
        }

        Console.Write("Enter new book title: ");
        string? title = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(title))
        {
            Console.WriteLine("Title cannot be empty");
            return;
        }

        Console.Write("Enter new book author: ");
        string? author = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(author))
        {
            Console.WriteLine("Author cannot be empty");
            return;
        }

        Console.Write("Enter new total copies: ");
        if (!int.TryParse(Console.ReadLine(), out int totalCopies) || totalCopies < 0)
        {
            Console.WriteLine("Invalid total copies, should be a positive number");
            return;
        }

        Console.Write("Enter new available copies: ");
        if (!int.TryParse(Console.ReadLine(), out int availableCopies) || availableCopies < 0)
        {
            Console.WriteLine("Invalid available copies, should be a positive number");
            return;
        }



        var b = new Book
        {
            Title = title,
            Author = author,
            TotalCopies = totalCopies,
            AvailableCopies = availableCopies
        };

        var updated = _bookService.Update(id, b);
        if (updated != null)
            Console.WriteLine($"Book {id} updated successfully");
        else
            Console.WriteLine("Book not found or update failed");
    }




    private void GetAllBooks()
    {
        var list = _bookService.GetAll();

        if (!list.Any())
        {
            Console.WriteLine("No books found");
            return;
        }


        Console.WriteLine("All Books:");

        foreach (var b in list)
        {
            Console.WriteLine($"ID: {b.Id}, Title: {b.Title}, Author: {b.Author}, Total Copies: {b.TotalCopies}, Available Copies: {b.AvailableCopies}");
        }
    }



    private void FindBookById()
    {
        Console.Write("Enter book ID to find: ");
        if (!int.TryParse(Console.ReadLine(), out int id) || id <= 0)
        {
            Console.WriteLine("Invalid ID, should be a positive number");
            return;
        }


        var b = _bookService.FindById(id);
        if (b != null)
            Console.WriteLine($"ID: {b.Id}, Title: {b.Title}, Author: {b.Author}, Total Copies: {b.TotalCopies}, Available Copies: {b.AvailableCopies}");
        else
            Console.WriteLine("Book not found");
    }





    private void SearchBooks()
    {
        Console.Write("Enter search string (that could be contained in the title or author): ");
        string? searchWhat = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(searchWhat))
        {
            Console.WriteLine("Search string cannot be empty");
            return;
        }



        var found = _bookService.SearchBooks(searchWhat);

        if (!found.Any())
        {
            Console.WriteLine("No books found");
            return;
        }

        Console.WriteLine("Search Results:");
        foreach (var b in found)
        {
            Console.WriteLine($"ID: {b.Id}, Title: {b.Title}, Author: {b.Author}, Total Copies: {b.TotalCopies}, Available Copies: {b.AvailableCopies}");
        }
    }




    private void BorrowBook()
    {
        Console.Write("Enter book ID to borrow: ");
        if (!int.TryParse(Console.ReadLine(), out int bookId) || bookId <= 0)
        {
            Console.WriteLine("Invalid book ID, should be a positive number");
            return;
        }


        Console.Write("Enter user ID: ");
        if (!int.TryParse(Console.ReadLine(), out int userId) || userId <= 0)
        {
            Console.WriteLine("Invalid user ID, should be a positive number");
            return;
        }


        var t = _transactionService.BorrowBook(bookId, userId);
        if (t != null)
            Console.WriteLine($"Book {bookId} borrowed successfully. Transaction ID: {t.Id}, Due: {t.DueDate:yyyy-MM-dd}");
        else
            Console.WriteLine("Failed to borrow book");
    }



    private void ReturnBook()
    {
        Console.Write("Enter transaction ID to return: ");
        if (!int.TryParse(Console.ReadLine(), out int tid) || tid <= 0)
        {
            Console.WriteLine("Invalid transaction ID, should be a positive number");
            return;
        }


        var t = _transactionService.ReturnBook(tid);
        if (t != null)
            Console.WriteLine($"Book returned successfully for transaction {tid}");
        else
            Console.WriteLine("Failed to return book");
    }





    private void ViewOverdueBooks()
    {
        var overdue = _transactionService.GetOverdueTransactions(DateTime.Now);

        if (!overdue.Any())
        {
            Console.WriteLine("No overdue books found");
            return;
        }


        Console.WriteLine("Overdue Books:");

        foreach (var t in overdue)
        {
            var b = _bookService.FindById(t.BookId);
            Console.WriteLine($"Transaction {t.Id}: Book {t.BookId} ({b?.Title ??"Unknown"}) borrowed by {t.UserId}, Due: {t.DueDate:yyyy-MM-dd}");
        }
    }





    private void ViewActiveTransactionsForBook()
    {

        Console.Write("Enter book ID to view active transactions: ");
        if (!int.TryParse(Console.ReadLine(), out int bookId) || bookId <= 0)
        {
            Console.WriteLine("Invalid book ID, should be a positive number");
            return;
        }

        var b = _bookService.FindById(bookId);
        if (b == null)
        {
            Console.WriteLine("Book not found");
            return;
        }


        var active = _transactionService.GetAll().Where(t => t.BookId == bookId && t.ReturnDate == null);
        if (!active.Any())
        {
            Console.WriteLine($"No active transactions for book {bookId} ({b.Title})");
            return;
        }

        Console.WriteLine($"Active Transactions for Book {bookId} ({b.Title}):");

        foreach (var t in active)
        {
            Console.WriteLine($"Transaction {t.Id}: Borrowed by {t.UserId}, Due: {t.DueDate:yyyy-MM-dd}");
        }
    }

}
