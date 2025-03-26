using Microsoft.EntityFrameworkCore;
using Product.API.Models;

namespace Product.API.Context
{
    public class ProductContext : DbContext
    {
        public ProductContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<ProductModel> Products { get { return Set<ProductModel>(); } }
    }
}
