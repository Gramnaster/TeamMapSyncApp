# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# ── React / Vite / TanStack frontend ────────────────────────────────────────
FROM node:22-alpine AS react-frontend
ENV PNPM_HOME="/pnpm"
ENV PATH="$PNPM_HOME:$PATH"
RUN corepack enable && corepack prepare pnpm@latest --activate
WORKDIR /app/client
# Copy lockfile and manifest first for maximum layer-cache reuse
COPY client/package.json client/pnpm-lock.yaml ./
RUN pnpm fetch
RUN pnpm install --frozen-lockfile --offline
COPY client/ .
RUN pnpm run build
# Output lands in /app/client/dist

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["api/TeamMapSyncApi/TeamMapSyncApi.csproj", "api/TeamMapSyncApi/"]
COPY ["Directory.Build.props", "."]
RUN dotnet restore "api/TeamMapSyncApi/TeamMapSyncApi.csproj"
COPY api/ api/
WORKDIR "/src/api/TeamMapSyncApi"
RUN dotnet build "TeamMapSyncApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "TeamMapSyncApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
COPY --from=react-frontend /app/client/dist ./wwwroot
ENTRYPOINT ["dotnet", "TeamMapSyncApi.dll"]