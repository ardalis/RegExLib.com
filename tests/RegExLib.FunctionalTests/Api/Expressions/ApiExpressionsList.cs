using RegExLib.Web;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.TestHost;
using RegExLib.Web.Endpoints.Expressions;
using RegExLib.Web.Seeds;
using Xunit;

namespace RegExLib.FunctionalTests.Api.expressions
{
  public class ApiExpressionsList : IClassFixture<CustomWebApplicationFactory<Startup>>
  {
    private readonly HttpClient _client;

    public ApiExpressionsList(CustomWebApplicationFactory<Startup> factory)
    {
      _client = factory
        .WithWebHostBuilder(builder => builder.UseSolutionRelativeContentRoot(""))
        .CreateClient();
    }

    [Fact]
    public async Task ReturnsPageNumberOneExpressions()
    {
      var response = await _client.GetAsync("/api/expressions?page=0");
      response.EnsureSuccessStatusCode();
      var stringResponse = await response.Content.ReadAsStringAsync();
      var result = JsonConvert.DeserializeObject<PagedExpressionResult>(stringResponse);

      Assert.Equal(0, result.Page);
      Assert.Equal(1, result.TotalRecords);
      Assert.Single(result.Expressions);
      Assert.Contains(result.Expressions, i => i.Title == ExpressionsSeed.Expression1.Title);
    }
  }
}
