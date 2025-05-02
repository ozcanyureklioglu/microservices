namespace MS.Basket.Api.Dtos
{
    public record BasketItemDto(
    Guid Id,
    string Name,
    string ImageUrl,
    decimal Price,
    decimal? PriceByApplyDiscountRate);
}
