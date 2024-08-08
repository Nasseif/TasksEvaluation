using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskEvaluation.Core.Entities.DTOs
{
	public class RegisterDTO
	{
		
		
        [Required(ErrorMessage = "FullName is required")]
        [MaxLength(50)]
        public string FullName { get; set; } 

        [Display(Name = "Email address")]
        [Required(ErrorMessage = "Email address is required")]
        [EmailAddress]
        public string Email { get; set; }
        [RegularExpression(RegexPatterns.MobileNumber, ErrorMessage = Errors.MobileNumber)]
        public string MobileNumber { get; set; } = null!;


        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Confirm password")]
        [Required(ErrorMessage = "Confirm password is required")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; }

    }
}
