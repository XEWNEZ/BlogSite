using Microsoft.EntityFrameworkCore;

namespace MyBlog.Models{
    
    public class BlogContext:DbContext{
        public BlogContext(DbContextOptions<BlogContext> options) : base(options) { }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}