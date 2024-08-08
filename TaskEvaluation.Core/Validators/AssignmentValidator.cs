using FluentValidation;
using TaskEvaluation.Core.Consts;
using TaskEvaluation.Core.Entities.Business;
using TaskEvaluation.Core.Entities.DTOs;
namespace TaskEvaluation.Core.Validators
{
    public class AssignmentValidator: AbstractValidator<AssignmentDTO>
    {
        public AssignmentValidator()
        {
            RuleFor(assign => assign.Title)
                .NotNull().WithMessage(Errors.RequiredField);
        }
    }
}
