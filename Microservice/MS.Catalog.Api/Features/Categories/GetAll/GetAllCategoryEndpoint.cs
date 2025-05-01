using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MS.Catalog.Api.Features.Categories.create;
using MS.Catalog.Api.Features.Categories.DTOs;
using MS.Catalog.Api.Repositories;
using MS.Shared;
using MS.Shared.Filters;

namespace MS.Catalog.Api.Features.Categories.GetAll
{
    public record GetAllCategoryQuery: IRequestByServiceResult<List<CategoryDto>>;

    public class GetAllCategoryHandler(AppDbContext context,IMapper mapper) : IRequestHandler<GetAllCategoryQuery, ServiceResult<List<CategoryDto>>>
    {
        public async Task<ServiceResult<List<CategoryDto>>> Handle(GetAllCategoryQuery request, CancellationToken cancellationToken)
        {
            var list = await context.Categories.ToListAsync(cancellationToken:cancellationToken);

            //var returnList = list.Select(x => new CategoryDto(x.Id, x.Name)).ToList();
            var returnList = mapper.Map<List<CategoryDto>>(list);

            return ServiceResult<List<CategoryDto>>.SuccessAsOk(returnList);
        }
    }

    public static class GetAllCategoryEndpoint
    {
        public static RouteGroupBuilder GetAllCategoryGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapGet("/",
                    async (IMediator mediator) =>
                    (await mediator.Send(new GetAllCategoryQuery())).ToResult())
                .MapToApiVersion(1, 0)
                .WithName("GetAllCategory");

            return group;
        }
    }
}
