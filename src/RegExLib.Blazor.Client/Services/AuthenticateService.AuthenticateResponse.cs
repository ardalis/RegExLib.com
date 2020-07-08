using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RegExLib.Blazor.Client.Services
{
  public class AuthenticateResponse
  {
    public AuthenticateResponse()
    {
    }
    public bool Result { get; set; }
    public string Token { get; set; } = string.Empty;
  }
}
