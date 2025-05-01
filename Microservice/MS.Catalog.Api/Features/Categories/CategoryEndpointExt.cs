using Asp.Versioning.Builder;
using MS.Catalog.Api.Features.Categories.create;
using MS.Catalog.Api.Features.Categories.GetAll;
using MS.Catalog.Api.Features.Categories.GetById;

namespace MS.Catalog.Api.Features.Categories
{
    public static class CategoryEndpointExt
    {
        public static void AddCategoryGroupEndpointExt(this WebApplication app, ApiVersionSet apiVersionSet)
        {
            app.MapGroup("api/v{version:apiVersion}/categories").WithTags("Categories").WithApiVersionSet(apiVersionSet)
                .CreateCategoryGroupItemEndpoint()
                .GetByIdCategoryGroupItemEndpoint()
                .GetAllCategoryGroupItemEndpoint();
        }
    }
}
