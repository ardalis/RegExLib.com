namespace RegExLib.Web.Extensions;
public static class WebApplicationBuilderExtensions
{
  public static WebApplicationBuilder AddAppSettings(this WebApplicationBuilder webApplicationBuilder)
  {
    webApplicationBuilder.Services.AddSingleton(webApplicationBuilder.Configuration.GetJwtSettings());

    return webApplicationBuilder;
  }
}

