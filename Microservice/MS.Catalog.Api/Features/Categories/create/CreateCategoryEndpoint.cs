using MediatR;
using MS.Shared;

namespace MS.Catalog.Api.Features.Categories.create
{
    public static class CreateCategoryEndpoint
    {
        public static RouteGroupBuilder CreateCategoryGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapPost("/",
                    async (CreateCategoryCommand command, IMediator mediator) =>(await mediator.Send(command)).ToResult());

            return group;
        }
    }
}
