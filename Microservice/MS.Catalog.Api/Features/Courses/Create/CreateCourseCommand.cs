using MS.Shared;

namespace MS.Catalog.Api.Features.Courses.Create
{
    public record CreateCourseCommand(
    string Name,
    string Description,
    decimal price,
    string? PictureUrl,
    Guid CategoryId
    ):IRequestByServiceResult<Guid>;
}
