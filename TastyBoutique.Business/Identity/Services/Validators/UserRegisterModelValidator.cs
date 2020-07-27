using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using TastyBoutique.Business.Identity.Models;

namespace TastyBoutique.Business.Identity.Services.Validators
{
    public class UserRegisterModelValidator : AbstractValidator<UserRegisterModel>
    {
        public UserRegisterModelValidator()
        {
            RuleFor(x => x.Email)
                .NotNull()
                .EmailAddress();
            RuleFor(x => x.Password)
                .NotNull();
            RuleFor(x => x.Username)
                .NotNull();
        
        }
    }
}
