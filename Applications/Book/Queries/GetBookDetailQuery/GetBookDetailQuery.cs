using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Mvcday1.Data;

namespace Mvcday1.Applications.Book.Queries.GetBookDetailQuery
{


    public class GetBookDetailQuery : IRequest<BookDetailViewModel>
    {
        public int BookId { get; set; }
        public class GetBookDetailQueryHandler : IRequestHandler<GetBookDetailQuery, BookDetailViewModel>
        {
            private readonly ApplicationDbContext _context;
            public GetBookDetailQueryHandler(ApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<BookDetailViewModel> Handle(GetBookDetailQuery request, CancellationToken cancellationToken)
            {
                return await _context.Books
                .Include(b => b.Category)
                .Where(b => b.Id == request.BookId)
                .Select(b => new BookDetailViewModel()
                {
                    Id = b.Id,
                    Title = b.Title,
                    Price = b.Price,
                    Category = b.Category.Name
                })
                .FirstOrDefaultAsync(cancellationToken);
            }
        }
    }
}

// var command ="SELECT COUNT(*) FROM account WHERE user = '%USER%' AND pass = '%PASS%'"
// command = command.replace("%USER%",userInput.Text);
// command = command.replace("%PASS%",passInput.Text);
// user = utarn , pass = 1234
// SELECT COUNT(*) FROM account WHERE user = 'utarn' AND pass = '1234'
// user = admin , pass = "1234' OR 1 OR pass = '"
// SELECT COUNT(*) FROM account WHERE user = 'admin' AND pass = '1234\' OR 1 OR pass = \''
