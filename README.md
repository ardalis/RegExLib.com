# RegExLib.com
Source code for the regexlib.com regular expression library site.

[Azure Hosted Version](https://regexlibcom.azurewebsites.net/)

## Migration Scripts

Create migrations:
```powershell
dotnet ef migrations add InitialModel --context appdbcontext -p ../RegExLib.Infrastructure/RegExLib.Infrastructure.csproj -s RegExLib.Web.csproj -o Data/Migrations

dotnet ef migrations add InitialIdentityModel --context appidentitydbcontext -p ../RegExLib.Infrastructure/RegExLib.Infrastructure.csproj -s RegExLib.Web.csproj -o Identity/Migrations
```

Apply migrations:

```powershell
dotnet ef database update -c appdbcontext -p ../RegExLib.Infrastructure/RegExLib.Infrastructure.csproj -s RegExLib.Web.csproj
dotnet ef database update -c appidentitydbcontext -p ../RegExLib.Infrastructure/RegExLib.Infrastructure.csproj -s RegExLib.Web.csproj
```
