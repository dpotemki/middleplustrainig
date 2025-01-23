using EntityFrameworkBook.Config;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkBook.HomeWork;

public class HomeWork2
{
    public static async void UpdateOne()
    {
        try
        {
            await using (var db = new AppDbContext())
            {
                await db.Books
                    .Where(b => b.BookId == 1)
                    .ExecuteUpdateAsync(setters => setters.SetProperty(b => b.Title, "Ура обновил"));
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    }


    public static async void UpdateMany()
    {
        try
        {
            await using (var db = new AppDbContext())
            {
                await db.Books
                    .Where(b => b.BookId < 3)
                    .ExecuteUpdateAsync(setters => setters
                        .SetProperty(b => b.Title, "Снова обновил")
                        .SetProperty(b => b.PublishedOn, DateTime.Now));
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    }

    public static async void UpdateBooksSql()
    {
        try
        {
            await using var db = new AppDbContext();
            var newTitle = "Вот так";
            var bookId = 2;
            string sql = $"UPDATE Books SET Title = {newTitle} WHERE BookId = {bookId}";
            await db.Database.ExecuteSqlRawAsync(
                "UPDATE Books SET Title = {0} WHERE BookId < {1}", 
                newTitle, bookId);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
        
    }
}