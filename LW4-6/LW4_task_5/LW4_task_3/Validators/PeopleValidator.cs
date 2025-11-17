using FluentValidation;
using LW4_task_3.Models.Request;

namespace LW4_task_3.Validators
{
    public class PeopleValidator: AbstractValidator<PeopleRequest>
    {
        public PeopleValidator() 
        {

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Поле не може бути порожнім")
                .Matches("^[A-Z][a-z]{5,10}[0-9]{0,5}$").WithMessage("Поле має відповідати стандарту (Перша велика англійська, " +
                "далів від 5 до 10 малих англійських літер та за бажаням до 5 цифр)");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Поле не може бути порожнім")
                .EmailAddress().WithMessage("Електронна адрсе має відповідати стандарту");
        }

    }
}
