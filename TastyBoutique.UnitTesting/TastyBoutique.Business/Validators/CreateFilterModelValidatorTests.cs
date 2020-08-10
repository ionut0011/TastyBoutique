using FluentAssertions;
using TastyBoutique.Business.Validators;
using TastyBoutique.UnitTesting.Shared.Factories;
using Xunit;

namespace TastyBoutique.UnitTesting.TastyBoutique.Business.Validators
{
    public class CreateFilterModelValidatorTests
    {

        [Fact]
        public void GivenCreateFilterModel_WhenHavingValidName_ThenResultShouldBeValid()
        {
            var model = CreateFilterModelFactory.Default();

            var validator = new CreateFilterModelValidator();

            var result = validator.Validate(model);

            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public void GivenCreateFilterModel_WhenHavingNameNull_ThenResultShouldBeInvalid()
        {
            var model = CreateFilterModelFactory.WithNameNull();

            var validator= new CreateFilterModelValidator();

            var result = validator.Validate(model);

            result.IsValid.Should().BeFalse();
        }

        [Fact]
        public void GivenCreateFilterModel_WhenHavingNameEmpty_ThenResultShouldBeInvalid()
        {
            var model = CreateFilterModelFactory.WithNameEmpty();

            var validator = new CreateFilterModelValidator();

            var result = validator.Validate(model);

            result.IsValid.Should().BeFalse();
        }

        [Fact]
        public void GivenCreateFilterModel_WhenHavingNameWithMoreThan50Characters_ThenResultShouldBeInvalid()
        {
            var model = CreateFilterModelFactory.WithNameHavingMoreThan50Characters();

            var validator = new CreateFilterModelValidator();

            var result = validator.Validate(model);

            result.IsValid.Should().BeFalse();
        }
    }
}
