using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskEvaluation.Core.Entities.Business;

namespace TaskEvaluation.Infrastructure.Configurations
{
	public class AssignmentConfiguration : IEntityTypeConfiguration<Assignment>
    {
        public void Configure(EntityTypeBuilder<Assignment> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Title).IsRequired();

            builder.HasOne(a => a.Solution)
               .WithOne(s => s.Assignment)
               .HasForeignKey<Solution>(s => s.AssignmentId).OnDelete(DeleteBehavior.SetNull); ;

            builder.HasOne(a => a.Group)
                   .WithMany(g => g.Assignments)
                   .HasForeignKey(a => a.GroupId)
                   .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
