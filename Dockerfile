# Fase base para la imagen final
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Fase de construcción
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copia el archivo .csproj y restaura las dependencias
COPY ["ApiPostgress.csproj", "./"]
RUN dotnet restore "ApiPostgress.csproj"

# Copia el resto del proyecto
COPY . .
WORKDIR "/src"
RUN dotnet build "ApiPostgress.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Fase de publicación
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "ApiPostgress.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Fase final
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ApiPostgress.dll"]
