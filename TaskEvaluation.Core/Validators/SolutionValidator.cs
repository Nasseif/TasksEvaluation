using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TaskEvaluation.Core.Consts;
using TaskEvaluation.Core.Entities.Business;
using TaskEvaluation.Core.Entities.DTOs;

namespace TaskEvaluation.Core.Validators
{
    public class SolutionValidator : AbstractValidator<SolutionDTO>
    {
        static readonly Regex validFileExtensions = new Regex
            (@"\.pdf$|\.png$|\.zip$|\.jpeg$|\.jpg$|\.rar$", RegexOptions.IgnoreCase);

        public SolutionValidator()
        {
            RuleFor(sol => sol.SolutionFile)
                .NotNull().WithMessage(Errors.RequiredField)
                .Matches(validFileExtensions).WithMessage(Errors.NotAllowed);
        }
    }
}
