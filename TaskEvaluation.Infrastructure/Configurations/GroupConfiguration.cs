using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskEvaluation.Core.Entities.Business;

namespace TaskEvaluation.Core.Configurations
{
    public class GroupConfiguration : IEntityTypeConfiguration<Group>
    {
        public void Configure(EntityTypeBuilder<Group> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(a => a.Title).IsRequired();

            builder.HasOne(g => g.Course)
              .WithMany(c => c.Groups)
              .HasForeignKey(g => g.CourseId)
              .OnDelete(DeleteBehavior.SetNull);

            builder.HasMany(a => a.Assignments)
              .WithOne(s => s.Group)
              .HasForeignKey(s => s.GroupId)
              .OnDelete(DeleteBehavior.SetNull);

            builder.HasMany(a => a.Students)
              .WithOne(s => s.Group)
              .HasForeignKey(s => s.GroupId)
              .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
