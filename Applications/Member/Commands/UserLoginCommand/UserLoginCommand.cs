using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Mvcday1.Data;

namespace Mvcday1.Applications.Member.Commands.UserLoginCommand
{
    public class UserLoginCommandValidator : AbstractValidator<UserLoginCommand>
    {
        public UserLoginCommandValidator(SignInManager<ApplicationUser> signInManager)
        {
            RuleFor(c => c.Username)
            .NotEmpty()
            .WithMessage("รหัสผู้ใช้ห้ามว่าง");

            RuleFor(c => c.Password)
            .NotEmpty()
            .WithMessage("รหัสผ่านห้ามว่าง");

            RuleFor(c => c)
            .CustomAsync(async (command, validatorContext, cancellationToken) =>
            {
                if (command.Username != null && command.Password != null)
                {
                    var result = await signInManager.PasswordSignInAsync(command.Username, command.Password, false, false);
                    if (!result.Succeeded)
                    {
                        validatorContext.AddFailure(new ValidationFailure("Password", "รหัสผ่านไม่ถูกต้อง"));
                    }
                }
            });
        }
    }

    public class UserLoginCommand : IRequest<RedirectResultModel>
    {
        public string? Username { get; set; }
        public string? Password { get; set; }

        public class UserLoginCommandHandler : IRequestHandler<UserLoginCommand, RedirectResultModel>
        {
            private readonly UserManager<ApplicationUser> _userManager;

            public UserLoginCommandHandler(UserManager<ApplicationUser> userManager)
            {
                _userManager = userManager;
            }

            public async Task<RedirectResultModel> Handle(UserLoginCommand request, CancellationToken cancellationToken)
            {
                var appUser = await _userManager.FindByNameAsync(request.Username);
                var isAdmin = await _userManager.IsInRoleAsync(appUser, "Administrator");
                if (isAdmin)
                {
                    return new RedirectResultModel()
                    {
                        ActionName = "Index",
                        ControllerName = "Admin"
                    };
                }
                var isUser = await _userManager.IsInRoleAsync(appUser, "User");
                if (isUser)
                {
                    return new RedirectResultModel()
                    {
                        ActionName = "Index",
                        ControllerName = "Book"
                    };
                }
                else
                {
                     return new RedirectResultModel()
                    {
                        ActionName = "Index",
                        ControllerName = "Home"
                    };
                }
            }
        }
    }

    public class RedirectResultModel
    {
        public string? PathName { get; set; }
        public string? ControllerName { get; set; }
        public string? ActionName { get; set; }
    }
}