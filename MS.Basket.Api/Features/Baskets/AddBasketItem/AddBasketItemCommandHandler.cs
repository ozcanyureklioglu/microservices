using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using MS.Basket.Api.Const;
using MS.Basket.Api.Dtos;
using MS.Shared;
using MS.Shared.Services;
using System.Text.Json;
using MS.Basket.Api.Data;

namespace MS.Basket.Api.Features.Baskets.AddBasketItem
{
    public class AddBasketItemCommandHandler(IDistributedCache cache, IIdentityService identityService) : IRequestHandler<AddBasketItemCommand, ServiceResult>
    {
        public async Task<ServiceResult> Handle(AddBasketItemCommand request, CancellationToken cancellationToken)
        {
            var userId = identityService.GetUserId;

            var cacheKey = string.Format(BasketConst.BasketCacheKey, userId);

            var basketAsString = await cache.GetStringAsync(cacheKey);

            Data.Basket? basketDto;

            var basketItemDto = new BasketItem(request.CourseId, request.CourseName, request.ImageUrl, request.CoursePrice,null);

            if (string.IsNullOrEmpty(basketAsString))
            {
                basketDto = new Data.Basket(userId, [basketItemDto]);

            }
            else
            {
                basketDto = JsonSerializer.Deserialize<Data.Basket>(basketAsString);

                var exists = basketDto.Items.FirstOrDefault(x => x.Id == request.CourseId);

                if (exists is not null)
                {
                    basketDto.Items.Remove(exists);
                    basketDto.Items.Add(basketItemDto);
                }
                else
                {
                    basketDto.Items.Add(basketItemDto);
                }

            }

            basketDto.ApplyAvailableDiscount();

            basketAsString = JsonSerializer.Serialize(basketDto);

            await cache.SetStringAsync(cacheKey, basketAsString, token: cancellationToken);

            return ServiceResult.SuccessAsNoContent();
        }
    }
}
