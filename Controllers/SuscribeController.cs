using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using AcmeWidget.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using AcmeWidget.ViewModels;
using AcmeWidget.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;

namespace AcmeWidget.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    [EnableCors("SiteCorsPolicy")]
    public class SuscribeController : Controller
    {
      private readonly AcmeDbContext _acmeDbContext;
      private readonly IMapper _mapper;

      public SuscribeController(IMapper mapper, AcmeDbContext acmeDbContext)
      {
        _mapper = mapper;
        _acmeDbContext = acmeDbContext;
      }

      [HttpGet]
      public async Task<IActionResult> Get()
      {
        var users = await _acmeDbContext.AcmeUsers.Include(c => c.Identity).ToListAsync();
        List<SignedUser> signedUsers = new List<SignedUser>();
        foreach(var user in users)
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
