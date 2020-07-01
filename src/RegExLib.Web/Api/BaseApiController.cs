using Microsoft.AspNetCore.Mvc;

namespace RegExLib.Web.Api
{
  [Route("api/[controller]")]
  [ApiController]
  public abstract class BaseApiController : Controller
  {
  }
}
