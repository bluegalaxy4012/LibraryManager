namespace LibraryManager.Repository;

using System.Collections.Generic;
using System.Data.Entity;
using LibraryManager.Data;
using LibraryManager.Domain;

public class BookRepository : IBookRepository
{

    private readonly LibraryManagerDbContext context;
    public BookRepository(LibraryManagerDbContext context)
    {
        this.context = context;
    }

    public Book? Add(Book entity)
    {
        context.Books.Add(entity);
        context.SaveChanges();
        return entity;
    }

    public Book? Delete(int id)
    {
        var b = context.Books.Find(id);

        if(b == null)
            return null;

        context.Books.Remove(b);
        context.SaveChanges();
        return b;

    }

    public Book? FindById(int id)
    {
        return context.Books.Find(id);
    }

    public IEnumerable<Book> GetAll()
    {
        return context.Books.AsNoTracking().ToList();
    }

    public Book? Update(int id, Book entity)
    {
        var b = context.Books.Find(id);
        if(b == null)
            return null;

        b.Title = entity.Title;
        b.Author = entity.Author;
        b.TotalCopies = entity.TotalCopies;
        b.AvailableCopies = entity.AvailableCopies;
        context.SaveChanges();

        return b;
    }

    public IEnumerable<Book> SearchBooks(string searchWhat)
    {
        return context.Books
            .Where(b => b.Title.Contains(searchWhat) || b.Author.Contains(searchWhat))
            .AsNoTracking().ToList();
    }

}