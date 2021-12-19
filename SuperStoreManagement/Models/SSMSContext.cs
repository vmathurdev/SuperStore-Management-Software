using Microsoft.EntityFrameworkCore;
using SuperStoreManagement.Models;

namespace project.Models
{
    public class SSMSContext:DbContext
    {
        public SSMSContext(
            DbContextOptions<SSMSContext> options) : base(options){
        }
        public DbSet<User> user { get; set; }
        public DbSet<Product> product { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Cart> Cart { get; set; }
        public DbSet<Order> order { get; set; }


    }
}
