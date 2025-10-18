using LW_4_2.Models;
using LW_4_2.Data;
using FluentValidation;

namespace LW_4_2.Validator
{
    public class SubscriptionValidator: AbstractValidator<SubscriptionItems>
    {
        public SubscriptionValidator()
        {
            RuleFor(x => x.OwnerId)
                .NotEmpty().WithMessage("Поле 'OwnerId не може бути порожнім'")
                .Must(ownerID => SubData.subsripptionItems.Any(s => s.OwnerId == ownerID))
                .WithMessage("Не знайдено користувача за вказаним 'OwnerId'");

            RuleFor(x => x.Service)
                .NotEmpty().WithMessage("Поле 'Service' не може бути порожнім")
                .Length(3, 50).WithMessage("Поле 'Service' має бути в межах від 3 до 50 символів");

            RuleFor(x => x.Status)
                .NotEmpty().WithMessage("Поле 'Status' не можу бути порожнім")
                .IsInEnum().WithMessage("Поле 'Status' має бути вибране в заданих межах");    
        }
    }
}
