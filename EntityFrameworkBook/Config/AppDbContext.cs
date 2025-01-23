using System.Text.Json;
using EntityFrameworkBook.Models.Book;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkBook.Config;

public sealed class AppDbContext : DbContext
{
    public AppDbContext()
    {
        Database.EnsureDeleted(); // удаляем бд со старой схемой
        Database.EnsureCreated(); // создаем бд с новой схемой
        Initialize();
    }

    public void Initialize()
    {
        if (!Database.CanConnect())
        {
            Database.EnsureCreated();
        }

        if (!Authors.Any())
        {
            Authors.AddRange(
                new Author() { AuthorId = 1, Name = "Иван Иванов", WebUrl = "Test" },
                new Author { AuthorId = 2, Name = "Петр Петров", WebUrl = "Test2" }
            );
            SaveChanges();
        }

        if (!Books.Any())
        {
            Books.AddRange(
                new Book()
                {
                    BookId = 1, AuthorId = 1, Description = "New Book", Title = "Title new", PublishedOn = DateTime.Now,
                },
                new Book()
                {
                    BookId = 2, AuthorId = 2, Description = "New Book22", Title = "Title new222",
                    PublishedOn = DateTime.Now,
                }
            );
            SaveChanges();
        }
    }

    private const string ConnectionString =
        "Data Source=C:\\Users\\kuznecov\\OneDrive\\Рабочий стол\\LevelUp\\sqlite.db";

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite(ConnectionString)
            .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
    }
    
    public override int SaveChanges()
    {
        
        var options = new JsonSerializerOptions
        {
            WriteIndented = true
        };
        
        // Получаем все сущности, которые были добавлены, удалены или изменены
        var addedEntities = ChangeTracker.Entries().Where(e => e.State == EntityState.Added).Select(e => e.Entity);
        var deletedEntities = ChangeTracker.Entries().Where(e => e.State == EntityState.Deleted).Select(e => e.Entity);
        
        //ToDo подумать как точечно отобразить что поменялось 
        foreach (var entity in addedEntities)
        {
            Console.WriteLine("Добавлены сущности:");
            Console.WriteLine(JsonSerializer.Serialize<object>(entity, options));
        }
        
        foreach (var entity in deletedEntities)
        {
            Console.WriteLine("Удалены сущности:");
            Console.WriteLine(entity);
        }

        foreach (var entry in ChangeTracker.Entries().Where(e => e.State == EntityState.Modified))
        {
            var originalValues = entry.OriginalValues.ToObject();
            var currentValues = entry.CurrentValues.ToObject();

            // Сравнение свойств с помощью рефлексии
            foreach (var property in entry.Entity.GetType().GetProperties())
            {
                var originalValue = property.GetValue(originalValues);
                var currentValue = property.GetValue(currentValues);

                if ((originalValue!= null && currentValue != null) && !originalValue.Equals(currentValue))
                {
                    Console.WriteLine($"Изменено свойство {property.Name}: {originalValue} -> {currentValue}");
                }
            }
        }

        // Вызываем базовый метод для сохранения изменений
        var result = base.SaveChanges();

        // Выводим сообщение об успешном сохранении
        Console.WriteLine("Изменения сохранены успешно.");

        return result;
    }

    public DbSet<Author> Authors { get; set; }
    public DbSet<Book> Books { get; set; }
}