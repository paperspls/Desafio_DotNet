FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
ENV ASPNETCORE_URLS http://*:80
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Geo_WebApi_ASP.NET.csproj", "."]
RUN dotnet restore "./Geo_WebApi_ASP.NET.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "Geo_WebApi_ASP.NET.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Geo_WebApi_ASP.NET.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Geo_WebApi_ASP.NET.dll"]