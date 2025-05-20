using AutoMapper;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using MS.Basket.Api.Const;
using MS.Basket.Api.Dtos;
using MS.Shared;
using MS.Shared.Services;
using System.Text.Json;

namespace MS.Basket.Api.Features.Baskets.GetBasket
{
    public class GetBasketHandler(IMapper mapper, IDistributedCache cache, IIdentityService identityService)
        : IRequestHandler<GetBasketQuery, ServiceResult<BasketDto>>
    {
        public async Task<ServiceResult<BasketDto>> Handle(GetBasketQuery request, CancellationToken cancellationToken)
        {
            var cacheKey = string.Format(BasketConst.BasketCacheKey, identityService.GetUserId);

            var basketAsString = await cache.GetStringAsync(cacheKey);

            if (string.IsNullOrEmpty(basketAsString))
            {
                return ServiceResult<BasketDto>.Error("Basket is empty!", System.Net.HttpStatusCode.NotFound);
            }

            var basket = JsonSerializer.Deserialize<Data.Basket>(basketAsString);

            var basketDto = mapper.Map<BasketDto>(basket);

            return ServiceResult<BasketDto>.SuccessAsOk(basketDto);

        }
    }
}
