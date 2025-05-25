using MediatR;
using MS.Shared;
using MS.Shared.Filters;

namespace MS.Basket.Api.Features.Baskets.ApplyDiscountCoupon
{
    public static class ApplyDiscountCouponEndpoint
    {
        public static RouteGroupBuilder ApplyDiscountCouponGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapPut("/apply-discount-coupon",
                async (ApplyDiscountCouponCommand command, IMediator mediator) =>
                    (await mediator.Send(command)).ToResult())
            .WithName("ApplyDiscountCoupon")
            .MapToApiVersion(1, 0)
            .AddEndpointFilter<ValidationFilter<ApplyDiscountCouponCommandValidator>>();
                    return group; 
        }
    }
}
