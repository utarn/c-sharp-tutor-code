using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Mvcday1.Data;

namespace Mvcday1.Applications.Book.Queries.GetBookEditQuery
{

    public class GetBookEditQuery : IRequest<BookEditViewModel>
    {
        public int BookId { get; set; }
        public class GetBookEditQueryHandler : IRequestHandler<GetBookEditQuery, BookEditViewModel>
        {
            private readonly ApplicationDbContext _context;

            public GetBookEditQueryHandler(ApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<BookEditViewModel> Handle(GetBookEditQuery request, CancellationToken cancellationToken)
            {
                return await _context.Books
                        .Where(b => b.Id == request.BookId)
                        .Select(b => new BookEditViewModel()
                        {
                            Id = b.Id,
                            Title = b.Title,
                            Price = b.Price,
                            CategoryId = b.CategoryId
                        })
                        .FirstOrDefaultAsync(cancellationToken);
            }
        }
    }
}