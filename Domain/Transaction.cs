namespace LibraryManager.Domain;

public class Transaction : Identifiable
{
    public int BookId { get; set; }
    public int UserId { get; set; }


    public DateTime BorrowDate { get; set; }

    public DateTime DueDate { get; set; }

    public DateTime? ReturnDate { get; set; }
}