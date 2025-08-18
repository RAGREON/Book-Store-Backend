using Microsoft.EntityFrameworkCore;
using Store.Api.Models;

namespace Store.Data;

public class AppDbContext : DbContext
{
  public DbSet<Book> Books => Set<Book>();
  public DbSet<Genre> Genres => Set<Genre>();
  public DbSet<Review> Reviews => Set<Review>();

  public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.Entity<Book>()
      .HasMany(b => b.Genres)
      .WithMany(g => g.Books)
      .UsingEntity(j => j.ToTable("BookGenres"));

    modelBuilder.Entity<Genre>()
      .HasIndex(g => g.Name)
      .IsUnique();

    modelBuilder.Entity<Review>()
      .HasOne(r => r.Book)
      .WithMany(b => b.Reviews)
      .HasForeignKey(r => r.BookId)
      .OnDelete(DeleteBehavior.Cascade);

    modelBuilder.Entity<Review>()
      .Property(r => r.CreatedAt)
      .HasDefaultValueSql("NOW()")
      .Metadata.SetAfterSaveBehavior(Microsoft.EntityFrameworkCore.Metadata.PropertySaveBehavior.Ignore);

    base.OnModelCreating(modelBuilder);
  }
}