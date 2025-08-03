# Etapa base
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5000

# Etapa de build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiar solución y proyectos necesarios para restaurar
COPY Finanzauto.ProductsApi.sln ./
COPY Finanzauto.Products/Finanzauto.Products.csproj Finanzauto.Products/
COPY Finanzauto.Products.Application/Finanzauto.Products.Application.csproj Finanzauto.Products.Application/
COPY Finanzauto.ProductsApi.Infrastructure/Finanzauto.ProductsApi.Infrastructure.csproj Finanzauto.ProductsApi.Infrastructure/
COPY Finanzauto.ProductsApi.Domain/Finanzauto.ProductsApi.Domain.csproj Finanzauto.ProductsApi.Domain/

# Restaurar dependencias
RUN dotnet restore Finanzauto.ProductsApi.sln

# Copiar el resto del código
COPY . .

# Compilar el proyecto principal
WORKDIR /src/Finanzauto.Products
RUN dotnet build -c Release -o /app/build

# Etapa de publicación
FROM build AS publish
RUN dotnet publish -c Release -o /app/publish /p:UseAppHost=false

# Imagen final
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Finanzauto.Products.dll"]
