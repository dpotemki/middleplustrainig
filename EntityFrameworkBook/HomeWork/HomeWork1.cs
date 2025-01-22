using EntityFrameworkBook.Config;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkBook.HomeWork;

public class HomeWork1
{
    public static void SelectNoTracking()
    {
        using (var db = new AppDbContext())
        {
            /*var book = db.Books.AsNoTracking().FirstOrDefault();*/
            //Могу задать через свойство.
            db.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            var book = db.Books.FirstOrDefault();
            if (book == null)
            {
                Console.WriteLine("No book (SelectNoTracking)");
            }

            Console.WriteLine("The Title:");
            Console.WriteLine(book.Title);
        }
    }

    public static void SelectTracking()
    {
        using (var db = new AppDbContext())
        {
            // Получаем сущность без отслеживания
            db.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.TrackAll;
            var book = db.Books.FirstOrDefault();
            if (book == null)
            {
                Console.WriteLine("No book (SelectTracking)");
            }

            Console.WriteLine("The Title:");
            Console.WriteLine(book.Title);
        }
    }

    public static void UpdateTracking()
    {
        using (var db = new AppDbContext())
        {
            // Получаем сущность без отслеживания
            var book = db.Books.AsNoTracking().FirstOrDefault(b => b.BookId == 1);
            if (book == null)
            {
                Console.WriteLine("No book (UpdateTracking)");
                return;
            }
            
            Console.WriteLine($"Book Title: {book?.Title}");
            // Изменяем данные
            var oldTitle = book.Title;
            book.Title = "Новый заголовок";

            // Проверяем, изменилось ли значение
            if (oldTitle != book.Title)
            {
                // Обновляем сущность, получим exception!
                db.Update(book);

                // Сохраняем изменения
                try
                {
                    db.SaveChanges();
                    Console.WriteLine($"Book Title Changed: {book.Title}");
                }
                catch (DbUpdateException ex)
                {
                    Console.WriteLine($"Error updating book: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("No changes were made to the book.");
            }
        }
    }
    
    
    public static void DeleteTracking()
    {
        using (var db = new AppDbContext())
        {
            // Получаем сущность без отслеживания
            var book = db.Books.AsNoTracking().FirstOrDefault(b => b.BookId == 1);
            if (book == null)
            {
                Console.WriteLine("No book (UpdateTracking)");
                return;
            }
            var entityToDelete = db.Books.Find(book.BookId);
            // Удаляем
            if (book != null)
            {
                db.Remove(entityToDelete);
                db.SaveChanges();
                Console.WriteLine($"{book.BookId} deleted");
                return;
            }
            
            Console.WriteLine($"{book.BookId} not found");
        }
    }
    
}