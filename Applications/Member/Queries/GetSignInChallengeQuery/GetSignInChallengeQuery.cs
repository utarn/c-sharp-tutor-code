using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Mvcday1.Data;

namespace Mvcday1.Applications.Member.Queries.GetSignInChallengeQuery
{
    public class GetSignInChallengeQuery : IRequest<ChallengeResult>
    {
        public string Provider { get; set; } = "Google";
        public string ReturnUrl { get; set; } = "/Book";
        public class GetSignInChallengeQueryHandler : IRequestHandler<GetSignInChallengeQuery, ChallengeResult>
        {
            private readonly SignInManager<ApplicationUser> _signInManager;
            private readonly IHttpContextAccessor _httpContextAccessor;
            private readonly LinkGenerator _linkGenerator;
            public GetSignInChallengeQueryHandler(SignInManager<ApplicationUser> signInManager, IHttpContextAccessor httpContextAccessor, LinkGenerator linkGenerator)
            {
                _signInManager = signInManager;
                _httpContextAccessor = httpContextAccessor;
                _linkGenerator = linkGenerator;
            }
            public Task<ChallengeResult> Handle(GetSignInChallengeQuery request, CancellationToken cancellationToken)
            {
                // var redirectUrl = "/Member/SocialLoginCallback";
                var redirectUrl = _linkGenerator.GetUriByAction(_httpContextAccessor.HttpContext, "SocialLoginCallback", "Member");
                var properties = _signInManager.ConfigureExternalAuthenticationProperties(request.Provider, redirectUrl);
                return Task.FromResult(new ChallengeResult(request.Provider, properties));
            }
        }
    }
}