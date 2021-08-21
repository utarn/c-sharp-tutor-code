using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Mvcday1.Data;

namespace Mvcday1.Applications.Member.Commands.UserRegisterCommand
{

    public class UserRegisterCommand : IRequest<bool>
    {
        public string? Email { get; set; }
        public string? Username { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Password { get; set; }
        public string? ConfirmPassword { get; set; }

        public class UserRegisterCommandHandler : IRequestHandler<UserRegisterCommand, bool>
        {
            private readonly UserManager<ApplicationUser> _userManager;
            private readonly IEmailService _emailService;
            public UserRegisterCommandHandler(UserManager<ApplicationUser> userManager, IEmailService emailService)
            {
                _userManager = userManager;
                _emailService = emailService;
            }

            public async Task<bool> Handle(UserRegisterCommand request, CancellationToken cancellationToken)
            {
                var appUser = new ApplicationUser
                {
                    UserName = request.Username,
                    Email = request.Email,
                    FirstName = request.FirstName ?? "",
                    LastName = request.LastName ?? "",
                    EmailConfirmed = true
                };
                var result = await _userManager.CreateAsync(appUser, request.Password);
                if (result.Succeeded)
                {                    
                    await _userManager.AddToRoleAsync(appUser, "User");
                    await _emailService.SendRegisterEmail($"{appUser.FirstName} {appUser.LastName}", appUser.Email, appUser.PhoneNumber);                    
                    return true;
                }
                else
                {
                    throw new ValidationException("ไม่สามารถสร้างบัญชีนี้ได้ เนื่องรหัสผ่านไม่ผ่านนโยบายรหัสผ่าน");
                }
            }
        }
    }
}