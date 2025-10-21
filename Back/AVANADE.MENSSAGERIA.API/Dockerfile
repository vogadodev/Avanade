FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copiar projetos principais e de referência para otimizar o cache do Docker
COPY ["AVANADE.MENSSAGERIA.API/AVANADE.MENSSAGERIA.API.csproj", "./AVANADE.MENSSAGERIA.API/"]
COPY ["Referencias/AVANADE.INFRASTRUCTURE/AVANADE.INFRASTRUCTURE.csproj", "./Referencias/AVANADE.INFRASTRUCTURE/"]
COPY ["Referencias/AVANADE.MODULOS/AVANADE.MODULOS.csproj", "./Referencias/AVANADE.MODULOS/"]

RUN dotnet restore "./AVANADE.MENSSAGERIA.API/AVANADE.MENSSAGERIA.API.csproj"

# Copiar todo o código-fonte
COPY . .

WORKDIR "/src"
RUN dotnet build "AVANADE.MENSSAGERIA.API/AVANADE.MENSSAGERIA.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "AVANADE.MENSSAGERIA.API/AVANADE.MENSSAGERIA.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AVANADE.MENSSAGERIA.API.dll"]