using RegExLib.Web;
using System.Net.Http;
using Microsoft.AspNetCore.TestHost;
using Xunit;

namespace RegExLib.FunctionalTests.Api.expressions
{
  public class ApiExpressionAdd : IClassFixture<CustomWebApplicationFactory<Startup>>
  {
    private readonly HttpClient _client;

    public ApiExpressionAdd(CustomWebApplicationFactory<Startup> factory)
    {
      _client = factory
        .WithWebHostBuilder(builder => builder.UseSolutionRelativeContentRoot(""))
        .CreateClient();
    }
  }
}
