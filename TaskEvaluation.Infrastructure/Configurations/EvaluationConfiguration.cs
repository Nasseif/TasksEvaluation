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
    public class EvaluationGradeConfiguration : IEntityTypeConfiguration<EvaluationGrade>
    {
        public void Configure(EntityTypeBuilder<EvaluationGrade> builder)
        {
            builder.HasKey(x => x.Id);  
            builder.Property(a => a.Grade).IsRequired();


            builder.HasMany(g => g.Solutions)
               .WithOne(s => s.EvaluationGrade)
               .HasForeignKey(s => s.EvaluationGradeId)
               .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
