using MediatR;
using MS.Shared;
using MS.Shared.Filters;

namespace MS.Basket.Api.Features.Baskets.AddBasketItem
{
    public static class AddBasketItemEndpoint
    {
        public static RouteGroupBuilder AddBasketItemGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapPost("/item",
                    async (AddBasketItemCommand command, IMediator mediator) =>
                        (await mediator.Send(command)).ToResult())
                .WithName("AddBasketItem")
                .MapToApiVersion(1, 0)
                .AddEndpointFilter<ValidationFilter<AddBasketItemCommandValidator>>();


            return group;
        }
    }
}
