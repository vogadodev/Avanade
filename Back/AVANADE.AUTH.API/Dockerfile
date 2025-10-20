FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

# Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copiar projetos principais e de referência
COPY AVANADE.AUTH.API/AVANADE.AUTH.API.csproj ./AVANADE.AUTH.API/
COPY Referencias/AVANADE.INFRASTRUCTURE/AVANADE.INFRASTRUCTURE.csproj ./Referencias/AVANADE.INFRASTRUCTURE/
COPY Referencias/AVANADE.MODULOS/AVANADE.MODULOS.csproj ./Referencias/AVANADE.MODULOS/

# Restaurar dependências
RUN dotnet restore ./AVANADE.AUTH.API/AVANADE.AUTH.API.csproj

# Copiar todo o código
COPY . .

# Build
RUN dotnet build ./AVANADE.AUTH.API/AVANADE.AUTH.API.csproj -c $BUILD_CONFIGURATION -o /app/build

# Publish
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish ./AVANADE.AUTH.API/AVANADE.AUTH.API.csproj -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Final runtime
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish ./
ENTRYPOINT ["dotnet", "AVANADE.AUTH.API.dll"]
