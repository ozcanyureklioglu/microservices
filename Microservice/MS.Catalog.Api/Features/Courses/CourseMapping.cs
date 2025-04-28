using AutoMapper;
using MS.Catalog.Api.Features.Courses.Create;
using MS.Catalog.Api.Features.Courses.Dtos;

namespace MS.Catalog.Api.Features.Courses
{
    public class CourseMapping : Profile
    {
        public CourseMapping()
        {
            CreateMap<CreateCourseCommand, Course>();
            CreateMap<Course, CourseDto>().ReverseMap();
            CreateMap<Feature, FeatureDto>().ReverseMap();
        }
    }
}
