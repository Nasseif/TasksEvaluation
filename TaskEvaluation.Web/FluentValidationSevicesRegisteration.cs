using FluentValidation;
using TaskEvaluation.Core.Entities.Business;
using TaskEvaluation.Core.Entities.DTOs;
using TaskEvaluation.Core.Validators;

namespace TaskEvaluation.Web
{
    public static class FluentValidationSevicesRegisteration
    {
        public static void AddFluentValidationServices(this IServiceCollection Services)
        {
            Services.AddScoped<IValidator<AssignmentDTO>, AssignmentValidator>();
            Services.AddScoped<IValidator<CourseDTO>, CourseValidator>();
            Services.AddScoped<IValidator<EvaluationGardeDTO>, EvaluationGradeValidator>();
            Services.AddScoped<IValidator<GroupDTO>, GroupValidator>();
            Services.AddScoped<IValidator<SolutionDTO>, SolutionValidator>();
            Services.AddScoped<IValidator<StudentDTO>, StudentValidator>();
        }
    }
}
