FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
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

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Define a porta que será exposta para acesso externo
EXPOSE 80

# Inicie o contêiner de autenticação primeiro
CMD ["dotnet", "Syntax.Auth.dll"]
# Em seguida, inicie o contêiner de aplicativo
CMD ["dotnet", "Syntax.Application.dll"]
# Finalmente, inicie o contêiner da API
CMD ["dotnet", "Syntax.API.dll"]
