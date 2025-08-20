using MediatR;
using Microsoft.EntityFrameworkCore;
using Store.Api.DTOs;
using Store.Commands;
using Store.Data;
using Store.Exceptions;
using Store.Api.Models;

namespace Commands.Handlers;

public class DeleteBookHandler : IRequestHandler<DeleteBookCommand, bool>
{
  private readonly AppDbContext _context;

  public DeleteBookHandler(AppDbContext context) 
  {
    _context = context;
  }

  public async Task<bool> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
  {
    var book = await _context.Books.FindAsync(request.Id);
    
    if (book != null)
    {
      _context.Books.Remove(book);
      await _context.SaveChangesAsync(cancellationToken);
      return true;
    }

    return false;
  }
}