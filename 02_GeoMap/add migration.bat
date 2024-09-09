migretions GeoMap:
dotnet ef migrations add Initial_2 --startup-project GeoMap/GeoMap.csproj --project Infrastructure.EntityFramework\Infrastructure.EntityFramework.csproj --context DatabaseContext

migretions WebApi:
dotnet ef migrations add Initial_2 --startup-project WebApi/WebApi.csproj --project Infrastructure.EntityFramework\Infrastructure.EntityFramework.csproj --context DataContext

migretions UserApi:
dotnet ef migrations add Initial_1 --startup-project UserApi/UserApi.csproj --project UserApi.DataAccess.EntityFramework\UserApi.DataAccess.EntityFramework.csproj --context DataContext

