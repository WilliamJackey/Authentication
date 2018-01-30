using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AcmeWidget.Data;
using AcmeWidget.ViewModels;

namespace AcmeWidget.Controllers
{
  [Produces("application/json")]
    [Route("api/[controller]")]
    [Authorize(Policy = "ApiUser")]
    public class ActivityController : Controller
    {
      private readonly AcmeDbContext _acmeDbContext;

      public ActivityController(AcmeDbContext acmeDbContext)
      {
        _acmeDbContext = acmeDbContext;
      }

      
      [HttpGet("load")]
      public async Task<IActionResult> Get()
      {
        var users = await _acmeDbContext.AcmeUsers.Include(c => c.Identity).ToListAsync();
        List<SignedUser> signedUsers = new List<SignedUser>();
        foreach (var user in users)
        {
          SignedUser signedUser = new SignedUser
          {
            Activity = user.Activity,
            Comments = user.Comments,
            FirstName = user.Identity.FirstName,
            LastName = user.Identity.LastName
          };
          signedUsers.Add(signedUser);
        }
        return new ObjectResult(signedUsers);
      }
  }
}
