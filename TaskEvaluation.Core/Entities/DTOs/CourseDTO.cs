using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskEvaluation.Core.Entities.Business;
namespace TaskEvaluation.Core.Entities.DTOs
{
    public class CourseDTO: BaseModel
    {
        public string Title { get; set; } = null!;
        public bool IsCompleted { get; set; }
        public int? AssignmentId { get; set; }
        //public Assignment? Assignment { get; set; }
        public List<AssignmentDTO> Assignments { get; set; }
        public ICollection<Group>? Groups { get; set; }

    }
}
