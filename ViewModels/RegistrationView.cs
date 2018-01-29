
using FluentValidation.Attributes;
using AcmeWidget.Helpers.Validations;

namespace AcmeWidget.ViewModels
{
    [Validator(typeof(RegistrationViewValidator))]
    public class RegistrationView
    {
      public string Activity { get; set; }
      public string Comments { get; set; }
      public string Email { get; set; }
      public string FirstName { get; set; }
      public string LastName { get; set; }
      public string Password { get; set; }
  }
}
