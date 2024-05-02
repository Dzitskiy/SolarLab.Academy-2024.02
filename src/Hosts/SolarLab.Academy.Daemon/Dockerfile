#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
USER app
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Hosts/SolarLab.Academy.Daemon/SolarLab.Academy.Daemon.csproj", "src/Hosts/SolarLab.Academy.Daemon/"]
RUN dotnet restore "./src/Hosts/SolarLab.Academy.Daemon/SolarLab.Academy.Daemon.csproj"
COPY . .
WORKDIR "/src/src/Hosts/SolarLab.Academy.Daemon"
RUN dotnet build "./SolarLab.Academy.Daemon.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./SolarLab.Academy.Daemon.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SolarLab.Academy.Daemon.dll"]