using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskEvaluation.Core.Consts
{
    public static class Errors
    {
        public const string RequiredField = "Required field";
        public const string MaxLength = "{PropertyName} cannot be more than {MaxLength} Characters";
        public const string NotAllowed = "{PropertyName} must end with .pdf or .png or .jpg or .zip or .jpeg";
        public const string NotEmpty = "{PropertyName} cannot be empty.";
        public const string MobileNumber = "Invalid Egyptian mobile number format.";
        public const string Email = "Invalid email format. ";
    }
}
