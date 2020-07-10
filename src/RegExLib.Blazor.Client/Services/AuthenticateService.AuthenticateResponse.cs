namespace RegExLib.Blazor.Client.Services
{
  public class AuthenticateResponse
  {
    public AuthenticateResponse()
    {
    }
    public bool Result { get; set; }
    public string Token { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
  }
}
