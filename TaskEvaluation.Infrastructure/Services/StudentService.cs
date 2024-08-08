using System.Linq.Expressions;

namespace TaskEvaluation.Infrastructure.Services
{
	public class StudentService : IStudentService
    {
		private readonly IBaseMapper<Student, StudentDTO> _studentDTOMapper;
		private readonly IBaseMapper<StudentDTO, Student> _studentMapper;
		private readonly IBaseRepository<Student> _studentRepository;

		public StudentService(
			IBaseMapper<Student, StudentDTO> studentDTOMapper,
			IBaseMapper<StudentDTO, Student> studentMapper,
			IBaseRepository<Student> studentRepository)
		{
			_studentDTOMapper = studentDTOMapper;
			_studentMapper = studentMapper;
			_studentRepository = studentRepository;
		}

		public async Task<StudentDTO> CreateAsync(StudentDTO model)
		{
			var entity = _studentMapper.MapModel(model);
			entity.EntryDate = DateTime.Now;
			return _studentDTOMapper.MapModel(await _studentRepository.Create(entity));
		}

		public async Task DeleteAsync(int id)
		{
			var entity = await _studentRepository.GetById(id);
			await _studentRepository.Delete(entity);
		}

		//public async Task<StudentDTO> GetStudentAsync(int id) => _studentDTOMapper.MapModel(await _studentRepository.GetById(id));

		//public async Task<IEnumerable<StudentDTO>> GetStudentsAsync() => _studentDTOMapper.MapList(await _studentRepository.GetAll());
		public async Task<StudentDTO> GetStudentAsync(int id, params Expression<Func<Student, object>>[] includes)
		{
			var assignment = await _studentRepository.GetById(id, includes);
			return _studentDTOMapper.MapModel(assignment);
		}

		public async Task<IEnumerable<StudentDTO>> GetStudentsAsync(params Expression<Func<Student, object>>[] includes)
		{
			var student = await _studentRepository.GetAll(includes);
			return _studentDTOMapper.MapList(student);
		}

		public async Task UpdateAsync(StudentDTO model)
		{
			var existingData = _studentMapper.MapModel(model);
			existingData.UpdateDate = DateTime.Now;
			await _studentRepository.Update(existingData);
		}
	}
}
