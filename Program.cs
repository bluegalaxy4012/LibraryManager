using LibraryManager.Data;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using LibraryManager.Repository;
using LibraryManager.Service;
using LibraryManager.UI;



class Program
{
    static void Main(string[] args)
    {
        var services = new ServiceCollection();

        string connectionString = "Data Source=library.db";


        services.AddDbContext<LibraryManagerDbContext>(options => options.UseSqlite(connectionString));

        services.AddScoped<IBookRepository, BookRepository>();
        services.AddScoped<ITransactionRepository, TransactionRepository>();
        services.AddScoped<IBookService>(provider => 
            new BookService(
                provider.GetRequiredService<IBookRepository>(),
                provider.GetRequiredService<ITransactionRepository>()));
        services.AddScoped<ITransactionService, TransactionService>();
        services.AddScoped<UI>();


        var serviceProvider = services.BuildServiceProvider();
        using (var scope = serviceProvider.CreateScope())
        {
            scope.ServiceProvider.GetRequiredService<LibraryManagerDbContext>().Database.EnsureCreated();
        }
        var ui = serviceProvider.GetRequiredService<UI>();
        ui.Run();
    }
}