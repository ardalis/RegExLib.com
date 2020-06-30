using RegExLib.Core.Entities;
using RegExLib.Web;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using RegExLib.Web.Seeds;
using Xunit;

namespace RegExLib.FunctionalTests.Api.expressions
{
  public class ApiExpressionsControllerList : IClassFixture<CustomWebApplicationFactory<Startup>>
  {
    private readonly HttpClient _client;

    public ApiExpressionsControllerList(CustomWebApplicationFactory<Startup> factory)
    {
      _client = factory.CreateClient();
    }

    [Fact]
    public async Task ReturnsOneExpressions()
    {
      var response = await _client.GetAsync("/api/expressions");
      response.EnsureSuccessStatusCode();
      var stringResponse = await response.Content.ReadAsStringAsync();
      var result = JsonConvert.DeserializeObject<IEnumerable<ToDoItem>>(stringResponse).ToList();

      Assert.Single(result);
      Assert.Contains(result, i => i.Title == ExpressionsSeed.Expression1.Title);
    }

    [Fact]
    public async Task ReturnsPageNumberOneExpressions()
    {
      var response = await _client.GetAsync("/api/expressions?page=0");
      response.EnsureSuccessStatusCode();
      var stringResponse = await response.Content.ReadAsStringAsync();
      var result = JsonConvert.DeserializeObject<IEnumerable<Expression>>(stringResponse).ToList();

      Assert.Single(result);
      Assert.Contains(result, i => i.Title == ExpressionsSeed.Expression1.Title);
    }
  }
}
