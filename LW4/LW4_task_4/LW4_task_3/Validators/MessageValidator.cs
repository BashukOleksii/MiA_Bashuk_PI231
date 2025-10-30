using FluentValidation;
using LW4_task_3.Models;

namespace LW4_task_3.Validators
{
    public class MessageValidator: AbstractValidator<MessageItem>
    {
        public MessageValidator()
        {
            RuleFor(x => x.OwnerId)
                .NotEmpty().WithMessage("Поле власника підписки не може бути порожнім");

            RuleFor(x => x.SubId)
                .NotEmpty().WithMessage("Поле підписки не може бути порожнім");

            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Поле не може бути порожнім")
                .Length(5, 100).WithMessage("Поле повідомлення має бути в межах від 5 до 100 символів");
        }
    }
}
