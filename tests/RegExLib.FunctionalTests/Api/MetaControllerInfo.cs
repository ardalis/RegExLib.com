﻿using RegExLib.Web;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.TestHost;
using Xunit;

namespace RegExLib.FunctionalTests.Api
{
  public class MetaControllerInfo : IClassFixture<CustomWebApplicationFactory<Startup>>
  {
    private readonly HttpClient _client;

    public MetaControllerInfo(CustomWebApplicationFactory<Startup> factory)
    {
      _client = factory
        .WithWebHostBuilder(builder => builder.UseSolutionRelativeContentRoot(""))
        .CreateClient();
    }

    [Fact]
    public async Task ReturnsVersionAndLastUpdateDate()
    {
      var response = await _client.GetAsync("/info");
      response.EnsureSuccessStatusCode();
      var stringResponse = await response.Content.ReadAsStringAsync();

      Assert.Contains("Version", stringResponse);
      Assert.Contains("Last Updated", stringResponse);
    }
  }
}
