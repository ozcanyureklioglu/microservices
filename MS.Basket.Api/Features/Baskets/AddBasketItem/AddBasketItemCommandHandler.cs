using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using MS.Basket.Api.Const;
using MS.Basket.Api.Dtos;
using MS.Shared;
using MS.Shared.Services;
using System.Text.Json;

namespace MS.Basket.Api.Features.Baskets.AddBasketItem
{
    public class AddBasketItemCommandHandler(IDistributedCache cache, IIdentityService identityService) : IRequestHandler<AddBasketItemCommand, ServiceResult>
    {
        public async Task<ServiceResult> Handle(AddBasketItemCommand request, CancellationToken cancellationToken)
        {
            var userId = identityService.GetUserId;

            var cacheKey = string.Format(BasketConst.BasketCacheKey, userId);

            var basketAsString = await cache.GetStringAsync(cacheKey);

            BasketDto? basketDto;

            var basketItemDto = new BasketItemDto(request.CourseId, request.CourseName, request.ImageUrl, request.CoursePrice,null);

            if (string.IsNullOrEmpty(basketAsString))
            {
                basketDto = new BasketDto(userId, [basketItemDto]);

            }
            else
            {
                basketDto = JsonSerializer.Deserialize<BasketDto>(basketAsString);

                var exists = basketDto.BasketItems.FirstOrDefault(x => x.Id == request.CourseId);

                if (exists is not null)
                {
                    basketDto.BasketItems.Remove(exists);
                    basketDto.BasketItems.Add(basketItemDto);
                }
                else
                {
                    basketDto.BasketItems.Add(basketItemDto);
                }

            }

            basketAsString = JsonSerializer.Serialize(basketDto);

            await cache.SetStringAsync(cacheKey, basketAsString, token: cancellationToken);

            return ServiceResult.SuccessAsNoContent();
        }
    }
}
