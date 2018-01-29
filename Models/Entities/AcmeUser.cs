using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AcmeWidget.Models.Entities
{
    public class AcmeUser
    {
      public int Id { get; set; }
      public string IdentityId { get; set; }
      public AuthenticationUser Identity { get; set; }  // navigation property
      public string Activity { get; set; }
      public string Comments { get; set; }
  }
}
