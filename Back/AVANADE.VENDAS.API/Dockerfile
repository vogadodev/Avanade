FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY AVANADE.VENDAS.API/AVANADE.VENDAS.API.csproj ./AVANADE.VENDAS.API/
COPY Referencias/AVANADE.INFRASTRUCTURE/AVANADE.INFRASTRUCTURE.csproj ./Referencias/AVANADE.INFRASTRUCTURE/
COPY Referencias/AVANADE.MODULOS/AVANADE.MODULOS.csproj ./Referencias/AVANADE.MODULOS/

RUN dotnet restore ./AVANADE.VENDAS.API/AVANADE.VENDAS.API.csproj

COPY . .

RUN dotnet build ./AVANADE.VENDAS.API/AVANADE.VENDAS.API.csproj -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish ./AVANADE.VENDAS.API/AVANADE.VENDAS.API.csproj -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish ./
ENTRYPOINT ["dotnet", "AVANADE.VENDAS.API.dll"]
