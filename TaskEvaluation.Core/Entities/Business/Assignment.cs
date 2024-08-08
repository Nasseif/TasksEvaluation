using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskEvaluation.Core.Entities.Filters;
namespace TaskEvaluation.Core.Entities.Business
{
    public class Assignment : BaseModel
    {
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        [LaterDate]
        public DateTime? Deadline { get; set; }
        //public int? GroupId { get; set; }
        //public Group? Group { get; set; }
        //public ICollection<Solution>? Solutions { get; set; }
        public int CourseId { get; set; }
        public Course Course { get; set; } = null!;
        public int? GroupId { get; set; }
        public Group? Group { get; set; }
        public int? SolutionId { get; set; }
        public Solution? Solution { get; set; }


    }
}
