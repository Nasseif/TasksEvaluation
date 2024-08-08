using TaskEvaluation.Core.Entities.Business;

namespace TaskEvaluation.Core.Interfaces.IServices
{
	public interface IAssignmentService
    {
        //Task<IEnumerable<AssignmentDTO>> GetAssignmentsAsync();
        //Task<AssignmentDTO> GetAssignmentAsync(int id);
        Task<AssignmentDTO> GetAssignmentAsync(int id, params Expression<Func<Assignment, object>>[] includes);
        Task<IEnumerable<AssignmentDTO>> GetAssignmentsAsync(params Expression<Func<Assignment, object>>[] includes);
        Task<AssignmentDTO> CreateAsync(AssignmentDTO model);
		Task UpdateAsync(AssignmentDTO model);
		Task DeleteAsync(int id);
	}
}
