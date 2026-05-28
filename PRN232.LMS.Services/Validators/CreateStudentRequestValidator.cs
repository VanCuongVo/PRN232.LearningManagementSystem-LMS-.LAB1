using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using PRN232.LMS.Models.RequestModel;

namespace PRN232.LMS.Services.Validators
{
    public class CreateStudentRequestValidator : AbstractValidator<CreateStudentRequest>
    {
        public CreateStudentRequestValidator()
        {
            RuleFor(x => x.FullName).NotEmpty().NotNull().MaximumLength(100).MinimumLength(3);
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
           
        }

    }
}