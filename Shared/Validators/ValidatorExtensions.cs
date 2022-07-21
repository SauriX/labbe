using FluentValidation;

namespace Shared.Validators
{
    public static class ValidatorExtensions
    {
        public static IRuleBuilder<T, string> PhoneNumber<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            var options = ruleBuilder
                .MinimumLength(13)
                .MaximumLength(13)
                .Matches(@"^\d{3}-\d{3}-\d{2}-\d{2}$");

            return options;
        }

        public static IRuleBuilder<T, string> Guid<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            var options = ruleBuilder
                .MaximumLength(36)
                .MaximumLength(36)
                .Matches(@"^[0-9a-f]{8}-[0-9a-f]{4}-[0-9a-f]{4}-[0-9a-f]{4}-[0-9a-f]{12}$");

            return options;
        }
    }
}
