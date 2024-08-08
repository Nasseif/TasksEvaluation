using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Text.RegularExpressions;
using TaskEvaluation.Core.Entities.Business;

namespace TaskEvaluation.Core.Configurations
{
	public class SolutionConfiguration : IEntityTypeConfiguration<Solution>
    {
        public void Configure(EntityTypeBuilder<Solution> builder)
        {

            builder.HasKey(x => x.Id);
            builder.Property(a => a.SolutionFile).IsRequired();

            builder.HasOne(sol => sol.Student)
               .WithMany(st => st.Solutions)
               .HasForeignKey(sol => sol.StudentId)
               .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(sol => sol.Assignment)
               .WithOne(a => a.Solution)
               .HasForeignKey<Solution>(sol => sol.AssignmentId)
               .OnDelete(DeleteBehavior.Cascade); 

            builder.HasOne(eva => eva.EvaluationGrade)
               .WithMany(a => a.Solutions)
               .HasForeignKey(sol => sol.EvaluationGradeId)
               .OnDelete(DeleteBehavior.SetNull);

        }
    }
}
