using MediatR;
using MS.Shared;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Net;
using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;
using MS.Shared.Services;
using MS.Basket.Api.Const;
using MS.Basket.Api.Dtos;

namespace MS.Basket.Api.Features.Baskets.DeleteBasket
{
    public class DeleteBasketItemCommandHandler(IDistributedCache cache, IIdentityService identityService)
    : IRequestHandler<DeleteBasketItemCommand, ServiceResult>
    {
        public async Task<ServiceResult> Handle(DeleteBasketItemCommand request, CancellationToken cancellationToken)
        {
            var userId = identityService.GetUserId;

            var cacheKey = string.Format(BasketConst.BasketCacheKey, userId);

            var basketAsString = await cache.GetStringAsync(cacheKey);

            if (string.IsNullOrEmpty(basketAsString))
            {
                return ServiceResult.Error("No Cart Items",HttpStatusCode.NotFound);
            }

            var currentBasket = JsonSerializer.Deserialize<BasketDto>(basketAsString);

            var basketItemToDelete = currentBasket!.BasketItems.FirstOrDefault(x => x.Id == request.Id);

            if (basketItemToDelete is null)
            {
                return ServiceResult.Error("Basket item not found", HttpStatusCode.NotFound);
            }

            currentBasket.BasketItems.Remove(basketItemToDelete);

            basketAsString = JsonSerializer.Serialize(currentBasket);

            await cache.SetStringAsync(cacheKey, basketAsString, token: cancellationToken);

            return ServiceResult.SuccessAsNoContent();
        }
    }
}
