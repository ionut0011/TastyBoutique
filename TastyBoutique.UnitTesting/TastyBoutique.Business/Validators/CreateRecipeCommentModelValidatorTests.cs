using FluentAssertions;
using TastyBoutique.Business.Validators;
using TastyBoutique.UnitTesting.Shared.Factories;
using Xunit;

namespace TastyBoutique.UnitTesting.TastyBoutique.Business.Validators
{
    public class CreateRecipeCommentModelValidatorTests
    {
        #region Comment
        [Fact]
        public void GivenCreateRecipeCommentModel_WhenHavingCommentWithLessThan5Characters_ThenResultIsInvalid()
        {
            var model = CreateRecipeCommentModelFactory.WithCommentLowerThan5Characters();
            var validator = new CreateRecipeCommentModelValidator();

            var result = validator.Validate(model);

            result.IsValid.Should().BeFalse();
        }

        [Fact]
        public void GivenCreateRecipeCommentModel_WhenHavingCommentWithMoreThan250Characters_ThenResultIsInvalid()
        {
            var model = CreateRecipeCommentModelFactory.WithCommentGreaterThan250Characters();
            var validator = new CreateRecipeCommentModelValidator();

            var result = validator.Validate(model);

            result.IsValid.Should().BeFalse();
        }
        [Fact]
        public void GivenCreateRecipeCommentModel_WhenHavingCommentNull_ThenResultIsInvalid()
        {
            var model = CreateRecipeCommentModelFactory.WithCommentNull();
            var validator = new CreateRecipeCommentModelValidator();

            var result = validator.Validate(model);

            result.IsValid.Should().BeFalse();
        }
        [Fact]
        public void GivenCreateRecipeCommentModel_WhenHavingCommentEmpty_ThenResultIsInvalid()
        {
            var model = CreateRecipeCommentModelFactory.WithCommentEmpty();
            var validator = new CreateRecipeCommentModelValidator();

            var result = validator.Validate(model);

            result.IsValid.Should().BeFalse();
        }

        [Fact]
        public void GivenCreateRecipeCommentModel_WhenHavingAValidComment_ThenResultIsValid()
        {
            var model = CreateRecipeCommentModelFactory.Default();
            var validator = new CreateRecipeCommentModelValidator();

            var result = validator.Validate(model);

            result.IsValid.Should().BeTrue();
        }
        #endregion

        #region Review

        [Fact]
        public void GivenCreateRecipecommentModel_WhenHavingNegativeReview_ThenResultIsInvalid()
        {
            var model = CreateRecipeCommentModelFactory.WithNegativeReview();
            var validator = new CreateRecipeCommentModelValidator();

            var result = validator.Validate(model);

            result.IsValid.Should().BeFalse();

        }
        [Fact]
        public void GivenCreateRecipecommentModel_WhenHavingAValidReview_ThenResultIsValid()
        {
            var model = CreateRecipeCommentModelFactory.Default();
            var validator = new CreateRecipeCommentModelValidator();

            var result = validator.Validate(model);

            result.IsValid.Should().BeTrue();

        }
        [Fact]
        public void GivenCreateRecipecommentModel_WhenHavingReviewGreaterThan5_ThenResultIsInvalid()
        {
            var model = CreateRecipeCommentModelFactory.WithReviewGreaterThan5();
            var validator = new CreateRecipeCommentModelValidator();

            var result = validator.Validate(model);

            result.IsValid.Should().BeFalse();

        }

        #endregion
    }
}
