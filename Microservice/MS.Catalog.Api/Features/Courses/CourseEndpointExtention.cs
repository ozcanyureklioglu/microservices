using MS.Catalog.Api.Features.Categories.create;
using MS.Catalog.Api.Features.Courses.Create;

namespace MS.Catalog.Api.Features.Courses
{
    public static class CourseEndpointExtention
    {
        public static void AddCourseGroupEndpointExt(this WebApplication app)
        {
            app.MapGroup("api/courses").WithTags("Courses")
                .CreateCourseGroupItemEndpoint();
        }
    }
}
