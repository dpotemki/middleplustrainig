using EntityFrameworkBook.Config;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace EntityFrameworkBook.Workers;

public class BookReaderWorker(AppDbContext dbContext) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                var books = await dbContext.Books.Include(book => book.Author)
                    .ToListAsync(cancellationToken: stoppingToken);

                // Обработка полученных книг
                foreach (var book in books)
                {
                    Console.WriteLine($"Книга: {book.Title}, Автор: {book.Author}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при чтении книг: {ex.Message}");
            }

            await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken);
        }
    }
}