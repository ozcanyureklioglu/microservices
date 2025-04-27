using MS.Catalog.Api.Features.Categories.create;
using MS.Catalog.Api.Features.Categories.GetAll;
using MS.Catalog.Api.Features.Categories.GetById;

namespace MS.Catalog.Api.Features.Categories
{
    public static class CategoryEndpointExt
    {
        public static void AddCategoryGroupEndpointExt(this WebApplication app)
        {
            app.MapGroup("api/categories").WithTags("Categories")
                .CreateCategoryGroupItemEndpoint()
                .GetByIdCategoryGroupItemEndpoint()
                .GetAllCategoryGroupItemEndpoint();
        }
    }
}
