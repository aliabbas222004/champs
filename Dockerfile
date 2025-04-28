# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# Base image for running
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Image for building
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["trySupa.csproj", "./"]
RUN dotnet restore "./trySupa.csproj"
COPY . .
RUN dotnet build "trySupa.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Image for publishing
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "trySupa.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Final stage
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "trySupa.dll"]
