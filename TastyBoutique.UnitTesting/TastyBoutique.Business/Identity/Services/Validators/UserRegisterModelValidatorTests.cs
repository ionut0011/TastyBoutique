using FluentAssertions;
using TastyBoutique.Business.Identity.Services.Validators;
using TastyBoutique.UnitTesting.Shared.Factories;
using Xunit;

namespace TastyBoutique.UnitTesting.TastyBoutique.Business.Identity.Services.Validators
{
    public class UserRegisterModelValidatorTests
    {
        #region Username
        [Fact]
        public void GivenUserRegisterModel_WhenHavingValidUsername_ThenResultShouldBeValid()
        {
            var model = UserRegisterModelFactory.Default();

            var validator = new UserRegisterModelValidator();

            var result = validator.Validate(model);

            result.IsValid.Should().BeTrue();
            
        }
        [Fact]
        public void GivenUserRegisterModel_WhenHavingUsernameWithMoreThan50Characters_ThenResultShouldBeInvalid()
        {
            var model = UserRegisterModelFactory.WithUsernameHavingMoreThan50Characters();

            var validator= new UserRegisterModelValidator();

            var result = validator.Validate(model);

            result.IsValid.Should().BeFalse();
        }

        [Fact]
        public void GivenUserRegisterModel_WhenHavingUsernameNull_ThenResultShouldBeInvalid()
        {
            var model = UserRegisterModelFactory.WithUsernameNull();

            var validator = new UserRegisterModelValidator();

            var result = validator.Validate(model);

            result.IsValid.Should().BeFalse();
        }

        [Fact]
        public void GivenUserRegisterModel_WhenHavingUsernameEmpty_ThenResultShouldBeInvalid()
        {
            var model =  UserRegisterModelFactory.WithUsernameEmpty();

            var validator = new UserRegisterModelValidator();

            var result = validator.Validate(model);

            result.IsValid.Should().BeFalse();
        }
        #endregion

        #region Age
        [Fact]
        public void GivenUserRegisterModel_WhenHavingValidAge_TheResultShouldBeValid()
        {
            var model = UserRegisterModelFactory.Default();

            var validator = new UserRegisterModelValidator();

            var result = validator.Validate(model);

            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public void GivenUserRegisterModel_WhenHavingInvalidAge_TheResultShouldBeInvalid()
        {
            var model = UserRegisterModelFactory.WithAgeBelow14();

            var validator = new UserRegisterModelValidator();

            var result = validator.Validate(model);

            result.IsValid.Should().BeFalse();
        }
        #endregion


        #region Password
        [Fact]
        public void GivenUserRegisterModel_WhenHavingValidPassword_TheResultShouldBeValid()
        {
            var model = UserRegisterModelFactory.Default();

            var validator = new UserRegisterModelValidator();

            var result = validator.Validate(model);

            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public void GivenUserRegisterModel_WhenHavingPasswordNull_TheResultShouldBeInvalid()
        {
            var model = UserRegisterModelFactory.WithPasswordNull();

            var validator = new UserRegisterModelValidator();

            var result = validator.Validate(model);

            result.IsValid.Should().BeFalse();
        }

        [Fact]
        public void GivenUserRegisterModel_WhenHavingPasswordEmpty_TheResultShouldBeInvalid()
        {
            var model = UserRegisterModelFactory.WithPasswordEmpty();

            var validator = new UserRegisterModelValidator();

            var result = validator.Validate(model);

            result.IsValid.Should().BeFalse();
        }
        [Fact]
        public void GivenUserRegisterModel_WhenHavingPasswordNotMatchingRegex_TheResultShouldBeInvalid()
        {
            var model = UserRegisterModelFactory.WithPasswordNotMatchingRegex();

            var validator = new UserRegisterModelValidator();

            var result = validator.Validate(model);

            result.IsValid.Should().BeFalse();
        }
        #endregion


        #region Email
        [Fact]
        public void GivenUserRegisterModel_WhenHavingValidEmail_ThenResultShouldBeValid()
        {
            var model = UserRegisterModelFactory.Default();

            var validator = new UserRegisterModelValidator();

            var result = validator.Validate(model);

            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public void GivenUserRegisterModel_WhenHavingEmailNull_ThenResultShouldBeInvalid()
        {
            var model = UserRegisterModelFactory.WithEmailNull();
            var validator = new UserRegisterModelValidator();

            var result = validator.Validate(model);

            result.IsValid.Should().BeFalse();
        }

        [Fact]
        public void GivenUserRegisterModel_WhenHavingEmailEmpty_ThenResultShouldBeInvalid()
        {
            var model = UserRegisterModelFactory.WithEmailEmpty();
            var validator = new UserRegisterModelValidator();

            var result = validator.Validate(model);

            result.IsValid.Should().BeFalse();
        }


        [Fact]
        public void GivenUserRegisterModel_WhenHavingAnInvalidEmailExtension_ThenResultShouldBeInvalid()
        {
            var model = UserRegisterModelFactory.WithInvalidEmailExtension();
            var validator = new UserRegisterModelValidator();

            var result = validator.Validate(model);

            result.IsValid.Should().BeFalse();
        }

        [Fact]
        public void GivenUserRegisterModel_WhenHavingEmailWithMoreThan50Characters_ThenResultShouldBeInvalid()
        {
            var model = UserRegisterModelFactory.WithEmailHavingMoreThan50Characters();
            var validator = new UserRegisterModelValidator();

            var result = validator.Validate(model);

            result.IsValid.Should().BeFalse();
        }

        #endregion

    }
}
