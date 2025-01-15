using System.ComponentModel.DataAnnotations;

namespace NewsPortal.Domain.Entities;

public class ArticleEntity
{
    public long Id { get; set; }
    public string Title { get; set; } =  string.Empty; 
    public string Content { get; set; } = string.Empty; 
    public DateTime PublishedAt { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public bool IsPublished { get; set; }
    
    public AuthorEntity Author { get; set; }
    public long AuthorId { get; set; }
    public CategoryEntity Category { get; set; }
    public long CategoryId { get; set; } 
    
    public ICollection<TagEntity> Tags { get; set; }
}