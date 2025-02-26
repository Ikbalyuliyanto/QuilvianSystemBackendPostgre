# Menggunakan image SDK .NET Core untuk membangun aplikasi
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app
COPY . . 
RUN dotnet restore
RUN dotnet publish -c Release -o out

# Menggunakan image runtime untuk menjalankan aplikasi
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build /app/out . 
ENTRYPOINT ["dotnet", "QuilvianSystemBackend.dll"]
