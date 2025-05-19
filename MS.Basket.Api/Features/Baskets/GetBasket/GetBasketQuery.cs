using MS.Basket.Api.Dtos;
using MS.Shared;

namespace MS.Basket.Api.Features.Baskets.GetBasket
{
    public record GetBasketQuery: IRequestByServiceResult<BasketDto>;
}
