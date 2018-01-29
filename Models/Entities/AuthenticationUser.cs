using Microsoft.AspNetCore.Identity;

namespace AcmeWidget.Models.Entities
{
    public class AuthenticationUser : IdentityUser
    {
      public string FirstName { get; set; }
      public string LastName { get; set; }
  }
}
