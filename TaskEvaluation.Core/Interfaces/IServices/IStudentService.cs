using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TaskEvaluation.Core.Entities.Business;
namespace TaskEvaluation.Core.Interfaces.IServices
{
    public interface IStudentService
    {
		//Task<IEnumerable<StudentDTO>> GetStudentsAsync();
		//Task<StudentDTO> GetStudentAsync(int id);
		Task<StudentDTO> GetStudentAsync(int id, params Expression<Func<Student, object>>[] includes);
		Task<IEnumerable<StudentDTO>> GetStudentsAsync(params Expression<Func<Student, object>>[] includes);
		Task<StudentDTO> CreateAsync(StudentDTO model);
		Task UpdateAsync(StudentDTO model);
		Task DeleteAsync(int id);

	}
}
