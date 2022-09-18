using RegExLib.BlazorShared;
using RegExLib.BlazorShared.Dtos;
using System.Text.Json;

namespace RegExLib.Client.Services;
public class ExpressionService
{
  private readonly HttpClient _httpClient;

  public ExpressionService(HttpClient httpClient)
  {
    _httpClient = httpClient;
  }

  public async Task<Result> ListAsync()
  {
    var response = await _httpClient.GetAsync("expressions");
    var jsonString = await response.Content.ReadAsStringAsync();

    return Result.DeserializeJson<Result>(jsonString);
  }
  
  public async Task<Result> AddAsync(AddExpressionRequest addExpressionRequest)
  {
    var jsonText = JsonSerializer.Serialize(addExpressionRequest);
    var httpContent = new StringContent(jsonText, System.Text.Encoding.UTF8, "application/json");
    var response = await _httpClient.PostAsync("expressions", httpContent);
    var jsonString = await response.Content.ReadAsStringAsync();

    return Result.DeserializeJson<Result>(jsonString);
  }
}
