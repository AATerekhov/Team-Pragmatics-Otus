FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["01_AdventureRialto/Web/AdventureRialto.Web/AdventureRialto.Web.csproj", "01_AdventureRialto/Web/AdventureRialto.Web/"]
RUN dotnet restore "01_AdventureRialto/Web/AdventureRialto.Web/AdventureRialto.Web.csproj"
COPY . .
WORKDIR "/src/01_AdventureRialto/Web/AdventureRialto.Web"
RUN dotnet build "AdventureRialto.Web.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "AdventureRialto.Web.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AdventureRialto.Web.dll"]
