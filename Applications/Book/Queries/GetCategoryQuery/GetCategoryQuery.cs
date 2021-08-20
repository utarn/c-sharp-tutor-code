using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Mvcday1.Data;

namespace Mvcday1.Applications.Book.Queries.GetCategoryQuery
{
    public class GetCategoryQuery : IRequest<List<Category>>
    {
        public class GetCategoryQueryHandler : IRequestHandler<GetCategoryQuery, List<Category>>
        {
            private readonly ApplicationDbContext _context;

            public GetCategoryQueryHandler(ApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<List<Category>> Handle(GetCategoryQuery request, CancellationToken cancellationToken)
            {
                return await _context.Categories.ToListAsync(cancellationToken);
            }
        }
    }
}