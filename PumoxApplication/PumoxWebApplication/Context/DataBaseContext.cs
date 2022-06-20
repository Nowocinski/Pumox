using Microsoft.EntityFrameworkCore;
using PumoxWebApplication.Models;

namespace PumoxWebApplication.Context
{
    public class DataBaseContext : DbContext
    {
        public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Company>(column =>
            {
                column.HasKey(key => key.Id);
            });

            modelBuilder.Entity<Employee>(column =>
            {
                column.HasKey(key => key.Id);
            });

            modelBuilder.Entity<Company>()
                        .HasMany(column => column.Employees)
                        .WithOne()
                        .HasForeignKey(column => column.Company_Id)
                        .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Employee>()
                        .HasOne(column => column.Company)
                        .WithMany(column => column.Employees)
                        .HasForeignKey(x => x.Company_Id)
                        .OnDelete(DeleteBehavior.Cascade);
        }

        public DbSet<Company> Companies { get; set; }
        public DbSet<Employee> Employees { get; set; }
    }
}
