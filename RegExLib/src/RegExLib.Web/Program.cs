using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RegExLib.Core.Interfaces;
using RegExLib.Infrastructure;
using RegExLib.Infrastructure.Data;
using RegExLib.Infrastructure.Identity;
using System.Reflection;
using System.Text;
using Ardalis.ListStartupServices;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using RegExLib.SharedKernel.Interfaces;
using RegExLib.Web.Extensions;
using RegExLib.Web.Seeds;

const string CORS_POLICY = "CorsPolicy";

var builder = WebApplication.CreateBuilder(args);

string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
void OptionsAction(DbContextOptionsBuilder options)
{
  options.EnableSensitiveDataLogging();
  options.UseSqlServer(connectionString);
}
builder.Services.AddDbContext<AppDbContext>(OptionsAction);

builder.Services.AddDbContext<AppIdentityDbContext>(options =>
  options
    .UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddLogging();

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
  .AddDefaultUI()
  .AddEntityFrameworkStores<AppIdentityDbContext>()
  .AddDefaultTokenProviders();

// Add services to the container.
builder.Services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
builder.Services.AddScoped(typeof(IReadRepository<>), typeof(EfRepository<>));

builder.Services.AddScoped<ITokenClaimsService, IdentityTokenClaimService>();
builder.Services.AddScoped<IEmailSender, EmailSender>();

builder.Services.AddMediatR(Assembly.GetAssembly(typeof(Program))!);
builder.Services.AddAutoMapper(typeof(Program).Assembly);

builder.AddAppSettings();

builder.Services.AddMemoryCache();

builder.Services.AddCors(options =>
{
  options.AddPolicy(CORS_POLICY, builder =>
  {
    builder.AllowAnyMethod();
    builder.AllowAnyHeader();
    builder.AllowAnyOrigin();
  });
});

// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

var jwtSettings = builder.Configuration.GetJwtSettings();
var key = Encoding.UTF8.GetBytes(jwtSettings.SecretKey);
builder.Services.AddAuthentication(config =>
  {
    config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    config.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
  })
  .AddJwtBearer(options =>
  {
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters
    {
      ValidateIssuerSigningKey = true,
      IssuerSigningKey = new SymmetricSecurityKey(key),
      ValidateLifetime = true,
      ValidateIssuer = true,
      ValidateAudience = true,
      ValidAudience = jwtSettings.ValidAudience,
      ValidIssuer = jwtSettings.ValidIssuer
    };
  });

builder.Services.AddSwaggerGen(config =>
{
  config.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
  config.EnableAnnotations();
  config.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
  {
    Description = @"JWT Authorization header using the Bearer scheme.<br /> 
                      Enter 'Bearer' [space] and then your token in the text input below.<br />
                      Example: 'Bearer 12345abcdef'",
    Name = "Authorization",
    In = ParameterLocation.Header,
    Type = SecuritySchemeType.ApiKey,
    Scheme = "Bearer"
  });
  config.CustomSchemaIds(type => type.ToString());
});

// add list services for diagnostic purposes - see https://github.com/ardalis/AspNetCoreStartupServices
builder.Services.Configure<ServiceConfig>(config =>
{
  config.Services = new List<ServiceDescriptor>(builder.Services);

  // optional - default path to view services is /listallservices - recommended to choose your own path
  config.Path = "/listservices";
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.Logger.LogInformation("Adding Development middleware...");
  app.UseDeveloperExceptionPage();
  app.UseShowAllServicesMiddleware();
  app.UseWebAssemblyDebugging();
  using var scope = app.Services.CreateScope();
  try
  {
    var services = scope.ServiceProvider;

    var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
    var identityContext = services.GetRequiredService<AppIdentityDbContext>();
    await AppIdentityDbContextSeed.SeedAsync(identityContext, userManager, roleManager);
  }
  catch (Exception ex)
  {
    app.Logger.LogError(ex, "An error occurred seeding the DB.");
  }
}
else
{
  app.UseExceptionHandler("/Error");
  // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
  app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();

app.UseCors(CORS_POLICY);

// Enable middleware to serve generated Swagger as a JSON endpoint.
app.UseSwagger();

// Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1"));

using (var scope = app.Services.CreateScope())
{
  try
  {
    var services = scope.ServiceProvider;
    var logger = services.GetRequiredService<ILogger<Program>>();
    var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
    logger.LogInformation($"Current environment: {environment}");

    var context = services.GetRequiredService<AppDbContext>();
    var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
    await SeedData.PopulateInitDataAsync(context, userManager);
  }
  catch (Exception ex)
  {
    app.Logger.LogError(ex, "An error occurred seeding the DB.");
  }
}

app.UseAuthentication();
app.UseAuthorization();


app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
