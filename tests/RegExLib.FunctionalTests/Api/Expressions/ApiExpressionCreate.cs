using RegExLib.Web;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using RegExLib.Web.Endpoints.Expressions;
using Xunit;

namespace RegExLib.FunctionalTests.Api.expressions
{
  public class ApiExpressionCreate : IClassFixture<CustomWebApplicationFactory<Startup>>
  {
    private readonly HttpClient _client;

    public ApiExpressionCreate(CustomWebApplicationFactory<Startup> factory)
    {
      _client = factory
        .WithWebHostBuilder(builder => builder.UseSolutionRelativeContentRoot(""))
        .CreateClient();
    }

    [Fact]
    public async Task CreateNewExpression()
    {
      var newExpression = new CreateExpressionCommand
      {
        Title = "title",
        Pattern = "pattern",
        Description = "description",
        AuthorId = 1
      };

      var response = await _client.PostAsync($"/api/expressions", new StringContent(JsonConvert.SerializeObject(newExpression), Encoding.UTF8, "application/json"));

      response.EnsureSuccessStatusCode();
      var stringResponse = await response.Content.ReadAsStringAsync();
      var result = JsonConvert.DeserializeObject<CreateExpressionResult>(stringResponse);

      Assert.NotNull(result);
      Assert.Equal(result.Title, newExpression.Title);
      Assert.Equal(result.Pattern, newExpression.Pattern);
      Assert.Equal(result.Description, newExpression.Description);
      Assert.Equal(result.AuthorId, newExpression.AuthorId);
    }
  }
}
