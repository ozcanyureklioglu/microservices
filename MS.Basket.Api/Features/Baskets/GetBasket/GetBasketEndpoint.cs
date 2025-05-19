using MediatR;
using MS.Shared;

namespace MS.Basket.Api.Features.Baskets.GetBasket
{
    public static class GetBasketEndpoint
    {
        public static RouteGroupBuilder GetBasketItemGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapGet("/user",
                    async (IMediator mediator) =>
                        (await mediator.Send(new GetBasketQuery())).ToResult())
                .WithName("GetBasket")
                .MapToApiVersion(1, 0);

            return group;
        }
    }
}
