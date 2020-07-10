using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using RegExLib.Blazor.Client.Services;

namespace RegExLib.Blazor.Client.Pages
{
  public partial class Login
  {
    [Inject] private AuthenticateService AuthenticateService { get; set; }
    [Inject] private NavigationManager NavigationManager { get; set; }

    protected User User { get; set; } = new User();
    protected bool ShowLoginFailed { get; set; }

    protected async Task LoginUser()
    {
      await AuthenticateService.Login(User);

      if (AuthenticateService.IsLoggedIn)
      {
        NavigationManager.NavigateTo("/");
      }
      else
      {
        ShowLoginFailed = true;
      }
    }
  }
}
