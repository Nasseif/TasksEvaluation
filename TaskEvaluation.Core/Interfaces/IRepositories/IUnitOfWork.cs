using TaskEvaluation.Core.Entities.Business;

namespace TaskEvaluation.Core.Interfaces.IRepositories
{
    public interface IUnitOfWork
    {      
        IBaseRepository<Student> Students { get; }
        IBaseRepository<Course> Courses { get; }

        int Complete();
    }
}
