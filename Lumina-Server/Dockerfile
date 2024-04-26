# Utiliza la imagen oficial de ASP.NET Core para .NET 8.0
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app

# Etapa de compilación y publicación
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copia el archivo de proyecto y restaura las dependencias
COPY ["Lumina-Backend/Lumina-Backend.csproj", "Lumina-Backend/"]
RUN dotnet restore "Lumina-Backend/Lumina-Backend.csproj"

# Copia la aplicación de backend al contenedor y realiza la compilación
COPY . .
WORKDIR "/src/Lumina-Backend"
RUN dotnet build "Lumina-Backend.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Etapa de publicación
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Lumina-Backend.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Etapa final
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Establece el punto de entrada para ejecutar la aplicación
ENTRYPOINT ["dotnet", "Lumina-Backend.dll"]