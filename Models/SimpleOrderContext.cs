namespace SimpleOrder.Models
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;

    public class SimpleOrderContext : DbContext
    {
        public virtual DbSet<Order> Orders { get; set; }

        public SimpleOrderContext(DbContextOptions options)
        : base(options) 
        { }
    }
}
