using RegExLib.Core.Services;

namespace RegExLib.Core.AppSettings;
public class JwtSettings : IAppSettings
{
  public string ValidAudience { get; set; } = string.Empty;
  public string ValidIssuer { get; set; } = string.Empty;
  public string SecretKey { get; set; } = string.Empty;
}

