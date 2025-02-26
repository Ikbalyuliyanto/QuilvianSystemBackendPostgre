FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /app

# Copy semua file dan restore dependencies
COPY *.csproj ./
RUN dotnet restore

# Copy semua kode sumber
COPY . ./
RUN dotnet publish -c Release -o out

# Gunakan ASP.NET Runtime untuk menjalankan aplikasi
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build-env /app/out .

ENTRYPOINT ["dotnet", "QuilvianSystemBackendDev.dll"]
