using Microsoft.Extensions.Logging.Abstractions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskEvaluation.Core.Entities.DTOs
{
    public class ForgetPasswordDTO
    {
        [EmailAddress]
        public string Email { get; set; } = null!;
    }
}
