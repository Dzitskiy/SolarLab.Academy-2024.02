#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Hosts/SolarLab.Academy.Api/SolarLab.Academy.Api.csproj", "Hosts/SolarLab.Academy.Api/"]
COPY ["Apllication/SolarLab.Academy.AppServices/SolarLab.Academy.AppServices.csproj", "Apllication/SolarLab.Academy.AppServices/"]
COPY ["Contracts/SolarLab.Academy.Contracts/SolarLab.Academy.Contracts.csproj", "Contracts/SolarLab.Academy.Contracts/"]
COPY ["Domain/SolarLab.Academy.Domain/SolarLab.Academy.Domain.csproj", "Domain/SolarLab.Academy.Domain/"]
COPY ["Infrastructure/SolarLab.Academy.ComponentRegistrar/SolarLab.Academy.ComponentRegistrar.csproj", "Infrastructure/SolarLab.Academy.ComponentRegistrar/"]
COPY ["Infrastructure/SolarLab.Academy.DataAccess/SolarLab.Academy.DataAccess.csproj", "Infrastructure/SolarLab.Academy.DataAccess/"]
COPY ["Infrastructure/SolarLab.Academy.Infrastructure/SolarLab.Academy.Infrastructure.csproj", "Infrastructure/SolarLab.Academy.Infrastructure/"]
RUN dotnet restore "./Hosts/SolarLab.Academy.Api/SolarLab.Academy.Api.csproj"
COPY . .
WORKDIR "/src/Hosts/SolarLab.Academy.Api"
RUN dotnet build "./SolarLab.Academy.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./SolarLab.Academy.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SolarLab.Academy.Api.dll"]