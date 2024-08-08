using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskEvaluation.Core.Entities.Business;

namespace TaskEvaluation.Core.Interfaces.IServices
{
    public interface ICourseService
    {
        //Task<IEnumerable<CourseDTO>> GetCoursesAsync();
        //Task<CourseDTO> GetCourseAsync(int id);
        Task<CourseDTO> GetCourseAsync(int id, params Expression<Func<Course, object>>[] includes);
        Task<IEnumerable<CourseDTO>> GetCoursesAsync(params Expression<Func<Course, object>>[] includes);
        Task<CourseDTO> CreateAsync(CourseDTO model);
		Task UpdateAsync(CourseDTO model);
		Task DeleteAsync(int id);
	}
}
