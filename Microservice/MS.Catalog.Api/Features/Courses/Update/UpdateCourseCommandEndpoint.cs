using MediatR;
using MS.Shared;
using MS.Shared.Filters;

namespace MS.Catalog.Api.Features.Courses.Update
{
    public static class UpdateCourseCommandEndpoint
    {
        public static RouteGroupBuilder UpdateCourseGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapPut("/",
                    async (UpdateCourseCommand command, IMediator mediator) =>
                        (await mediator.Send(command)).ToResult())
                .MapToApiVersion(1, 0)
                .WithName("UpdateCourse")
                .AddEndpointFilter<ValidationFilter<UpdateCourseCommand>>();

            return group;
        }
    }
}
