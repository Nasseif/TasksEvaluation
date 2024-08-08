
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace TaskEvaluation.Core.Entities.Business
{
    public class Student: BaseModel
    {
        public string FullName { get; set; } = null!;
        [RegularExpression(RegexPatterns.MobileNumber ,ErrorMessage = Errors.MobileNumber)]
        public string MobileNumber { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? ProfilePic { get; set; }
        public int? GroupId { get; set; }
        public Group? Group { get; set; }
        public ICollection<Solution>? Solutions { get; set; }
    }
}
