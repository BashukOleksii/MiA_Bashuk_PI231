using FluentValidation;
using LW4_task_3.Models.Request;

namespace LW4_task_3.Validators
{
    public class UserValidator: AbstractValidator<UserRegistration>
    {
        public UserValidator()
        {
            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage("Обов'язкове поле для заповнення");

            RuleFor(x => x.Email)
                .EmailAddress().WithMessage("Пошта має відповідати стандарту")
                .NotEmpty().WithMessage("Поле не може бути порожнім");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Поле паролю не можу бути порожнім")
                .MinimumLength(8).WithMessage("Пароль має бути принаймні 8 символів")
                .Matches(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d).+$")
                .WithMessage("Пароль має мати хоча б одну велику та малу англійську літера та одну цифру");
        }
    }
}
