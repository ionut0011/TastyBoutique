using FluentAssertions;
using TastyBoutique.Business.Validators;
using TastyBoutique.UnitTesting.Shared.Factories;
using Xunit;

namespace TastyBoutique.UnitTesting.TastyBoutique.Business.Validators
{
   public  class TotalRecipeModelValidatorTests
    {
        #region AverageReview
        [Fact]
        public void ValideTotalRecipeModel_WhenHavingAverageReviewLowerThanZero_ThenResultIsInvalid()
        {
            var model = TotalRecipeModelFactory.WithAverageReviewLowerThanZero();
            var validator= new TotalRecipeModelValidator();

            var result = validator.Validate(model);

            result.IsValid.Should().BeFalse();
        }

        [Fact]
        public void ValideTotalRecipeModel_WhenHavingAverageReviewGreaterThanFive_ThenResultIsInvalid()
        {
            var model = TotalRecipeModelFactory.WithAverageReviewGreaterThan5();
            var validator = new TotalRecipeModelValidator();

            var result = validator.Validate(model);

            result.IsValid.Should().BeFalse();
        }

        [Fact]
        public void ValideTotalRecipeModel_WhenHavingValidAverageReview_ThenResultIsValid()
        {
            var model = TotalRecipeModelFactory.Default();
            var validator = new TotalRecipeModelValidator();

            var result = validator.Validate(model);

            result.IsValid.Should().BeTrue();
        }
        #endregion
        #region Name
        [Fact]
        public void GivenTotalRecipeModel_WhenHavingValidName_ThenResultShouldBeValid()
        {
            var model = TotalRecipeModelFactory.Default();
            var validator = new TotalRecipeModelValidator();

            var result = validator.Validate(model);

            result.IsValid.Should().BeTrue();
        }
        [Fact]
        public void GivenTotalRecipeModel_WhenHavingdNameEmpty_ThenResultShouldBeInvalid()
        {
            var model = TotalRecipeModelFactory.WithNameEmpty();
            var validator = new TotalRecipeModelValidator();

            var result = validator.Validate(model);

            result.IsValid.Should().BeFalse();
        }

        [Fact]
        public void GivenTotalRecipeModel_WhenHavingdNameNull_ThenResultShouldBeInvalid()
        {
            var model = TotalRecipeModelFactory.WithNameNull();
            var validator = new TotalRecipeModelValidator();

            var result = validator.Validate(model);

            result.IsValid.Should().BeFalse();
        }

        [Fact]
        public void GivenTotalRecipeModel_WhenHavingdNameGreaterThan50Characters_ThenResultShouldBeInvalid()
        {
            var model = TotalRecipeModelFactory.WithNameGreaterThan50Characters();
            var validator = new TotalRecipeModelValidator();

            var result = validator.Validate(model);

            result.IsValid.Should().BeFalse();
        }


        #endregion
        #region Description
        [Fact]
        public void GivenTotalRecipeModel_WhenHavingValidDescription_ThenResultShouldBeValid()
        {
            var model = TotalRecipeModelFactory.Default();
            var validator = new TotalRecipeModelValidator();

            var result = validator.Validate(model);

            result.IsValid.Should().BeTrue();
        }


        [Fact]
        public void GivenTotalRecipeModel_WhenHavingDescriptionWithMoreThan300Characters_ThenResultShouldBeInvalid()
        {
            var model = TotalRecipeModelFactory.WithDescriptionGreaterThan300Characters();
            var validator = new TotalRecipeModelValidator();

            var result = validator.Validate(model);

            result.IsValid.Should().BeFalse();
        }

        #endregion
        #region Type

        [Fact]
        public void GivenTotalRecipeModel_WhenHavingValidType_ThenResultShouldBeValid()
        {
            var model = TotalRecipeModelFactory.Default();
            var validator = new TotalRecipeModelValidator();

            var result = validator.Validate(model);

            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public void GivenTotalRecipeModel_WhenHavingTypeEmpty_ThenResultShouldBeInvalid()
        {
            var model = TotalRecipeModelFactory.WithTypeEmpty();
            var validator = new TotalRecipeModelValidator();

            var result = validator.Validate(model);

            result.IsValid.Should().BeFalse();
        }

        [Fact]
        public void GivenTotalRecipeModel_WhenHavingTypeGreaterThan50Characters_ThenResultShouldBeValid()
        {
            var model = TotalRecipeModelFactory.WithTypeGreaterThan50Characters();
            var validator = new TotalRecipeModelValidator();

            var result = validator.Validate(model);

            result.IsValid.Should().BeFalse();
        }
        #endregion
        #region RecipeIngredients

        [Fact]
        public void GivenTotalRecipeModel_WhenHavingValidRecipeIngrediens_ThenResultShouldBeValid()
        {
            var model = TotalRecipeModelFactory.Default();
            var validator = new TotalRecipeModelValidator();

            var result = validator.Validate(model);

            result.IsValid.Should().BeTrue();
        }


        [Fact]
        public void GivenTotalRecipeModel_WhenHavingNullRecipeIngrediens_ThenResultShouldBeValid()
        {
            var model = TotalRecipeModelFactory.WithRecipeIngredientsNull();
            var validator = new TotalRecipeModelValidator();

            var result = validator.Validate(model);

            result.IsValid.Should().BeFalse();
        }

        [Fact]
        public void GivenTotalRecipeModel_WhenHavingEmptyRecipeIngrediens_ThenResultShouldBeValid()
        {
            var model = TotalRecipeModelFactory.WithRecipeIngredientsEmpty();
            var validator = new TotalRecipeModelValidator();

            var result = validator.Validate(model);

            result.IsValid.Should().BeFalse();
        }

        #endregion
        #region RecipeFilters
        [Fact]
        public void GivenTotalRecipeModel_WhenHavingValidRecipeFilters_ThenResultShouldBeValid()
        {
            var model = TotalRecipeModelFactory.Default();
            var validator = new TotalRecipeModelValidator();

            var result = validator.Validate(model);

            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public void GivenTotalRecipeModel_WhenHavingEmptyRecipeFilters_ThenResultShouldBeInvalid()
        {
            var model = TotalRecipeModelFactory.WithRecipeFiltersEmpty();
            var validator = new TotalRecipeModelValidator();

            var result = validator.Validate(model);

            result.IsValid.Should().BeFalse();
        }
        #endregion
    }
}
