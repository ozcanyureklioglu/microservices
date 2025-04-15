using MediatR;
using MS.Shared;

namespace MS.Catalog.Api.Features.Categories.create
{
    public record CreateCategoryCommand(string name): IRequest<ServiceResult<CreateCategoryResponse>>;
}
