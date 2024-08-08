using TaskEvaluation.Infrastructure.Repositoies;

namespace TaskEvaluation.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public IBaseRepository<Student> Students => new BaseRepository<Student>(_context);
        public IBaseRepository<Course> Courses => new BaseRepository<Course>(_context);

        public int Complete()
        {
            return _context.SaveChanges();
        }
    }
}
