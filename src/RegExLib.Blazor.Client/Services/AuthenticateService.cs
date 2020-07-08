using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using Newtonsoft.Json;
using RegExLib.Blazor.Client.Constants;

namespace RegExLib.Blazor.Client.Services
{
  public class AuthenticateService
  {
    private readonly HttpClient _httpClient;
    private readonly ILocalStorageService _localStorage;
    public static bool IsLoggedIn { get; set; }
    public static string UserName { get; set; }

    public AuthenticateService(HttpClient httpClient, ILocalStorageService localStorage)
    {
      _httpClient = httpClient;
      _localStorage = localStorage;
    }

    public async Task Login(User user)
    {
      var response = await _httpClient.PostAsync($"{GeneralConstants.API_URL}authenticate", new MultipartFormDataContent
      {
        {new StringContent(user.Username), "\"Username\""},
        {new StringContent(user.Password), "\"Password\""},

      });

      if (response.IsSuccessStatusCode)
      {
        await SaveToken(response);
        await SetAuthorizationHeader();

        UserName = user.Username;
        IsLoggedIn = true;
      }
    }

    public async Task Logout()
    {
      await _localStorage.RemoveItemAsync("authToken");
      UserName = null;
      IsLoggedIn = false;
    }

    private async Task SaveToken(HttpResponseMessage response)
    {
      var responseContent = await response.Content.ReadAsStringAsync();
      var jwt = JsonConvert.DeserializeObject<AuthenticateResponse>(responseContent);

      await _localStorage.SetItemAsync("authToken", jwt.Token);
    }

    public async Task<string> GetToken()
    {

      var token = await _localStorage.GetItemAsync<string>("authToken");
      return token;
    }

    private async Task SetAuthorizationHeader()
    {
      if (!_httpClient.DefaultRequestHeaders.Contains("Authorization"))
      {
        var token = await GetToken();
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
      }
    }

    private IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
    {
      var claims = new List<Claim>();
      var payload = jwt.Split('.')[1];
      var jsonBytes = ParseBase64WithoutPadding(payload);
      var keyValuePairs = JsonConvert.DeserializeObject<Dictionary<string, object>>(Encoding.UTF8.GetString(jsonBytes));

      keyValuePairs.TryGetValue(ClaimTypes.Role, out object roles);

      if (roles != null)
      {
        if (roles.ToString().Trim().StartsWith("["))
        {
          var parsedRoles = JsonConvert.DeserializeObject<string[]>(roles.ToString());

          foreach (var parsedRole in parsedRoles)
          {
            claims.Add(new Claim(ClaimTypes.Role, parsedRole));
          }
        }
        else
        {
          claims.Add(new Claim(ClaimTypes.Role, roles.ToString()));
        }

        keyValuePairs.Remove(ClaimTypes.Role);
      }

      claims.AddRange(keyValuePairs.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString())));

      return claims;
    }

    private byte[] ParseBase64WithoutPadding(string base64)
    {
      switch (base64.Length % 4)
      {
        case 2: base64 += "=="; break;
        case 3: base64 += "="; break;
      }
      return Convert.FromBase64String(base64);
    }
  }
}
