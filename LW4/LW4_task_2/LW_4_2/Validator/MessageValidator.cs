using FluentValidation;
using LW_4_2.Models;
using LW_4_2.Data;

namespace LW_4_2.Validator
{
    public class MessageValidator: AbstractValidator<MessageItems>
    {
        public MessageValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Поле 'Title' не може бути порожнім")
                .Length(5, 100).WithMessage("Поле 'Title' має бути від трьох до ста символів");

            RuleFor(x => x.SubId)
                .NotNull().WithMessage("Поле 'SubId' не може бути порожнім")
                .Must(SubId => SubData.subsripptionItems.Any(s => s.Id == SubId))
                .WithMessage("Не знайдено 'SubId' із таким індексом");

            RuleFor(x => x.OwnerId)
                .NotEmpty().WithMessage("Поле 'OwnerId' не можу бу порожнім")
                .Must(ownerId => PeopleData.peopleItems.Any(u => u.Id == ownerId))
                .WithMessage("Немає людина із таким значенням 'Id'")
                .Must((mes, ownerId) =>
                {
                    var sub = SubData.subsripptionItems.FirstOrDefault(s => s.Id == mes.SubId);

                    if (sub is null)
                        return false;

                    return sub.OwnerId == ownerId;
                }).WithMessage("Відсутня підписка за вказаним 'SubId' або для неї не існує користувача");

            
        }
    }
}
