#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
USER app
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["SolarLab.Academy.DbMigrator/SolarLab.Academy.DbMigrator.csproj", "SolarLab.Academy.DbMigrator/"]
COPY ["Infrastructure/SolarLab.Academy.DataAccess/SolarLab.Academy.DataAccess.csproj", "Infrastructure/SolarLab.Academy.DataAccess/"]
COPY ["Apllication/SolarLab.Academy.AppServices/SolarLab.Academy.AppServices.csproj", "Apllication/SolarLab.Academy.AppServices/"]
COPY ["Contracts/SolarLab.Academy.Contracts/SolarLab.Academy.Contracts.csproj", "Contracts/SolarLab.Academy.Contracts/"]
COPY ["Domain/SolarLab.Academy.Domain/SolarLab.Academy.Domain.csproj", "Domain/SolarLab.Academy.Domain/"]
COPY ["Infrastructure/SolarLab.Academy.Infrastructure/SolarLab.Academy.Infrastructure.csproj", "Infrastructure/SolarLab.Academy.Infrastructure/"]
RUN dotnet restore "./SolarLab.Academy.DbMigrator/SolarLab.Academy.DbMigrator.csproj"
COPY . .
WORKDIR "/src/SolarLab.Academy.DbMigrator"
RUN dotnet build "./SolarLab.Academy.DbMigrator.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./SolarLab.Academy.DbMigrator.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SolarLab.Academy.DbMigrator.dll"]