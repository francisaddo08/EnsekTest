using System;
using FluentValidation;
using System.Linq;
using Domain.Models;
namespace BackEndApiEF.validators
{
    public class AccountValidator : AbstractValidator<Domain.Models.AccountModel>
    {
        public AccountValidator()
        {
            RuleFor(a => a.AccountId)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                .NotEmpty();

            RuleFor(a => a.FirstName)
                 .Cascade(CascadeMode.Stop)
                .Must(IsValidName).WithMessage("Your First Name cannot have Numbers")
                .Length(2, 250)
                .NotEmpty()
                .WithMessage("First Name can not be Empty");

            RuleFor(a => a.LastName)
                 .Cascade(CascadeMode.Stop)
                 .Must(IsValidName).WithMessage("Your Last Name cannot have Numbers")
                .Length(2, 250)
                .NotEmpty()
                .WithMessage("Last Name can not be Empty");


        }
        private bool IsValidName(string name)
        {
            return name.All(Char.IsLetter);
        }
    }
}
