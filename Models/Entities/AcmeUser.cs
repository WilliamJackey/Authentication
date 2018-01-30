namespace AcmeWidget.Models.Entities
{
    public class AcmeUser
    {
      public string Activity { get; set; }
      public string Comments { get; set; }
      public int Id { get; set; }
      public string IdentityId { get; set; }
      public AuthenticationUser Identity { get; set; }  // navigation property
  }
}
