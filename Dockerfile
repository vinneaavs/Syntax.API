FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY . .
WORKDIR "/src/Syntax.API"
RUN dotnet restore
RUN dotnet build "Syntax.API.csproj" -c Release -o /app/build

FROM build AS publish
WORKDIR "/src/Syntax.API"
RUN dotnet publish "Syntax.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .

EXPOSE 80

ENTRYPOINT ["dotnet", "Syntax.API.dll"]