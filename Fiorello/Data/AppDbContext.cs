using Microsoft.EntityFrameworkCore;
using Fiorello.Models;

namespace Fiorello.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    

    public DbSet<Fiorello.Models.Slider> Sliders { get; set; }
    public DbSet<Fiorello.Models.SliderInfo> SliderInfos { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<Setting> Settings { get; set; }


    }
}
