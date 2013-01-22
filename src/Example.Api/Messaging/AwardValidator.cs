using ServiceStack.FluentValidation;

namespace Example.Api.Messaging
{
    public class AwardValidator : AbstractValidator<AwardRequest>
    {
        public AwardValidator()
        {
            RuleFor(m => m.PointAmount).GreaterThan(0).WithMessage("Point must be greater than 0");
            RuleFor(m => m.PointAmount).LessThan(100).WithMessage("Point Amount must be less than 100");
            RuleFor(m => m.Comments).NotNull().NotEmpty().WithMessage("Comments cannot be empty");
        }
    }
}