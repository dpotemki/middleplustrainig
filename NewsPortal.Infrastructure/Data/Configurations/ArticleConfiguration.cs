using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NewsPortal.Domain.Entities;

namespace NewsPortal.Infrastructure.Data.Configurations;

public class ArticleConfiguration : IEntityTypeConfiguration<ArticleEntity>
{
    public void Configure(EntityTypeBuilder<ArticleEntity> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasOne(a=>a.Author);
        builder.HasOne(a=>a.Category);
        builder.HasMany(a=>a.Tags).WithOne();
        
        builder.Property(x=>x.Title).IsRequired().HasMaxLength(50);
        builder.Property(x=>x.Content).IsRequired();
        builder.Property(x=>x.PublishedAt).IsRequired().HasColumnType("datetime");
        builder.Property(x=>x.CreatedAt).IsRequired().HasColumnType("datetime");
        builder.Property(x=>x.UpdatedAt).IsRequired().HasColumnType("datetime");
        builder.Property(x=>x.IsPublished).IsRequired().HasDefaultValue(false);
        builder.Property(x=>x.AuthorId).IsRequired();
        builder.Property(x=>x.CategoryId).IsRequired();
        
    }
}