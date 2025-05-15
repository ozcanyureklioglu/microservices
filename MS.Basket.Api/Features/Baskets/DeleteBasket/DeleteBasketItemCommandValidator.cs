using FluentValidation;

namespace MS.Basket.Api.Features.Baskets.DeleteBasket
{
    public class DeleteBasketItemCommandValidator : AbstractValidator<DeleteBasketItemCommand>
    {
        public DeleteBasketItemCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("CourseId is required");
        }
    }
}
