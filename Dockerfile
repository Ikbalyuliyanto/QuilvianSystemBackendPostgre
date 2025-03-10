# Use the official .NET SDK image kang di hapus yak
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

# Set the working directory inside the container
WORKDIR /app

# Copy the main project file
COPY QuilvianSystemBackend.csproj ./

# Restore dependencies
RUN dotnet restore QuilvianSystemBackend.csproj

# Copy the rest of the application code
COPY . ./

# Publish the app to a directory in the container
RUN dotnet publish -c Release -o out

# Use the official ASP.NET image for runtime
FROM mcr.microsoft.com/dotnet/aspnet:6.0

# Set the working directory inside the container
WORKDIR /app

# Copy the published app from the previous image
COPY --from=build /app/out .

# Set the entry point to start the application
ENTRYPOINT ["dotnet", "QuilvianSystemBackend.dll"]
