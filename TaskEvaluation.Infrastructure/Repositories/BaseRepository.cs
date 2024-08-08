using System.Linq.Expressions;

namespace TaskEvaluation.Infrastructure.Repositoies
{
	public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
		protected readonly ApplicationDbContext _dbContext;
		protected DbSet<T> DbSet => _dbContext.Set<T>();

		public BaseRepository(ApplicationDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task<T> Create(T model)
		{
			await DbSet.AddAsync(model);
			await SaveChangesAsync();
			return model;
		}

		public async Task Delete(T model)
		{
			DbSet.Remove(model);
			await SaveChangesAsync();
		}

        //public async Task<IEnumerable<T>> GetAll( ) => await DbSet.AsNoTracking().ToListAsync();

        //public async Task<T> GetById<IdType>(IdType id)
        //{
        //	var data = await DbSet.FindAsync(id);
        //	return data is null ? throw new InvalidOperationException("No data Found") : data;
        //}
        public async Task<IEnumerable<T>> GetAll(params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = DbSet.AsNoTracking();
            query = ApplyIncludes(query, includes);
            return await query.ToListAsync();
        }

        public async Task<T> GetById<IdType>(IdType id, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = DbSet.AsNoTracking();
            query = ApplyIncludes(query, includes);
            var data = await query.FirstOrDefaultAsync(e => EF.Property<IdType>(e, "Id").Equals(id));
            return data ?? throw new InvalidOperationException("No data found");
        }
        public async Task SaveChangesAsync() => await _dbContext.SaveChangesAsync();

		public async Task Update(T model)
		{
			DbSet.Update(model);
			await SaveChangesAsync();
		}
        private IQueryable<T> ApplyIncludes(IQueryable<T> query, params Expression<Func<T, object>>[] includes)
        {
            if (includes != null)
            {
                query = includes.Aggregate(query, (current, include) => current.Include(include));
            }
            return query;
        }

        
    }
}
