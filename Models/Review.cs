using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace Store.Api.Models;

public class Review 
{
  [Key]
  public int Id { get; set; }

  [Required]
  public short Rating { get; set; }

  [StringLength(1000)]
  public string? Description { get; set; }

  [Required]
  public int BookId { get; set; }

  [ForeignKey("BookId")]
  public Book? Book { get; set; }
}