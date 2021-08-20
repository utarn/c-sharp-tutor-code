using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Mvcday1.Data;

namespace Mvcday1.Applications.Book.Queries.GetCategoryQuery
{
    public class GetCategoryQuery : IRequest<List<CategoryViewModel>>
    {
        public class GetCategoryQueryHandler : IRequestHandler<GetCategoryQuery, List<CategoryViewModel>>
        {
            private readonly ApplicationDbContext _context;

            public GetCategoryQueryHandler(ApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<List<CategoryViewModel>> Handle(GetCategoryQuery request, CancellationToken cancellationToken)
            {
                return await _context.Categories
                .Select(c => new CategoryViewModel() {
                    Id = c.Id,
                    Name = c.Name
                })
                .ToListAsync(cancellationToken);
            }
        }
    }
}
