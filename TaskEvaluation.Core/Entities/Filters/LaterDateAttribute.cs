using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskEvaluation.Core.Entities.Filters
{
	public class LaterDateAttribute: ValidationAttribute
	{
		protected override ValidationResult IsValid(object value, ValidationContext validationContext)
		{
			var currentValue = (DateTime)value;

			var now = DateTime.Now;

			if (currentValue < now)
			{
				return new ValidationResult(ErrorMessage = $"Date must be later than now");
			}

			return ValidationResult.Success!;
		}
	}
}
