using DataInterface;
using Microsoft.EntityFrameworkCore;
using System;

namespace DataAccess
{
    public class LibraryContext : DbContext 
    {



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=LAPTOP-F5T9EA2F;Database=Library;Trusted_connection=true");
        }
        public DbSet<Path> Paths { get; set; }
        public DbSet<Shelf> Shelves { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<MinorCustomer> MinorCustomers { get; set; }
        public DbSet<Loan> Loans { get; set; }
       // public DbSet<WornOutBook> WornOutBooks { get; set; }
       // public DbSet<MinorCustomerLoan> MinorCustomerLoans { get; set; }
       



    }
}
