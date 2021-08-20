using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Mvcday1.Applications.Home.Queries.GetDataQuery
{
    public class GetDataQuery : IRequest<string>
    {
        public class GetDataQueryHandler : IRequestHandler<GetDataQuery, string>
        {
            public Task<string> Handle(GetDataQuery request, CancellationToken cancellationToken)
            {
                return Task.FromResult("Pong!");
            }
        }
    }
}