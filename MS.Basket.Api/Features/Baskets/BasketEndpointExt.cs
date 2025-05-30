﻿using Asp.Versioning.Builder;
using MS.Basket.Api.Features.Baskets.AddBasketItem;
using MS.Basket.Api.Features.Baskets.ApplyDiscountCoupon;
using MS.Basket.Api.Features.Baskets.DeleteBasket;
using MS.Basket.Api.Features.Baskets.GetBasket;
using MS.Basket.Api.Features.Baskets.RemoveDiscountCoupon;

namespace MS.Basket.Api.Features.Baskets
{
    public static class BasketEndpointExt
    {
        public static void AddBasketGroupEndpointExt(this WebApplication app, ApiVersionSet apiVersionSet)
        {
            app.MapGroup("api/v{version:apiVersion}/baskets").WithTags("Baskets")
                .WithApiVersionSet(apiVersionSet)
                .AddBasketItemGroupItemEndpoint()
                .DeleteBasketItemGroupItemEndpoint()
                .GetBasketItemGroupItemEndpoint()
                .RemoveDiscountCouponGroupItemEndpoint()
                .ApplyDiscountCouponGroupItemEndpoint();
        }
    }
}
