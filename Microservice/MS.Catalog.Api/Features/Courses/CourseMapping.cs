using AutoMapper;
using MS.Catalog.Api.Features.Courses.Create;

namespace MS.Catalog.Api.Features.Courses
{
    public class CourseMapping : Profile
    {
        public CourseMapping()
        {
            CreateMap<CreateCourseCommand, Course>();
        }
    }
}
