using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskEvaluation.Core.Entities.Business;

namespace TaskEvaluation.Core.Entities.DTOs
{
    public class StudentDTO: BaseModel
    {
        public string FullName { get; set; } = null!;
        public string MobileNumber { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? ProfilePic { get; set; }
        public int? GroupId { get; set; }
        public Group? Group { get; set; }
        public ICollection<Solution>? Solutions { get; set; }
    }
}
