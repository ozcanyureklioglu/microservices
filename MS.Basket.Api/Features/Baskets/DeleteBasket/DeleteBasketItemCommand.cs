using MS.Shared;

namespace MS.Basket.Api.Features.Baskets.DeleteBasket
{
    public record DeleteBasketItemCommand(Guid Id) : IRequestByServiceResult;
}
