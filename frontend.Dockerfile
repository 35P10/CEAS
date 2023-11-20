# Utiliza la imagen de .NET SDK 7.0 para construir la aplicación
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["FrontEnd/FrontEnd.csproj", "FrontEnd/"]
RUN dotnet restore "FrontEnd/FrontEnd.csproj"
COPY . .
WORKDIR "/src/FrontEnd"
RUN dotnet build "FrontEnd.csproj" -c Release -o /app/build

# Publica la aplicación
FROM build AS publish
RUN dotnet publish "FrontEnd.csproj" -c Release -o /app/publish

# Utiliza una imagen de Nginx como servidor web
FROM nginx:alpine AS final

# Copia el archivo de configuración de Nginx
COPY frontend.conf /etc/nginx/conf.d/default.conf

# Copia los archivos publicados de la aplicación Blazor
COPY --from=publish /app/publish/wwwroot /usr/share/nginx/html
