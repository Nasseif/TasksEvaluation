using System.Linq.Expressions;

namespace TaskEvaluation.Infrastructure.Services
{
	public class SolutionService : ISolutionService
    {
		private readonly IBaseMapper<Solution, SolutionDTO> _solutionDTOMapper;
		private readonly IBaseMapper<SolutionDTO, Solution> _solutionMapper;
		private readonly IBaseRepository<Solution> _solutionRepository;

		public SolutionService(
			IBaseMapper<Solution, SolutionDTO> solutionDTOMapper,
			IBaseMapper<SolutionDTO, Solution> solutionMapper,
			IBaseRepository<Solution> solutionRepository)
		{
			_solutionDTOMapper = solutionDTOMapper;
			_solutionMapper = solutionMapper;
			_solutionRepository = solutionRepository;
		}

		public async Task<SolutionDTO> CreateAsync(SolutionDTO model)
		{
			var entity = _solutionMapper.MapModel(model);
			entity.EntryDate = DateTime.Now;
			return _solutionDTOMapper.MapModel(await _solutionRepository.Create(entity));
		}

		public async Task DeleteAsync(int id)
		{
			var entity = await _solutionRepository.GetById(id);
			await _solutionRepository.Delete(entity);
		}

		//public async Task<SolutionDTO> GetSolutionAsync(int id) => _solutionDTOMapper.MapModel(await _solutionRepository.GetById(id));

		//public async Task<IEnumerable<SolutionDTO>> GetSolutionsAsync() => _solutionDTOMapper.MapList(await _solutionRepository.GetAll());
		public async Task<SolutionDTO> GetSolutionAsync(int id, params Expression<Func<Solution, object>>[] includes)
		{
			var solution = await _solutionRepository.GetById(id, includes);
			return _solutionDTOMapper.MapModel(solution);
		}

		public async Task<IEnumerable<SolutionDTO>> GetSolutionsAsync(params Expression<Func<Solution, object>>[] includes)
		{
			var solution = await _solutionRepository.GetAll(includes);
			return _solutionDTOMapper.MapList(solution);
		}

		public async Task UpdateAsync(SolutionDTO model)
		{
			var existingData = _solutionMapper.MapModel(model);
			existingData.UpdateDate = DateTime.Now;
			await _solutionRepository.Update(existingData);
		}
	}
}
