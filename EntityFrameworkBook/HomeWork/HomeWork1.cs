using EntityFrameworkBook.Config;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkBook.HomeWork;

public class HomeWork1
{
    public static void Tracking()
    {
        using (var context = new AppDbContext())
        {
            //Select
            var entity = context.Books.AsTracking().FirstOrDefault(e => e.BookId == 1);

            // Update
            if (context != null)
            {
                entity.Title = "Updated Title";
                context.SaveChanges();
            }

            // Delete
            if (entity != null)
            {
                context.Books.Remove(entity);
                context.SaveChanges();
            }
        }
    }
    
    
    public static void NoTracking()
    {
        using (var context = new AppDbContext())
        {
            //Select
            var entity = context.Books.FirstOrDefault(e => e.BookId == 1);
            context.ChangeTracker.Clear();
            // Update
            if (context != null)
            {
                entity.Title = "Updated Title";
                context.Books.Update(entity);
                context.SaveChanges();
            }

            // Delete
            if (entity != null)
            {
                context.Books.Attach(entity);
                // Устанавливаем состояние сущности как изменённое
                context.Entry(entity).State = EntityState.Modified;
                context.Books.Remove(entity);
                context.SaveChanges();
            }
        }
    }
}