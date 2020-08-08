
using FluentAssertions;
using FluentValidation;
using System.Runtime.InteropServices;
using TastyBoutique.Business.Identity.Models;
using TastyBoutique.Business.Identity.Services.Validators;
using TastyBoutique.UnitTests.Shared.Factories;
using Xunit;

namespace TastyBoutique.UnitTests.TastyBoutique.Business.Identity.Services.Validators
{
   public class UserRegisterModelValidatorTests
    {
        [Fact]
        public void GivenUserRegister_WhenHavingANullEmail_ThenResultShouldBeInvalid()
        {
            var model = UserRegisterModelFactory.WithEmailNull();
            var validator = new UserRegisterModelValidator();


            var result = validator.Validate(model);


            result.IsValid.Should().BeFalse();
            result.Errors.Count.Should().Be(1);


        }

        [Fact]
        public void GivenUserRegister_WhenHavingAValidEmail_ThenResultShouldBeValid()
        {
            var model = UserRegisterModelFactory.Default();
            var validator = new UserRegisterModelValidator();

            var result = validator.Validate(model);

            result.IsValid.Should().BeTrue();
            result.Errors.Count.Should().Be(0);

        }

        [Fact]
        public void GivenUserRegister_WhenHavingPasswordNull_ThenResultShouldBeInvalid()
        {
            var model = UserRegisterModelFactory.WithPasswordNull();

            var validator = new UserRegisterModelValidator();

            var result = validator.Validate(model);

            result.IsValid.Should().BeFalse();
            result.Errors.Count.Should().Be(1);
        }

        [Fact]
        public void GivenUserRegister_WhenHavingAValidPassword_ThenResultShouldBeValid()
        {
            var model = UserRegisterModelFactory.Default();

            var validator = new UserRegisterModelValidator();

            var result = validator.Validate(model);

            result.IsValid.Should().BeTrue();
            result.Errors.Count.Should().Be(0);
        }

        [Fact]
        public void GivenUserRegister_WhenHavingAValidUsername_ThenResultShouldBeValid()
        {
            var model = UserRegisterModelFactory.Default();
            var validator=new UserRegisterModelValidator();

            var result = validator.Validate(model);

            result.IsValid.Should().BeTrue();
            result.Errors.Count.Should().Be(0);

        }
        

        [Fact]
        public void GivenUserRegister_WhenHavingAUsernameNull_ThenResultShouldBeInvalid()
        {
            var model = UserRegisterModelFactory.WithUsernameNull();
            var validator = new UserRegisterModelValidator();

            var result = validator.Validate(model);

            result.IsValid.Should().BeFalse();
            result.Errors.Count.Should().Be(1);
        }





    }
}
