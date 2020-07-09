using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using RegExLib.Blazor.Client.Services;

namespace RegExLib.Blazor.Client.Shared
{
  public partial class MainLayout : LayoutComponentBase
  {
    [Inject] private NavigationManager NavigationManager { get; set; }
    [Inject] protected AuthenticateService AuthenticateService { get; set; }

    protected bool IsLoggedIn => AuthenticateService.IsLoggedIn;
    protected string UserName => AuthenticateService.UserName;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
      if (firstRender)
      {
        await AuthenticateService.RefreshLoginInfo();
        this.StateHasChanged();
      }
    }

    protected async Task LogoutClick()
    {
      await AuthenticateService.Logout();
      NavigationManager.NavigateTo("Login");
    }

    protected void LoginClick()
    {
      NavigationManager.NavigateTo("Login");
    }

    protected void ProfileClick()
    {
      NavigationManager.NavigateTo("Profile");
    }
  }
}
