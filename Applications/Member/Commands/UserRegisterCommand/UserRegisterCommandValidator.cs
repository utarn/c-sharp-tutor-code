using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Mvcday1.Data;

namespace Mvcday1.Applications.Member.Commands.UserRegisterCommand
{
    public class UserRegisterCommandValidator : AbstractValidator<UserRegisterCommand>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public UserRegisterCommandValidator(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            RuleFor(c => c.Email)
               .Cascade(CascadeMode.Stop)
               .NotEmpty()
               .WithMessage("อีเมลห้ามว่าง")
               .EmailAddress()
               .WithMessage("อีเมลไม่ถูกต้อง")
               .MustAsync(NoDuplicateEmail)
               .WithMessage("อีเมลซ้ำในระบบ");

            RuleFor(c => c.Username)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("รหัสผู้ใช้ห้ามว่าง")
                .MustAsync(NoDuplicateUsername)
                .WithMessage("รหัสผู้ใช้ซ้ำในระบบ");

            RuleFor(c => c.FirstName)
                .NotEmpty()
                .WithMessage("ชื่อห้ามว่าง");

            RuleFor(c => c.LastName)
                .NotEmpty()
                .WithMessage("นามสกุลห้ามว่าง");

            RuleFor(c => c.Password)
                .NotEmpty()
                .WithMessage("รหัสผ่านห้ามว่าง");

            RuleFor(c => c.ConfirmPassword)
                .Equal(c => c.Password)
                .When(c => !string.IsNullOrEmpty(c.Password))
                .WithMessage("รหัสผ่านยืนยันไม่ตรงกัน");
        }


        private async Task<bool> NoDuplicateEmail(string email, CancellationToken cancellationToken)
        {
            var exist = await _userManager.FindByEmailAsync(email);
            if (exist == null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private async Task<bool> NoDuplicateUsername(string username, CancellationToken cancellationToken)
        {
            var exist = await _userManager.FindByNameAsync(username);
            if (exist == null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}