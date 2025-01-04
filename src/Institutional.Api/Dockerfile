##See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.
#
FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine AS base
WORKDIR /app
EXPOSE 8080

# Install cultures (same approach as Alpine SDK image)
RUN apk add --no-cache icu-libs
RUN apk add --no-cache tzdata

# Disable the invariant mode (set in base image)
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false

FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS build
WORKDIR /src
COPY ["./Directory.Packages.props", "./"]
COPY ["src/Institutional.Api/Institutional.Api.csproj", "src/Institutional.Api/"]
COPY ["src/Institutional.Application/Institutional.Application.csproj", "src/Institutional.Application/"]
COPY ["src/Institutional.Domain/Institutional.Domain.csproj", "src/Institutional.Domain/"]
COPY ["src/Institutional.Infrastructure/Institutional.Infrastructure.csproj", "src/Institutional.Infrastructure/"]
COPY . .
WORKDIR "/src/src/Institutional.Api"

FROM build AS publish
RUN dotnet publish "Institutional.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
USER $APP_UID 
ENTRYPOINT ["dotnet", "Institutional.Api.dll"]