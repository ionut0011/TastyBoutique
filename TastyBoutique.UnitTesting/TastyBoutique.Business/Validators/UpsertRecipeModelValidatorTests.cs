
using FluentAssertions;
using TastyBoutique.Business.Validators;
using TastyBoutique.UnitTesting.Shared.Factories;
using Xunit;

namespace TastyBoutique.UnitTesting.TastyBoutique.Business.Validators
{
   public  class UpsertRecipeModelValidatorTests
    {

        #region Name

        [Fact]
        public void GivenUpsertRecipeModel_WhenHavingValidName_ThenResultShouldBeValid()
        {
            var model = UpsertRecipeModelFactory.Default();
            var validator = new UpsertRecipeModelValidator();

            var result = validator.Validate(model);

            result.IsValid.Should().BeTrue();
        }
        [Fact]
        public void GivenUpsertRecipeModel_WhenHavingdNameEmpty_ThenResultShouldBeInvalid()
        {
            var model = UpsertRecipeModelFactory.WithNameEmpty();
            var validator = new UpsertRecipeModelValidator();

            var result = validator.Validate(model);

            result.IsValid.Should().BeFalse();
        }

        [Fact]
        public void GivenUpsertRecipeModel_WhenHavingdNameNull_ThenResultShouldBeInvalid()
        {
            var model = UpsertRecipeModelFactory.WithNameNull();
            var validator = new UpsertRecipeModelValidator();

            var result = validator.Validate(model);

            result.IsValid.Should().BeFalse();
        }

        [Fact]
        public void GivenUpsertRecipeModel_WhenHavingdNameGreaterThan50Characters_ThenResultShouldBeInvalid()
        {
            var model = UpsertRecipeModelFactory.WithNameGreaterThan50Characters();
            var validator = new UpsertRecipeModelValidator();

            var result = validator.Validate(model);

            result.IsValid.Should().BeFalse();
        }

        #endregion
        #region Description
        [Fact]
        public void GivenUpsertRecipeModel_WhenHavingValidDescription_ThenResultShouldBeValid()
        {
            var model = UpsertRecipeModelFactory.Default();
            var validator = new UpsertRecipeModelValidator();

            var result = validator.Validate(model);

            result.IsValid.Should().BeTrue();
        }


        [Fact]
        public void GivenUpsertRecipeModel_WhenHavingDescriptionWithMoreThan300Characters_ThenResultShouldBeInvalid()
        {
            var model = UpsertRecipeModelFactory.WithDescriptionGreaterThan300Characters();
            var validator = new UpsertRecipeModelValidator();

            var result = validator.Validate(model);

            result.IsValid.Should().BeFalse();
        }
        #endregion
        #region Type
        [Fact]
        public void GivenUpsertRecipeModel_WhenHavingValidType_ThenResultShouldBeValid()
        {
            var model = UpsertRecipeModelFactory.Default();
            var validator = new UpsertRecipeModelValidator();

            var result = validator.Validate(model);

            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public void GivenUpsertRecipeModel_WhenHavingTypeEmpty_ThenResultShouldBeInvalid()
        {
            var model = UpsertRecipeModelFactory.WithTypeEmpty();
            var validator = new UpsertRecipeModelValidator();

            var result = validator.Validate(model);

            result.IsValid.Should().BeFalse();
        }

        [Fact]
        public void GivenUpsertRecipeModel_WhenHavingTypeGreaterThan50Characters_ThenResultShouldBeInvalid()
        {
            var model = UpsertRecipeModelFactory.WithTypeGreaterThan50Characters();
            var validator = new UpsertRecipeModelValidator();

            var result = validator.Validate(model);

            result.IsValid.Should().BeFalse();
        }
        #endregion
        #region Filter
        [Fact]
        public void GivenUpsertRecipeModel_WhenHavingAValidFilter_ThenResultShouldBeValid()
        {
            var model = UpsertRecipeModelFactory.Default();
            var validator = new UpsertRecipeModelValidator();

            var result = validator.Validate(model);

            result.IsValid.Should().BeTrue();
        }
        [Fact]
        public void GivenUpsertRecipeModel_WhenHavingAnEmptyFilter_ThenResultShouldBeInvalid()
        {
            var model = UpsertRecipeModelFactory.WithFilterEmpty();
            var validator = new UpsertRecipeModelValidator();

            var result = validator.Validate(model);

            result.IsValid.Should().BeFalse();
        }
        #endregion
        #region IngredientsList
        [Fact]
        public void GivenUpsertRecipeModel_WhenHavingAValidIngredientsList_ThenResultShouldBeValid()
        {
            var model = UpsertRecipeModelFactory.Default();
            var validator = new UpsertRecipeModelValidator();

            var result = validator.Validate(model);

            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public void GivenUpsertRecipeModel_WhenHavingAnEmptyIngredientsList_ThenResultShouldBeInvalid()
        {
            var model = UpsertRecipeModelFactory.WithIngredientsListEmpty();
            var validator = new UpsertRecipeModelValidator();

            var result = validator.Validate(model);

            result.IsValid.Should().BeFalse();
        }

        [Fact]
        public void GivenUpsertRecipeModel_WhenHavingANullIngredientsList_ThenResultShouldBeInvalid()
        {
            var model = UpsertRecipeModelFactory.WithIngredientsListNull();
            var validator = new UpsertRecipeModelValidator();

            var result = validator.Validate(model);

            result.IsValid.Should().BeFalse();
        }

        #endregion
    }
}
