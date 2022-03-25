using FluentValidation;
using Domain.Models;
namespace BackEndApiEF.validators
{
    public class MeterReadValidator : AbstractValidator<MeterReadModel>
    {
        public MeterReadValidator()
        {
            RuleFor(m => m.AccountId)
                 .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("{PropertyName} can not be empty")
                .NotNull().WithMessage("{PropertyName} can not be Null");

            RuleFor(m => m.MeterReadingDateTime)
                 .Cascade(CascadeMode.Stop)
               .NotEmpty().WithMessage("{PropertyName} can not be empty")
               .NotNull().WithMessage("{PropertyName} can not be Null");

            RuleFor(m => m.MeterReadValue)
             .Cascade(CascadeMode.Stop);


        }
    }
}
