FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build-env
WORKDIR /app

COPY . .
RUN dotnet publish ./src/BandAccountManager.BlazorApp/Server/BandAccountManager.BlazorApp.Server.csproj -c Release -o dist

FROM mcr.microsoft.com/dotnet/aspnet:5.0
WORKDIR /app
COPY --from=build-env /app/dist .
ENTRYPOINT ["dotnet", "BandAccountManager.BlazorApp.Server.dll"]