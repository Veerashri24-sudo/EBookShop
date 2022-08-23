using BookApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BookApp
{
    public class DatabaseContext:DbContext
    {
        public DatabaseContext()
        {

        }
        public DatabaseContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Book> Book { get; set; }
        public DbSet<BookApp.Models.UserAccounts>? UserAccounts { get; set; }
      

        public DbSet<BookApp.Models.CartItems>? CartItems { get; set; }

        public DbSet<BookApp.Models.Billing>? Billing { get; set; }

        public DbSet<BookApp.Models.Transaction>? Transaction { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=DESKTOP-J6USA91\\SQLEXPRESS;Initial Catalog=BookShopApp;Integrated Security=true");
        }
    }
}
