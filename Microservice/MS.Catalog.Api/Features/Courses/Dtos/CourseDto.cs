using MS.Catalog.Api.Features.Categories;
using MS.Catalog.Api.Features.Categories.DTOs;

namespace MS.Catalog.Api.Features.Courses.Dtos
{
    public class CourseDto{
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string? Picture { get; set; }
        public CategoryDto Category { get; set; }
        public FeatureDto Feature { get; set; }
    }
}
