using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RegExLib.Infrastructure.Identity;
using Swashbuckle.AspNetCore.Annotations;

namespace RegExLib.Web.Endpoints.Authenticate
{
  public class AuthenticateRequest
  {
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
  }
}
