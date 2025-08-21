using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using Store.Api.Models;

namespace Store.Api.Models;

public enum ImageType
{
  Cover, Gallery
};

public class BookImage
{
  [Key]
  public int Id { get; set; }

  [Required]
  public required int BookId { get; set; }

  [ForeignKey(nameof(BookId))]
  public Book? Book { get; set; }

  [Required]
  public required ImageType Type { get; set; }

  [Required]
  [StringLength(500)]
  public string Url { get; set; } = string.Empty;
}