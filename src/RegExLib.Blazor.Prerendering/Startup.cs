using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Net.Http;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using RegExLib.Blazor.Client.Services;

namespace RegExLib.Blazor.Prerendering
{
  public class Startup
  {
    // This method gets called by the runtime. Use this method to add services to the container.
    // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddControllers();
      services.AddRazorPages();

      services.AddScoped<HttpClient>(s =>
      {
        var navigationManager = s.GetRequiredService<NavigationManager>();
        return new HttpClient
        {
          BaseAddress = new Uri(navigationManager.BaseUri)
        };
      });

      services.AddBlazoredLocalStorage();
      services.AddScoped<AuthenticateService>();

    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
        app.UseWebAssemblyDebugging();
      }

      app.UseHttpsRedirection();
      app.UseBlazorFrameworkFiles();
      app.UseStaticFiles();

      app.UseRouting();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapDefaultControllerRoute();
        endpoints.MapFallbackToPage("/_Host");
      });
    }
  }
}
