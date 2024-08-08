using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TaskEvaluation.Core.Entities.Business
{
    public class Solution : BaseModel
    {
        [RegularExpression(RegexPatterns.AllowedExtensions,ErrorMessage = Errors.NotAllowed)]
        public string SolutionFile { get; set; } = null!;
        public string? Notes { get; set; }
     
        public int AssignmentId { get; set; }
        public Assignment Assignment { get; set; } = null!;
        public int? EvaluationGradeId { get; set; }
        public EvaluationGrade? EvaluationGrade { get; set; }
        public int? StudentId { get; set; }
        public Student? Student { get; set; }
    }
}
