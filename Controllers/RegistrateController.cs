using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using AcmeWidget.Data;
using AcmeWidget.Helpers;
using AcmeWidget.Models.Entities;
using AcmeWidget.ViewModels;

namespace AcmeWidget.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class RegistrateController : Controller
    {
        private readonly AcmeDbContext _acmeDbContext;
        private readonly UserManager<AuthenticationUser> _userManager;
        private readonly IMapper _mapper;

        public RegistrateController(UserManager<AuthenticationUser> userManager, IMapper mapper, AcmeDbContext acmeDbContext)
        {
            _userManager = userManager;
            _mapper = mapper;
            _acmeDbContext = acmeDbContext;
        }

        // POST api/registrate
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]RegistrationView model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userIdentity = _mapper.Map<AuthenticationUser>(model);

            var result = await _userManager.CreateAsync(userIdentity, model.Password);

            if (!result.Succeeded) return new BadRequestObjectResult(Errors.AddErrorsToModelState(result, ModelState));

            await _acmeDbContext.AcmeUsers.AddAsync(new AcmeUser { IdentityId = userIdentity.Id, Activity = model.Activity, Comments = model.Comments });
            await _acmeDbContext.SaveChangesAsync();

            return new OkObjectResult("Account created");
        }
  }
}
