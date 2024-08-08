using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskEvaluation.Core.Entities.Business;
namespace TaskEvaluation.Core.Entities.DTOs
{
    public class AssignmentDTO : BaseModel
    {
        [Required]
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
		[Required]
		public DateTime? Deadline { get; set; }
        [Required]
        public int CourseId { get; set; }
        public Course Course { get; set; } = null!;
        public int? GroupId { get; set; }
        public Group? Group { get; set; }
        public int? SolutionId { get; set; }
        public Solution? Solution { get; set; }


    }
}
