using FluentValidation.Attributes;
using AcmeWidget.Helpers.Validations;

namespace AcmeWidget.ViewModels
{
    [Validator(typeof(CredentialsViewValidator))]
    public class CredentialsView
    {
      public string UserName { get; set; }
      public string Password { get; set; }
    }
}
