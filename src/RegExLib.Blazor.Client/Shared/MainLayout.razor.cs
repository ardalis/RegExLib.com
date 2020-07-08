using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using RegExLib.Blazor.Client.Services;

namespace RegExLib.Blazor.Client.Shared
{
  public partial class MainLayout : LayoutComponentBase
  {
    [Inject] private NavigationManager NavigationManager { get; set; }
    [Inject] protected AuthenticateService AuthenticateService { get; set; }

    protected bool IsLoggedIn
    {
      get
      {
        return AuthenticateService.IsLoggedIn;
      }
    }

    protected string UserName
    {
      get
      {
        return AuthenticateService.UserName;
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
