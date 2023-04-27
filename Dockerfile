FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY . .
WORKDIR "/src/Syntax.API"
RUN dotnet build "Syntax.API.csproj" -c Release -o /app/build

WORKDIR "/src/Syntax.Application"
RUN dotnet build "Syntax.Application.csproj" -c Release -o /app/build

WORKDIR "/src/Syntax.Auth"
RUN dotnet build "Syntax.Auth.csproj" -c Release -o /app/build

FROM build AS publish
WORKDIR "/src/Syntax.API"
RUN dotnet publish "Syntax.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

WORKDIR "/src/Syntax.Application"
RUN dotnet publish "Syntax.Application.csproj" -c Release -o /app/publish /p:UseAppHost=false

WORKDIR "/src/Syntax.Auth"
RUN dotnet publish "Syntax.Auth.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .

EXPOSE 80

ENTRYPOINT ["dotnet", "Syntax.API.dll"]