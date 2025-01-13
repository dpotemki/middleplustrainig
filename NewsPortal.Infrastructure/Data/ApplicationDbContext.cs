using System.Reflection;
using Microsoft.EntityFrameworkCore;
using NewsPortal.Domain.Entities;

namespace NewsPortal.Infrastructure.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<Article> Article => Set<Article>();
    public DbSet<ArticleTag> ArticleTag => Set<ArticleTag>();
    public DbSet<Author> Author => Set<Author>();
    public DbSet<Category> Category => Set<Category>();
    public DbSet<Tag> Tag => Set<Tag>();
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}