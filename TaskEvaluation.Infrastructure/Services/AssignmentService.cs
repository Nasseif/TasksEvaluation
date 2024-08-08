using System.Linq.Expressions;

namespace TaskEvaluation.Infrastructure.Services
{
	public class AssignmentService : IAssignmentService
	{
		private readonly IBaseMapper<Assignment, AssignmentDTO> _assignmentDTOMapper;
		private readonly IBaseMapper<AssignmentDTO, Assignment> _assignmentMapper;
		private readonly IBaseRepository<Assignment> _assignmentRepository;

		public AssignmentService(
			IBaseMapper<Assignment, AssignmentDTO> assignmentDTOMapper,
			IBaseMapper<AssignmentDTO, Assignment> assignmentMapper,
			IBaseRepository<Assignment> assignmentRepository)
		{
			_assignmentDTOMapper = assignmentDTOMapper;
			_assignmentMapper = assignmentMapper;
			_assignmentRepository = assignmentRepository;
		}

		public async Task<AssignmentDTO> CreateAsync(AssignmentDTO model)
		{
			var entity = _assignmentMapper.MapModel(model);
			entity.EntryDate = DateTime.Now;
			var assignment = await _assignmentRepository.Create(entity);

            return _assignmentDTOMapper.MapModel(assignment);
		}

		public async Task DeleteAsync(int id)
		{
			var entity = await _assignmentRepository.GetById(id);
			await _assignmentRepository.Delete(entity);
		}

        //public async Task<AssignmentDTO> GetAssignmentAsync(int id) => _assignmentDTOMapper.MapModel(await _assignmentRepository.GetById(id));

        //public async Task<IEnumerable<AssignmentDTO>> GetAssignmentsAsync() => _assignmentDTOMapper.MapList(await _assignmentRepository.GetAll());
        public async Task<AssignmentDTO> GetAssignmentAsync(int id, params Expression<Func<Assignment, object>>[] includes)
        {
            var assignment = await _assignmentRepository.GetById(id, includes);
            return _assignmentDTOMapper.MapModel(assignment);
        }

        public async Task<IEnumerable<AssignmentDTO>> GetAssignmentsAsync(params Expression<Func<Assignment, object>>[] includes)
        {
            var assignments = await _assignmentRepository.GetAll(includes);
            return _assignmentDTOMapper.MapList(assignments);
        }
        public async Task UpdateAsync(AssignmentDTO model)
		{
			var existingData = _assignmentMapper.MapModel(model);
			existingData.UpdateDate = DateTime.Now;
			await _assignmentRepository.Update(existingData);
		}
	}
}
