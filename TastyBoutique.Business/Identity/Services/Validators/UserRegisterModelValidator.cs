using FluentValidation;
using TastyBoutique.Business.Identity.Models;

namespace TastyBoutique.Business.Identity.Services.Validators
{
    public class UserRegisterModelValidator : AbstractValidator<UserRegisterModel>
    {
        public UserRegisterModelValidator()
        {
            RuleFor(x => x.Email)
                .NotNull()
                .NotEmpty()
                .MaximumLength(50)
                .EmailAddress();


            RuleFor(x => x.Password)
                .Matches("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d).{8,15}$")
                .NotEmpty()
                .NotNull();
                
                
            RuleFor(x => x.Username)
                .NotEmpty()
                .MaximumLength(50)
                .NotNull();

            RuleFor(x => x.Age)
                .GreaterThan(14);

        }
    }
}
