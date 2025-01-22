using EntityFrameworkBook.Config;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkBook.Repo;

public class RepoBook
{
    public static void ListAll()
    {
        using (var db = new AppDbContext())
        {
            var books = db.Books.ToList();
            if (!books.Any())
            {
                Console.WriteLine("no books");
                return;
            }
            foreach (var book in
                     db.Books.AsNoTracking()
                         .Include(book => book.Author))
            {
                var webUrl = book.Author.WebUrl == null
                    ? "- no web URL given -"
                    : book.Author.WebUrl;
                Console.WriteLine(
                    $"{book.Title} by {book.Author.Name}");
                Console.WriteLine(" " +
                                  "Published on " +
                                  $"{book.PublishedOn:dd-MMM-yyyy}" +
                                  $". {webUrl}");
            }
        }
    }
    
    public static void ChangeWebUrl()
    {
        Console.Write("New Quantum Networking WebUrl > ");
        var newWebUrl = Console.ReadLine();
        using (var db = new AppDbContext())
        {
            var singleBook = db.Books
                .Include(book => book.Author)
                .Single(book => book.Title == "Title new");
            singleBook.Author.WebUrl = newWebUrl;
            db.SaveChanges();
            Console.WriteLine("... SavedChanges called.");
        }
        ListAll();
    }
}

