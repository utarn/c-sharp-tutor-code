using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Mvcday1.Data;

namespace Mvcday1.Applications.Member.Commands.UserLogoutCommand
{
    public class UserLogoutCommand : IRequest<bool>
    {
        public class UserLogoutCommandHandler : IRequestHandler<UserLogoutCommand, bool>
        {
            private readonly SignInManager<ApplicationUser> _signInManager;

            public UserLogoutCommandHandler(SignInManager<ApplicationUser> signInManager)
            {
                _signInManager = signInManager;
            }

            public async Task<bool> Handle(UserLogoutCommand request, CancellationToken cancellationToken)
            {
                await _signInManager.SignOutAsync();
                return true;
            }
        }
    }
}