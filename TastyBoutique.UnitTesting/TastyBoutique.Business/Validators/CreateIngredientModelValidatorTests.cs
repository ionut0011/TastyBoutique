using FluentAssertions;
using TastyBoutique.Business.Validators;
using TastyBoutique.UnitTesting.Shared.Factories;
using Xunit;

namespace TastyBoutique.UnitTesting.TastyBoutique.Business.Validators
{
    public class CreateIngredientModelValidatorTests
    {

        [Fact]
        public void GivenCreateFilterModel_WhenHavingValidName_ThenResultShouldBeValid()
        {
            var model = CreateIngredientModelFactory.Default();

            var validator = new CreateIngredientModelValidator();

            var result = validator.Validate(model);

            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public void GivenCreateFilterModel_WhenHavingNameNull_ThenResultShouldBeInvalid()
        {
            var model = CreateIngredientModelFactory.WithNameNull();

            var validator = new CreateIngredientModelValidator();

            var result = validator.Validate(model);

            result.IsValid.Should().BeFalse();
        }

        [Fact]
        public void GivenCreateFilterModel_WhenHavingNameEmpty_ThenResultShouldBeInvalid()
        {
            var model = CreateIngredientModelFactory.WithNameEmpty();

            var validator = new CreateIngredientModelValidator();

            var result = validator.Validate(model);

            result.IsValid.Should().BeFalse();
        }

        [Fact]
        public void GivenCreateFilterModel_WhenHavingNameWithMoreThan50Characters_ThenResultShouldBeInvalid()
        {
            var model = CreateIngredientModelFactory.WithNameHavingMoreThan50Characters();

            var validator = new CreateIngredientModelValidator();

            var result = validator.Validate(model);

            result.IsValid.Should().BeFalse();
        }
    }
}
