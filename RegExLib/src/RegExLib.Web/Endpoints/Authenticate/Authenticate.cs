using System.Threading.Tasks;
using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RegExLib.Core.Interfaces;
using RegExLib.Infrastructure.Identity;
using Swashbuckle.AspNetCore.Annotations;

namespace RegExLib.Web.Endpoints.Authenticate
{
  public class Authenticate : EndpointBaseAsync
    .WithRequest<AuthenticateRequest>
    .WithResult<ActionResult<AuthenticateResponse>>
  {
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly ITokenClaimsService _tokenClaimsService;

    public Authenticate(SignInManager<ApplicationUser> signInManager,
      ITokenClaimsService tokenClaimsService)
    {
      _signInManager = signInManager;
      _tokenClaimsService = tokenClaimsService;
    }

    [HttpPost("api/authenticate")]
    [SwaggerOperation(
      Summary = "Authenticates a user",
      Description = "Authenticates a user",
      OperationId = "auth.authenticate",
      Tags = new[] { "AuthEndpoints" })
    ]
    public override async Task<ActionResult<AuthenticateResponse>> HandleAsync([FromForm] AuthenticateRequest request, CancellationToken cancellationToken = default)
    {
      var response = new AuthenticateResponse();

      var result = await _signInManager.PasswordSignInAsync(request.Username, request.Password, false, true);
      if (!result.Succeeded)
      {
        return Unauthorized();
      }

      response.Result = result.Succeeded;
      response.Token = await _tokenClaimsService.GetTokenAsync(request.Username);
      response.Username = request.Username;

      return response;
    }

  }
}
