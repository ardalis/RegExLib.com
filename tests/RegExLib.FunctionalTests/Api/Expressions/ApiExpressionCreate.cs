using System.Net;
using RegExLib.Web;
using System.Net.Http;
using System.Net.Http.Headers;
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

    private readonly string _title;
    private readonly string _pattern;
    private readonly string _description;
    private readonly int _authorId;

    public ApiExpressionCreate(CustomWebApplicationFactory<Startup> factory)
    {
      _client = factory
        .WithWebHostBuilder(builder => builder.UseSolutionRelativeContentRoot(""))
        .CreateClient();

      _title = "title";
      _pattern = "pattern";
      _description = "description";
      _authorId = 1;
    }

    [Fact]
    public async Task ReturnsNotAuthorizedGivenNormalUserToken()
    {
      var jsonContent = GetValidNewExpressionJson();
      var token = ApiTokenHelper.GetNormalUserToken();
      _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

      var response = await _client.PostAsync("api/expressions", jsonContent);

      Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
    }

    [Fact]
    public async Task CreateNewExpression()
    {
      var jsonContent = GetValidNewExpressionJson();
      var adminToken = ApiTokenHelper.GetAdminUserToken();
      _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", adminToken);

      var response = await _client.PostAsync($"/api/expressions", jsonContent);

      response.EnsureSuccessStatusCode();
      var stringResponse = await response.Content.ReadAsStringAsync();
      var result = JsonConvert.DeserializeObject<CreateExpressionResult>(stringResponse);

      Assert.NotNull(result);
      Assert.Equal(_title, result.Title);
      Assert.Equal(_pattern, result.Pattern);
      Assert.Equal(_description, result.Description);
      Assert.Equal(_authorId, result.AuthorId);
    }

    private StringContent GetValidNewExpressionJson()
    {
      var newExpression = new CreateExpressionCommand
      {
        Title = _title,
        Pattern = _pattern,
        Description = _description,
        AuthorId = _authorId
      };

      var jsonContent = new StringContent(JsonConvert.SerializeObject(newExpression), Encoding.UTF8, "application/json");

      return jsonContent;
    }
  }
}
