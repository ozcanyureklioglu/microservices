using AutoMapper;
using MS.Catalog.Api.Features.Categories.DTOs;

namespace MS.Catalog.Api.Features.Categories
{
    public class CategoryMapping:Profile
    {
        public CategoryMapping()
        {
            CreateMap<Category, CategoryDto>();
        }
    }
}
