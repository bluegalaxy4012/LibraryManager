using Microsoft.EntityFrameworkCore;
using LibraryManager.Domain;

namespace LibraryManager.Data;


public class LibraryManagerDbContext : DbContext
{
    public DbSet<Book> Books { get; set; } = null!;
    public DbSet<Transaction> Transactions { get; set; } = null!;

    public LibraryManagerDbContext(DbContextOptions<LibraryManagerDbContext> options) : base(options) {}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Book>().ToTable("Books");
        modelBuilder.Entity<Book>().HasKey(b => b.Id);
        modelBuilder.Entity<Book>().Property(b => b.Title).IsRequired();
        modelBuilder.Entity<Book>().Property(b => b.Author).IsRequired();
        modelBuilder.Entity<Book>().Property(b => b.TotalCopies).IsRequired();
        modelBuilder.Entity<Book>().Property(b => b.AvailableCopies).IsRequired();

        modelBuilder.Entity<Transaction>().ToTable("Transactions");
        modelBuilder.Entity<Transaction>().HasKey(t => t.Id);
        modelBuilder.Entity<Transaction>().Property(t => t.BookId).IsRequired();
        modelBuilder.Entity<Transaction>().Property(t => t.UserId).IsRequired();
        modelBuilder.Entity<Transaction>().Property(t => t.BorrowDate).IsRequired();
        modelBuilder.Entity<Transaction>().Property(t => t.DueDate).IsRequired();
        modelBuilder.Entity<Transaction>().Property(t => t.ReturnDate).IsRequired(false);
    }
}