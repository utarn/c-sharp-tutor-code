using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Mvcday1.Data;

namespace Mvcday1.Applications.Book.Commands.CreateBookCommand
{
    public class CreateBookCommand : IRequest<bool>
    {
        [Display(Name = "ชื่อ")]
        public string Title { get; set; } = default!;
        [Display(Name = "ราคา")]
        public decimal Price { get; set; }
        [Display(Name = "ประเภท")]
        public int CategoryId { get; set; }

        public class CreateBookCommandHandler : IRequestHandler<CreateBookCommand, bool>
        {
            private readonly ApplicationDbContext _context;

            public CreateBookCommandHandler(ApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<bool> Handle(CreateBookCommand request, CancellationToken cancellationToken)
            {
                var newBook = new Data.Book() {
                    Title = request.Title,
                    Price = request.Price,
                    CategoryId = request.CategoryId
                };
                await _context.Books.AddAsync(newBook, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);
                return true;
            }
        }
    }
}