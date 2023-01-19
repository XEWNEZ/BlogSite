
namespace MyBlog.Models
{
          public class Blog
          {
                    public int Id { get; set; }
                    public string? Name { get; set; }
                    public string? Subtitle { get; set; }
                    public string? Content { get; set; }
                    public string? ImagePath { get; set; }
                    public DateTime CreateTime { get; set; } = DateTime.Now;
                    public bool IsPublish { get; set; }
                    public Author? Author { get; set; }
                    public int AuthorId { get; set; }
                    public Category? Category { get; set; }
                    public int CategoryId { get; set; }
          }
}