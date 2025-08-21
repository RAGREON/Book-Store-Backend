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

    var editedAtProperty = modelBuilder.Entity<Review>()
      .Property(r => r.EditedAt)
      .HasDefaultValueSql("NULL")
      .Metadata;

    modelBuilder.Entity<BookImage>()
      .HasOne(bi => bi.Book)
      .WithMany(b => b.Images)
      .HasForeignKey(bi => bi.BookId)
      .OnDelete(DeleteBehavior.Cascade);

    modelBuilder.Entity<BookImage>()
      .HasIndex(bi => new { bi.BookId, bi.Type })
      .IsUnique()
      .HasFilter(@"""Type"" = 0");

    base.OnModelCreating(modelBuilder);
  }

  public override int SaveChanges()
  {
    UpdateEditedTimestamps();
    return base.SaveChanges();
  }

  public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
  {
    UpdateEditedTimestamps();
    return await base.SaveChangesAsync(cancellationToken);
  }

  private void UpdateEditedTimestamps()
  {
    var modifiedReviews = ChangeTracker
      .Entries<Review>()
      .Where(e => e.State == EntityState.Modified);

    foreach (var entry in modifiedReviews)
    {
      entry.Entity.EditedAt = DateTime.UtcNow; 
    }
  }
}