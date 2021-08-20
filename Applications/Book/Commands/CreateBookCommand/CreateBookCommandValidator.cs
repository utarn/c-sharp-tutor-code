using FluentValidation;

namespace Mvcday1.Applications.Book.Commands.CreateBookCommand
{
    public class CreateBookCommandValidator : AbstractValidator<CreateBookCommand>
    {
        public CreateBookCommandValidator()
        {
            RuleFor(c => c.Title)
                .NotEmpty()
                .WithMessage("ชื่อหนังสือห้ามว่าง");

            RuleFor(c => c.Price)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                .WithMessage("ราคาห้ามว่าง")
                .Must(c => c > 0)
                .WithMessage("ราคาไม่ถูกต้อง");

            RuleFor(c => c.CategoryId)
                .NotNull()
                .WithMessage("ประเภทหนังสือห้ามว่าง");
        }
    }
}