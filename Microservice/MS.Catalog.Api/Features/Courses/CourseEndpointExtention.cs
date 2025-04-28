using MS.Catalog.Api.Features.Categories.create;
using MS.Catalog.Api.Features.Courses.Create;
using MS.Catalog.Api.Features.Courses.Delete;
using MS.Catalog.Api.Features.Courses.GetAll;
using MS.Catalog.Api.Features.Courses.GetByUserId;
using MS.Catalog.Api.Features.Courses.Update;

namespace MS.Catalog.Api.Features.Courses
{
    public static class CourseEndpointExtention
    {
        public static void AddCourseGroupEndpointExt(this WebApplication app)
        {
            app.MapGroup("api/courses").WithTags("Courses")
                .CreateCourseGroupItemEndpoint()
                .GetByUserIdCourseGroupItemEndpoint()
                .UpdateCourseGroupItemEndpoint()
                .DeleteCourseGroupItemEndpoint()
                .GetAllCourseGroupItemEndpoint();
        }
    }
}
