using Microsoft.EntityFrameworkCore.Query;

namespace TaskEvaluation.Core.Interfaces.IRepositories
{
	public interface IBaseRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAll(params Expression<Func<T, object>>[] includes);
        Task<T> GetById<IdType>(IdType id, params Expression<Func<T, object>>[] includes);
        Task<T> Create(T model);
		Task Update(T model);
		Task Delete(T model);
		Task SaveChangesAsync();
	}
}
