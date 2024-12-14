using ef_core_exam.Models;
using System.Data.Entity;


namespace ef_core_exam
{
    public partial class BookShop : DbContext
    {
        public BookShop()
            : base("name=BookShop")
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<Purchase> Purchases { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>()
                .HasOptional(b => b.Author)
                .WithMany(a => a.Books); 
            modelBuilder.Entity<Book>()
                .HasOptional(b => b.Publisher)
                .WithMany(p => p.Books); 
            modelBuilder.Entity<Purchase>() 
                .HasRequired(p => p.Book) 
                .WithMany() 
                .HasForeignKey(p => p.BookId); 
            modelBuilder.Entity<Purchase>() 
                .HasRequired(p => p.User) 
                .WithMany(u => u.Purchases) 
                .HasForeignKey(p => p.UserId);
        }
    }
}
