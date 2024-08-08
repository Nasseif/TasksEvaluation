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
    public class StudentValidator: AbstractValidator<StudentDTO>
    {
        public StudentValidator()
        {
            RuleFor(student => student.FullName)
                .NotNull().WithMessage(Errors.RequiredField);

            RuleFor(dto => dto.MobileNumber)
            .NotEmpty().WithMessage(Errors.NotEmpty)
            .Must(BeValidEgyptianMobileNumber).WithMessage(Errors.MobileNumber);

            RuleFor(student => student.Email)
              .NotNull().WithMessage(Errors.RequiredField);

            RuleFor(student => student.Email)
                .EmailAddress().WithMessage(Errors.Email);

            
        }
        private bool BeValidEgyptianMobileNumber(string mobileNumber)
        {
            string pattern = @"^01[0,1,2,5]{1}[0-9]{8}$";
            return !string.IsNullOrEmpty(mobileNumber) && Regex.IsMatch(mobileNumber, pattern);
        }
    }
}
