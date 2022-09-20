using Entities.DbEntities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class CreditCardContext : IdentityDbContext
    {

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("server=(localdb)\\MSSQLLocalDB;database=TSELatestProject;integrated security=true;");
        }
        public DbSet<BankConfiguration>? BankConfigurations { get; set; }
        public DbSet<CreditCardConfiguration>? CreditCardConfigurations { get; set; }
        public DbSet<TerminalConfiguration>? TerminalConfigurations  { get; set; }
        public DbSet<Customer>? Customers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //modelBuilder.Entity<BankConfiguration>().Property(x => x.Id).ValueGeneratedOnAdd();
            //modelBuilder.Entity<CreditCardConfiguration>().Property(x => x.Id).ValueGeneratedOnAdd();
            //modelBuilder.Entity<TerminalConfiguration>().Property(x => x.Id).ValueGeneratedOnAdd();
        }
    }
}
