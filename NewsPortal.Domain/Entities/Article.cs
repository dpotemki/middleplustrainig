using System.ComponentModel.DataAnnotations;

namespace NewsPortal.Domain.Entities;

public class Article
{
    public long Id { get; set; }
    public string Title { get; set; } 
    public string Content { get; set; } 
    public DateTime PublishedAt { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public bool IsPublished { get; set; }
    
    public Author Author { get; set; }
    public long AuthorId { get; set; }
    public Category Category { get; set; }
    public long CategoryId { get; set; }
    public ICollection<ArticleTag> ArticleTags { get; set; }
}