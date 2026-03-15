# Phase runtime
FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
USER $APP_UID
WORKDIR /app

# Phase build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copier le csproj et restaurer les packages
COPY ["NotificationService.csproj", "./"]
RUN dotnet restore "./NotificationService.csproj"

# Copier tout le code
COPY . .
RUN dotnet build "./NotificationService.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publier
FROM build AS publish
RUN dotnet publish "./NotificationService.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Image finale
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NotificationService.dll"]