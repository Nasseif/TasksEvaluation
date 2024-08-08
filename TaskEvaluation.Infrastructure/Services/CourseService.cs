using System.Linq.Expressions;

namespace TaskEvaluation.Infrastructure.Services
{
	public class CourseService : ICourseService
    {
		private readonly IBaseMapper<Course, CourseDTO> _courseDTOMapper;
		private readonly IBaseMapper<CourseDTO, Course> _courseMapper;
		private readonly IBaseRepository<Course> _courseRepository;

		public CourseService(
			IBaseMapper<Course, CourseDTO> courseDTOMapper,
			IBaseMapper<CourseDTO, Course> courseMapper,
			IBaseRepository<Course> courseRepository)
		{
			_courseDTOMapper = courseDTOMapper;
			_courseMapper = courseMapper;
			_courseRepository = courseRepository;
		}

		public async Task<CourseDTO> CreateAsync(CourseDTO model)
		{
			var entity = _courseMapper.MapModel(model);	
			entity.EntryDate = DateTime.Now;	
			return _courseDTOMapper.MapModel(await _courseRepository.Create(entity));
		}

		public async Task DeleteAsync(int id)
		{
			var entity = await _courseRepository.GetById(id);
			await _courseRepository.Delete(entity);
		}

        //public async Task<CourseDTO> GetCourseAsync(int id) => _courseDTOMapper.MapModel(await _courseRepository.GetById(id));

        //public async Task<IEnumerable<CourseDTO>> GetCoursesAsync() => _courseDTOMapper.MapList(await _courseRepository.GetAll());
        public async Task<CourseDTO> GetCourseAsync(int id, params Expression<Func<Course, object>>[] includes)
        {
            var course = await _courseRepository.GetById(id, includes);
            return _courseDTOMapper.MapModel(course);
        }

        public async Task<IEnumerable<CourseDTO>> GetCoursesAsync(params Expression<Func<Course, object>>[] includes)
        {
            var courses = await _courseRepository.GetAll(includes);
            return _courseDTOMapper.MapList(courses);
        }

        public async Task UpdateAsync(CourseDTO model)
		{
			var existingData = _courseMapper.MapModel(model);
			existingData.UpdateDate = DateTime.Now;
			await _courseRepository.Update(existingData);
		}
	}
}
