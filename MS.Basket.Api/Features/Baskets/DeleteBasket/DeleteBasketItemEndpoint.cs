﻿using MediatR;
using MS.Shared;

namespace MS.Basket.Api.Features.Baskets.DeleteBasket
{
    public static class DeleteBasketItemEndpoint
    {
        public static RouteGroupBuilder DeleteBasketItemGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapDelete("/item/{id:guid}",
                    async (Guid id, IMediator mediator) =>
                        (await mediator.Send(new DeleteBasketItemCommand(id))).ToResult())
                .WithName("DeleteBasketItem")
                .MapToApiVersion(1, 0);


            return group;
        }
    }
}
