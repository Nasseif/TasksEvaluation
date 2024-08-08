using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskEvaluation.Core.Consts;
using TaskEvaluation.Core.Entities.Business;
using TaskEvaluation.Core.Entities.DTOs;

namespace TaskEvaluation.Core.Validators
{
    public class EvaluationGradeValidator : AbstractValidator<EvaluationGardeDTO>
    {
        public EvaluationGradeValidator()
        {
            RuleFor(g => g.Grade)
           .NotNull().WithMessage(Errors.RequiredField);
        }

    }
}
