namespace LibraryManager.Domain;


public class Book : Identifiable
{
    public string Title { get; set; } = string.Empty;

    public string Author { get; set; } = string.Empty;

    public int TotalCopies { get; set; }

    public int AvailableCopies { get; set; }    
}