using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskEvaluation.Core.Configurations
{
    public class CourseConfiguration : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.Property(a => a.Title).IsRequired();
            builder.HasOne(c => c.Assignment)
                .WithOne(a => a.Course)
                .HasForeignKey<Assignment>(a => a.CourseId);

            builder.HasMany(c => c.Groups)
                .WithOne(c => c.Course)
                .HasForeignKey(c => c.CourseId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
