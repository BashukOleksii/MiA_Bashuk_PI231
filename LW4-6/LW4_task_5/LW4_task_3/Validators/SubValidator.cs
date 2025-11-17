using FluentValidation;
using LW4_task_3.Models.Request;



namespace LW4_task_3.Validators
{
    public class SubValidator:AbstractValidator<SubscriptionRequest>
    {
        public SubValidator()
        {
            RuleFor(x => x.OwnerId)
                .NotEmpty().WithMessage("Для підписки неохідно вказати власника")
                .Must(ValidElement.Id).WithMessage("Поле має ownerId відповідати стандарту");

            RuleFor(x => x.Service)
                .NotEmpty().WithMessage("Поле не може бути порожнім")
                .Length(3, 30).WithMessage("Поле має бути в межах [3;30]");

            RuleFor(x => x.Status)
                .NotEmpty().WithMessage("Поле обов'язкове для заповнення")
                .Must(ValidElement.ValidStatus).WithMessage("Значення має бути в вказаному переліку");

        }
    }
}
