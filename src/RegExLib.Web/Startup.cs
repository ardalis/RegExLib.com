using Ardalis.ListStartupServices;
using Autofac;
using RegExLib.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System.Collections.Generic;
using RegExLib.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Interfaces;
using RegExLib.Core.Constants;
using RegExLib.Core.Interfaces;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;

namespace RegExLib.Web
{
  public class Startup
  {
    private readonly IWebHostEnvironment _env;

    public Startup(IConfiguration config, IWebHostEnvironment env)
    {
      Configuration = config;
      _env = env;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
      string connectionString = Configuration.GetConnectionString("DefaultConnection");
      string identityConnectionString = Configuration.GetConnectionString("DefaultIdentityConnection");

      services.AddDbContext(connectionString);
      services.AddIdentityDbContext(identityConnectionString);

      services.AddIdentity<ApplicationUser, IdentityRole>()
              .AddDefaultUI()
              .AddEntityFrameworkStores<AppIdentityDbContext>()
              .AddDefaultTokenProviders();

      ConfigureCookieSettings(services);

      services.AddAutoMapper(typeof(Startup).Assembly);

      services.AddControllersWithViews().AddNewtonsoftJson();
      services.AddRazorPages();

      services.AddScoped<ITokenClaimsService, IdentityTokenClaimService>();

      var key = Encoding.ASCII.GetBytes(AuthorizationConstants.JWT_SECRET_KEY);
      services.AddAuthentication(config =>
        {
          config.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
          config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
          config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
          config.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(x =>
        {
          x.RequireHttpsMetadata = true;
          x.SaveToken = true;
          x.TokenValidationParameters = new TokenValidationParameters
          {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false,
            ValidateAudience = false
          };
        });

      services.AddSwaggerGen(c =>
      {
        c.SwaggerDoc("v1", new OpenApiInfo {Title = "My API", Version = "v1"});
        c.EnableAnnotations();
        c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
          Description = @"Insert your username and password to access secure api.",
          Name = "Authorization",
          In = ParameterLocation.Header,
          Type = SecuritySchemeType.OAuth2,
          Extensions = new Dictionary<string, IOpenApiExtension>
          {
            { "x-tokenName", new OpenApiString("token") }
          },
          Flows = new OpenApiOAuthFlows
          {
            Password = new OpenApiOAuthFlow
            {
              TokenUrl = new Uri("/api/authenticate", UriKind.Relative),
            }

          },
          Scheme = "Bearer"
        });

        c.AddSecurityRequirement(new OpenApiSecurityRequirement()
        {
          {
            new OpenApiSecurityScheme
            {
              Reference = new OpenApiReference
              {
                Type = ReferenceType.SecurityScheme,
                Id = "Bearer"
              },
              Scheme = "oauth2",
              Name = "Bearer",
              In = ParameterLocation.Header,

            },
            new List<string>()
          }
        });
      });


      // add list services for diagnostic purposes - see https://github.com/ardalis/AspNetCoreStartupServices
      services.Configure<ServiceConfig>(config =>
      {
        config.Services = new List<ServiceDescriptor>(services);

              // optional - default path to view services is /listallservices - recommended to choose your own path
              config.Path = "/listservices";
      });
    }

    public void ConfigureContainer(ContainerBuilder builder)
    {
      builder.RegisterModule(new DefaultInfrastructureModule(_env.EnvironmentName == "Development"));
    }

    private static void ConfigureCookieSettings(IServiceCollection services)
    {
      services.Configure<CookiePolicyOptions>(options =>
      {
              // This lambda determines whether user consent for non-essential cookies is needed for a given request.
              options.CheckConsentNeeded = context => true;
        options.MinimumSameSitePolicy = SameSiteMode.None;
      });
      services.ConfigureApplicationCookie(options =>
      {
        options.Cookie.HttpOnly = true;
        options.ExpireTimeSpan = TimeSpan.FromHours(1);
        options.LoginPath = "/Identity/Account/Login";
        options.LogoutPath = "/Identity/Account/Logout";
        options.SlidingExpiration = true;
        options.Cookie = new CookieBuilder
        {
          IsEssential = true // required for auth to work without explicit user consent; adjust to suit your privacy policy
              };
      });
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.EnvironmentName == "Development")
      {
        app.UseDeveloperExceptionPage();
        app.UseShowAllServicesMiddleware();
      }
      else
      {
        app.UseExceptionHandler("/Home/Error");
        app.UseHsts();
      }
      app.UseHttpsRedirection();
      app.UseStaticFiles();

      app.UseRouting();
      app.UseCookiePolicy();

      app.UseAuthentication();
      app.UseAuthorization();

      // Enable middleware to serve generated Swagger as a JSON endpoint.
      app.UseSwagger();

      // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
      app.UseSwaggerUI(options =>
      {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
        options.InjectStylesheet("/swagger/ui/style.css");
      });

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapDefaultControllerRoute();
        endpoints.MapRazorPages();
      });
    }
  }
}
