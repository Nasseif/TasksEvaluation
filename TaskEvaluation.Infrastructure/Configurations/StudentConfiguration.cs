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
    public class StudentConfiguration : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(a => a.FullName).IsRequired();
            builder.Property(a => a.MobileNumber).IsRequired();


            builder.HasOne(s => s.Group)
              .WithMany(g => g.Students)
              .HasForeignKey(s => s.GroupId)
              .OnDelete(DeleteBehavior.SetNull);

            builder.HasMany(s => s.Solutions)
              .WithOne(sol => sol.Student)
              .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
