using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Mvcday1.Data;

namespace Mvcday1.Applications.Book.Queries.GetBookDetailQuery
{
    public class GetBookDetailQueryValidator : AbstractValidator<GetBookDetailQuery>
    {
        private readonly ApplicationDbContext _context;
        public GetBookDetailQueryValidator(ApplicationDbContext context)
        {
            _context = context;
            // RuleFor(c => c.BookId)
            //     .MustAsync(async (bookId, cancellationToken) =>
            //     {
            //         return await context.Books.AnyAsync(b => b.Id == bookId, cancellationToken);
            //     })
            //     .WithMessage("ไม่มีรหัสหนังสือเล่มนี้");

            RuleFor(c => c.BookId)
            .MustAsync(mustExist)
            .WithMessage("ไม่มีรหัสหนังสือเล่มนี้");
        }

        private async Task<bool> mustExist(int bookId, CancellationToken cancellationToken)
        {
            return await _context.Books.AnyAsync(b => b.Id == bookId, cancellationToken);
        }

    }
}