FROM mcr.microsoft.com/dotnet/sdk:10.0-preview AS build
WORKDIR /app

# 1. Copia todo el contenido primero para encontrar las subcarpetas
COPY . ./

# 2. Busca el archivo .csproj recursivamente y restaura
# Si sabes el nombre de la carpeta, puedes usar: RUN dotnet restore "SistemaAlmacen/SistemaAlmacen.csproj"
RUN dotnet restore

# 3. Publica el proyecto
RUN dotnet publish -c Release -o out

# Etapa de ejecución
FROM mcr.microsoft.com/dotnet/aspnet:10.0-preview
WORKDIR /app
COPY --from=build /app/out .

ENV ASPNETCORE_URLS=http://+:10000
EXPOSE 10000

# Asegúrate de que el nombre del .dll sea el correcto
ENTRYPOINT ["dotnet", "SistemaAlmacen.dll"]