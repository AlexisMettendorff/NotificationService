# Phase runtime
FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
USER $APP_UID
WORKDIR /app

# Phase build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copier le csproj et restaurer les packages
COPY ["Producer.csproj", "./"]
RUN dotnet restore "./Producer.csproj"

# Copier tout le code
COPY . .
RUN dotnet build "./Producer.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publier
FROM build AS publish
RUN dotnet publish "./Producer.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Image finale
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Producer.dll"]