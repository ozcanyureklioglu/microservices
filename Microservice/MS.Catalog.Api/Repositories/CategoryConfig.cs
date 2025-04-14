using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MongoDB.EntityFrameworkCore.Extensions;
using MS.Catalog.Api.Features.Categories;
using System.Reflection.Emit;

namespace MS.Catalog.Api.Repositories
{
    public class CategoryConfig : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToCollection("courses");
            builder.HasKey(x => x.Id);
            builder.Ignore(x => x.Courses);
            builder.Property(x => x.Id).ValueGeneratedNever();
        }
    }
}
