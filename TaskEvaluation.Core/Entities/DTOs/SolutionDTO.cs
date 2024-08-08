using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskEvaluation.Core.Entities.Business;


namespace TaskEvaluation.Core.Entities.DTOs
{
    public class SolutionDTO: BaseModel
    {
        public string SolutionFile { get; set; } = null!;
        public string? Notes { get; set; }
        public int AssignmentId { get; set; }
        public AssignmentDTO Assignment { get; set; } = null!;
        public int? EvaluationGradeId { get; set; }
        public EvaluationGardeDTO? EvaluationGrade { get; set; }
        public int? StudentId { get; set; }
        public StudentDTO? Student { get; set; }


    }
}
