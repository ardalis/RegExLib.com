using RegExLib.Web;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using RegExLib.Web.ApiModels;
using RegExLib.Web.Endpoints.Expressions;
using RegExLib.Web.Seeds;
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
        ExpressionDto = new ExpressionDTO
        {
          Title = "title",
          Pattern = "pattern",
          Description = "description",
          AuthorId = 1
        }
      };

      var response = await _client.PostAsync($"/api/expressions", new StringContent(JsonConvert.SerializeObject(newExpression), Encoding.UTF8, "application/json"));

      response.EnsureSuccessStatusCode();
      var stringResponse = await response.Content.ReadAsStringAsync();
      var result = JsonConvert.DeserializeObject<CreateExpressionResult>(stringResponse);

      Assert.NotNull(result);
      Assert.Equal(result.ExpressionDto.Title, newExpression.ExpressionDto.Title);
      Assert.Equal(result.ExpressionDto.Pattern, newExpression.ExpressionDto.Pattern);
      Assert.Equal(result.ExpressionDto.Description, newExpression.ExpressionDto.Description);
      Assert.Equal(result.ExpressionDto.AuthorId, newExpression.ExpressionDto.AuthorId);
    }
  }
}
