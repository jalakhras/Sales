using SalesApp.Domain.Entity;
using System.Data.Entity;

namespace SalesApp.Data
{
    public class OrderSystemContext : DbContext
    {
        public OrderSystemContext() : base("name=OrderSystemContext")
        {
        }

        public virtual DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}
