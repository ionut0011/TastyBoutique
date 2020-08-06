﻿using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using TastyBoutique.Business.Models.Recipe;

namespace TastyBoutique.Business.Validators
{
    public sealed class TotalRecipeModelValidator : AbstractValidator<TotalRecipeModel>
    {
        public TotalRecipeModelValidator()
        {
            RuleFor(model => model.Name)
                .MaximumLength(50)
                .NotEmpty()
                .NotNull();

            RuleFor(model => model.Description)
                .MaximumLength(300);

            RuleFor(model => model.Type)
                .MaximumLength(50)
                .NotEmpty();

            RuleFor(model => model.Ingredients)
                .NotNull()
                .NotEmpty();

            RuleFor(model => model.Filters)
                .NotEmpty();

            RuleFor(model => model.AverageReview)
                .GreaterThanOrEqualTo(0);
                
        }
    }
}
