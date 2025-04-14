using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MongoDB.EntityFrameworkCore.Extensions;
using MS.Catalog.Api.Features.Courses;
using System.Reflection.Emit;

namespace MS.Catalog.Api.Repositories
{
    public class CourseConfig : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> modelBuilder)
        {
            modelBuilder.ToCollection("courses");
            modelBuilder.HasKey(x => x.Id);
            modelBuilder.Property(x => x.Id).ValueGeneratedNever();
            modelBuilder.Property(x => x.Name).HasMaxLength(100);
            modelBuilder.Property(x => x.Description).HasMaxLength(1000);
            modelBuilder.Ignore(x => x.Category);
            modelBuilder.OwnsOne(c => c.Feature, feature =>
            {
                feature.HasElementName("feature");
            });
        }
    }
}
