using AcmeWidget.ViewModels;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AcmeWidget.Helpers.Validations
{
  public class RegistrationViewValidator : AbstractValidator<RegistrationView>
  {
    public RegistrationViewValidator()
    {
      RuleFor(vm => vm.Email).NotEmpty().WithMessage("Email cannot be empty");
      RuleFor(vm => vm.Password).NotEmpty().WithMessage("Password cannot be empty");
      RuleFor(vm => vm.FirstName).NotEmpty().WithMessage("FirstName cannot be empty");
      RuleFor(vm => vm.LastName).NotEmpty().WithMessage("LastName cannot be empty");
      RuleFor(vm => vm.Activity).NotEmpty().WithMessage("Activity cannot be empty");
    }
  }
}
