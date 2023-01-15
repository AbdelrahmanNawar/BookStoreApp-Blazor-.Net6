using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BookStoreApp.API.Data
{
    public partial class BookStoreDBContext : IdentityDbContext<ApiUser>
    {
        public BookStoreDBContext() { }

        public BookStoreDBContext(DbContextOptions<BookStoreDBContext> options)
            : base(options) { }

        public virtual DbSet<Author> Authors { get; set; } = null!;
        public virtual DbSet<Book> Books { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Author>(entity =>
            {
                entity.Property(e => e.Bio).HasMaxLength(Int32.MaxValue);

                entity.Property(e => e.FirstName).HasMaxLength(Int32.MaxValue);

                entity.Property(e => e.LastName).HasMaxLength(Int32.MaxValue);
            });

            modelBuilder.Entity<Book>(entity =>
            {
                entity.HasIndex(e => e.Isbn, "UQ__Books__447D36EA0F9209FF")
                    .IsUnique();

                entity.Property(e => e.ImageURL).HasMaxLength(Int32.MaxValue);
                entity.Property(e => e.OriginalImageName).HasMaxLength(Int32.MaxValue);

                entity.Property(e => e.Isbn)
                    .HasMaxLength(50)
                    .HasColumnName("ISBN");

                entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Summary).HasMaxLength(Int32.MaxValue);

                entity.Property(e => e.Title).HasMaxLength(Int32.MaxValue);

                entity.HasOne(d => d.Author)
                    .WithMany(p => p.Books)
                    .HasForeignKey(d => d.AuthorId)
                    .HasConstraintName("FK_Books_Authors");
            });

            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole
                {
                    Name = "User",
                    NormalizedName = "USER",
                    Id = "5e87387f-029c-46e4-84f9-f7cf2f497f6f"
                },
                new IdentityRole
                {
                    Name = "Administrator",
                    NormalizedName = "ADMINSTRATOR",
                    Id = "2e9ab3a5-9529-4c55-a1b7-a8403877ef39"
                });

            modelBuilder.Entity<ApiUser>().HasData(
                new ApiUser
                {
                    Id = "6d59e082-5b57-4a85-9ed8-a16bdd14da6a",
                    Email = "admin@bookstore.com",
                    NormalizedEmail = "ADMIN@BOOKSTORE.COM",
                    UserName = "admin@bookstore.com",
                    NormalizedUserName = "ADMIN@BOOKSTORE.COM",
                    FirstName = "System",
                    LastName = "Admin",
                    PasswordHash = new PasswordHasher<ApiUser>().HashPassword(null, "P@ssword1")
                },
                new ApiUser
                {
                    Id = "a7dcaa02-593a-4b26-98b7-568f84a4fc86",
                    Email = "user@bookstore.com",
                    NormalizedEmail = "USER@BOOKSTORE.COM",
                    UserName = "user@bookstore.com",
                    NormalizedUserName = "USER@BOOKSTORE.COM",
                    FirstName = "System",
                    LastName = "User ",
                    PasswordHash = new PasswordHasher<ApiUser>().HashPassword(null, "P@ssword1")
                });

            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                    //admin
                    RoleId = "2e9ab3a5-9529-4c55-a1b7-a8403877ef39",
                    UserId = "6d59e082-5b57-4a85-9ed8-a16bdd14da6a"
                },
                new IdentityUserRole<string>
                {
                    //user
                    RoleId = "5e87387f-029c-46e4-84f9-f7cf2f497f6f",
                    UserId = "a7dcaa02-593a-4b26-98b7-568f84a4fc86"
                });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
