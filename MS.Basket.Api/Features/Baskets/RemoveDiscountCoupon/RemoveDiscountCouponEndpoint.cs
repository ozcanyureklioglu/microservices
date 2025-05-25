using AutoMapper;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using MS.Basket.Api.Const;
using MS.Basket.Api.Dtos;
using MS.Shared;
using MS.Shared.Services;
using System.Net;
using System.Text.Json;

namespace MS.Basket.Api.Features.Baskets.RemoveDiscountCoupon
{
    public record RemoveDiscountCouponCommand : IRequestByServiceResult;

    public class RemoveDiscountCouponCommandHandler(
    IIdentityService identityService,
    IDistributedCache cache) : IRequestHandler<RemoveDiscountCouponCommand, ServiceResult>
    {
        public async Task<ServiceResult> Handle(RemoveDiscountCouponCommand request,
            CancellationToken cancellationToken)
        {
            var cacheKey = string.Format(BasketConst.BasketCacheKey, identityService.GetUserId);

            var basketAsString = await cache.GetStringAsync(cacheKey);

            if (string.IsNullOrEmpty(basketAsString))
            {
                return ServiceResult.Error("Basket not found", HttpStatusCode.NotFound);
            }

            var basket = JsonSerializer.Deserialize<Data.Basket>(basketAsString);

            basket!.ClearDiscount();

            basketAsString = JsonSerializer.Serialize(basket);

            await cache.SetStringAsync(cacheKey, basketAsString, token: cancellationToken);

            return ServiceResult.SuccessAsNoContent();
        }
    }

    public static class RemoveDiscountCouponEndpoint
    {
        public static RouteGroupBuilder RemoveDiscountCouponGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapDelete("/remove-discount-coupon",
                    async (IMediator mediator) =>
                        (await mediator.Send(new RemoveDiscountCouponCommand())).ToResult())
                .WithName("RemoveDiscountCoupon")
                .MapToApiVersion(1, 0);


            return group;
        }
    }
}
