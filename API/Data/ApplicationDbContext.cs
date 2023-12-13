using API.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
  public class ApplicationDbContext : DbContext
  {
    public ApplicationDbContext(DbContextOptions options) : base(options) { }

    public virtual DbSet<BlogPost> BlogPosts { get; set; }
    public virtual DbSet<Category> Categories { get; set; }
  }
}