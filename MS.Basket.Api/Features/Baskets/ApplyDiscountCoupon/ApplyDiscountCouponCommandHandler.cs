using AutoMapper;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using MS.Basket.Api.Dtos;
using MS.Basket.Api.Features.Baskets.GetBasket;
using MS.Shared.Services;
using MS.Shared;
using MS.Basket.Api.Const;
using System.Text.Json;

namespace MS.Basket.Api.Features.Baskets.ApplyDiscountCoupon
{
    public class ApplyDiscountCouponCommandHandler(IMapper mapper, IDistributedCache cache, IIdentityService identityService)
        : IRequestHandler<ApplyDiscountCouponCommand, ServiceResult>
    {
        public async Task<ServiceResult> Handle(ApplyDiscountCouponCommand request, CancellationToken cancellationToken)
        {
            var cacheKey = string.Format(BasketConst.BasketCacheKey, identityService.GetUserId);

            var basketAsString = await cache.GetStringAsync(cacheKey);

            if (string.IsNullOrEmpty(basketAsString))
            {
                return ServiceResult.Error("Basket is empty!", System.Net.HttpStatusCode.NotFound);
            }

            var basket = JsonSerializer.Deserialize<Data.Basket>(basketAsString);

            basket.ApplyNewDiscount(request.Coupon, request.DiscountRate);

            basketAsString = JsonSerializer.Serialize(basket);

            await cache.SetStringAsync(cacheKey, basketAsString, token: cancellationToken);

            return ServiceResult.SuccessAsNoContent();

        }
    }
}
