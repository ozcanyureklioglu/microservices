using MediatR;
using MS.Shared;
using MS.Shared.Filters;

namespace MS.Catalog.Api.Features.Categories.create
{
    public static class CreateCategoryEndpoint
    {
        public static RouteGroupBuilder CreateCategoryGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapPost("/",
                    async (CreateCategoryCommand command, IMediator mediator) =>
                    (await mediator.Send(command)).ToResult())
                .WithName("CreateCategory")
                .MapToApiVersion(1, 0)
                .AddEndpointFilter<ValidationFilter<CreateCategoryCommand>>();

            return group;
        }
    }
}
