using MediatR;
using MS.Catalog.Api.Features.Categories.create;
using MS.Shared;
using MS.Shared.Filters;

namespace MS.Catalog.Api.Features.Courses.Create
{
    public static class CreateCourseCommandEndpoint
    {
        public static RouteGroupBuilder CreateCourseGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapPost("/",
                    async (CreateCourseCommand command, IMediator mediator) =>
                    (await mediator.Send(command)).ToResult())
                .MapToApiVersion(1, 0)
                .WithName("CreateCourse")
                .Produces<Guid>(StatusCodes.Status201Created)
                .Produces(StatusCodes.Status404NotFound)
                .AddEndpointFilter<ValidationFilter<CreateCourseCommand>>();

            return group;
        }
    }
}
