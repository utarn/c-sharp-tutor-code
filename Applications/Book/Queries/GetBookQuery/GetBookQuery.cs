using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Mvcday1.Data;

namespace Mvcday1.Applications.Book.Queries.GetBookQuery
{
    public class GetBookQuery : IRequest<List<BookViewModel>>
    {
        public class GetBookQueryHandler : IRequestHandler<GetBookQuery, List<BookViewModel>>
        {
            private readonly ApplicationDbContext _context;
            private readonly ILogger<GetBookQuery> _logger;
            public GetBookQueryHandler(ApplicationDbContext context, ILogger<GetBookQuery> logger)
            {
                _context = context;
                _logger = logger;
            }
            public async Task<List<BookViewModel>> Handle(GetBookQuery request, CancellationToken cancellationToken)
            {
                return await _context.Books.Select(b => new BookViewModel() {
                    Id = b.Id,
                    Title = b.Title
                }).ToListAsync(cancellationToken);
            }
        }
    }

}