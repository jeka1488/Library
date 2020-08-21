using Library.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Library.Data
{
    /// <summary>
    /// DataBase adapter with custom Identity
    /// </summary>
   public class ApplicationDbContext : IdentityDbContext<User, IdentityRole<int>, int,
       IdentityUserClaim<int>, IdentityUserRole<int>, IdentityUserLogin<int>, IdentityRoleClaim<int>,
       IdentityUserToken<int>>
    {

        /// <summary>
        /// Authors reposithory
        /// </summary>
        public DbSet<Models.Author> Authors { get; set; }
        
        /// <summary>
        /// Books repository
        /// </summary>
        public DbSet<Models.Book> Books { get; set; }

       /// <summary>
       /// constructor with auto generate database
       /// </summary>
       /// <param name="options"></param>
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            // /Database.EnsureCreated();
        }

       /// <summary>
       /// Database models schema
       /// </summary>
       /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.UseIdentityColumns();
            modelBuilder.HasAnnotation("Npgsql:ValueGenerationStrategy",
                NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            #region Identity models builders
            modelBuilder.Entity<User>().ToTable("User", "identity");
            modelBuilder.Entity<IdentityRole<int>>().ToTable("Role", "identity");
            modelBuilder.Entity<IdentityUserClaim<int>>().ToTable("UserClaim", "identity");
            modelBuilder.Entity<IdentityRoleClaim<int>>().ToTable("RoleClaim", "identity");
           
            modelBuilder.Entity<IdentityUserLogin<int>>().ToTable("UserLogin", "identity");
            modelBuilder.Entity<IdentityUserRole<int>>().ToTable("UserRole", "identity");
            modelBuilder.Entity<IdentityUserToken<int>>().ToTable("UserToken", "identity");
            #endregion
            
            //books model builder
            modelBuilder.Entity<Models.Book>(entity =>
            {
                entity.ToTable("book", "library");
                entity.Property(e => e.Id)
                    .HasColumnType("int")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.HasKey(e => e.Id).HasName("libraryBookPK");

                entity.Property(e => e.Title).HasColumnName("title");
                
                entity.Property(e => e.AuthorId).HasColumnName("authorId");
                entity.HasIndex(e => e.AuthorId).HasName("idxLibraryBookAuthorId");
                entity.HasOne(e => e.Author)
                    .WithMany(pt => pt.Books)
                    .HasForeignKey(e => e.AuthorId);
                
            });  
            
            // authors model builder
            modelBuilder.Entity<Models.Author>(entity =>
            {
                entity.ToTable("author", "library");
                entity.Property(e => e.Id)
                    .HasColumnType("int")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.HasKey(e => e.Id).HasName("libraryAuthorPK");

                entity.Property(e => e.Name).HasColumnName("name");
                entity.Property(e => e.Surname).HasColumnName("surname");
                entity.Property(e => e.Patronymic).HasColumnName("patronymic");
            });
        }

       /// <summary>
       /// Connecting configuration
       /// </summary>
       /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(Startup.Configuration.GetConnectionString("DefaultConnection"));
        }
    }

}