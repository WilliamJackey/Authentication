using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using AcmeWidget.Authentication;
using AcmeWidget.Models;
using AcmeWidget.Models.Entities;
using AcmeWidget.ViewModels;
using AcmeWidget.Helpers;
using Microsoft.AspNetCore.Cors;

namespace AcmeWidget.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [EnableCors("SiteCorsPolicy")]
  public class SignInController : Controller
    {
    private readonly UserManager<AuthenticationUser> _userManager;
    private readonly IJwtFactory _jwtFactory;
    private readonly JwtIssuerOptions _jwtOptions;

    public SignInController(UserManager<AuthenticationUser> userManager, IJwtFactory jwtFactory, IOptions<JwtIssuerOptions> jwtOptions)
    {
      _userManager = userManager;
      _jwtFactory = jwtFactory;
      _jwtOptions = jwtOptions.Value;
    }

    // POST api/SignIn/login
    [HttpPost("login")]
    public async Task<IActionResult> Post([FromBody]CredentialsView credentials)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      var identity = await GetClaimsIdentity(credentials.UserName, credentials.Password);
      if (identity == null)
      {
        return BadRequest(Errors.AddErrorToModelState("login_failure", "Invalid username or password.", ModelState));
      }

      var jwt = await Tokens.GenerateJwt(identity, _jwtFactory, credentials.UserName, _jwtOptions, new JsonSerializerSettings { Formatting = Formatting.Indented });
      return new OkObjectResult(jwt);
    }

    private async Task<ClaimsIdentity> GetClaimsIdentity(string userName, string password)
    {
      if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
        return await Task.FromResult<ClaimsIdentity>(null);

      // verify the user name
      var userToVerify = await _userManager.FindByNameAsync(userName);

      if (userToVerify == null) return await Task.FromResult<ClaimsIdentity>(null);

      // check the credentials
      if (await _userManager.CheckPasswordAsync(userToVerify, password))
      {
        return await Task.FromResult(_jwtFactory.GenerateClaimsIdentity(userName, userToVerify.Id));
      }

      // Credentials are invalid, or account doesn't exist
      return await Task.FromResult<ClaimsIdentity>(null);
    }
  }
}
