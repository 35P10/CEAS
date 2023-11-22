# Utiliza la imagen de .NET 7 SDK para construir la aplicación
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /app

# Copia el proyecto y restaura las dependencias
COPY WebApi/*.csproj WebApi/
COPY Domain.Application/*.csproj Domain.Application/
COPY Domain.Core/*.csproj Domain.Core/
COPY Infra.Integration/*.csproj Infra.Integration/

RUN dotnet restore WebApi/*.csproj

# Copia todo el código fuente y construye la aplicación
COPY . .
WORKDIR /app/WebApi
RUN dotnet publish -c Release -o out

# Utiliza la imagen de .NET 7 para ejecutar la aplicación
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app

# Copia los archivos publicados desde el contenedor de compilación
COPY --from=build /app/WebApi/out .

# Establece la variable de entorno ASPNETCORE_URLS
ENV ASPNETCORE_URLS=http://+:80
EXPOSE 80

# Ejecuta la aplicación cuando se inicie el contenedor
ENTRYPOINT ["dotnet", "WebApi.dll"]
