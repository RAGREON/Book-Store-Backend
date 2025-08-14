using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Store.Api.Models;

public class Genre 
{
  [Key]
  public int Id { get; set; }

  [Required]
  [StringLength(100)]
  public required string Name { get; set; }

  public ICollection<Book> Books { get; set; } = new List<Book>();
}