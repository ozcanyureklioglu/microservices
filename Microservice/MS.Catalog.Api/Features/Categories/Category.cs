using MS.Catalog.Api.Features.Courses;
using MS.Catalog.Api.Repositories;

namespace MS.Catalog.Api.Features.Categories
{
    public class Category:BaseEntity
    {
        public string Name { get; set; } = default!;
        public List<Course>? Courses { get; set; }
    }
}
