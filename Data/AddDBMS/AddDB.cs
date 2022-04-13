using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using EReaderNow.Models;
using EReaderNow.Data.Domain;


namespace EReaderNow.Data.AddDBMS
{
    public class AddDB : IdentityDbContext<IdentityUser>
    {
        public AddDB(DbContextOptions<AddDB> options) : base(options) { }
        public DbSet<BooksItem> BooksItem { get; set; }
        public DbSet<TextField> TextField { get; set; }
        public DbSet<Genre> genres { get; set; }
        public DbSet<ListGenre> listGenre { get; set; }
        public DbSet<AllDescriptionBooks> AllDescriptionBooks { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {   
            Console.WriteLine("AddDB create");
          
            base.OnModelCreating(builder);
            
            builder.Entity<IdentityRole>().HasData(new IdentityRole
            { 
                Id = "2df552e1-149c-4a91-a5b8-7b368662daa1",
                Name = "admin",
                NormalizedName = "ADMIN"
            });
            builder.Entity<IdentityUser>().HasData(new IdentityUser
            {
                Id = "cf7f86b3-6f3b-46f7-b519-b091003a5f56",
                UserName = "admin",
                NormalizedUserName = "ADMIN",
                Email = "s.doctor608@gmail.com",
                EmailConfirmed = true,
                PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(null, "superpassword")
            });
            builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = "2df552e1-149c-4a91-a5b8-7b368662daa1",
                UserId = "cf7f86b3-6f3b-46f7-b519-b091003a5f56"
            });
            builder.Entity<TextField>().HasData(new TextField
            {
                ID = new Guid ("2880b3ea-9e5e-4493-9b62-37d8c9a225af"),
                codeWord = "PageIndex",
                title = "Главная"
            });
            builder.Entity<TextField>().HasData(new TextField
            {
                ID = new Guid ("8008dbbc-ba1e-4247-85af-79d40c75c3aa"),
                codeWord = "PageBooks",
                title = "Все книги"
            });
            builder.Entity<TextField>().HasData(new TextField
            {
                ID = new Guid ("a9d6e8f4-1592-437a-8162-82e46e40ff27"),
                codeWord = "PageGenre",
                title = "Жанры"
            });
        }
    }
}
