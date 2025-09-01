# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy the project file(s) and restore dependencies
COPY *.csproj ./
RUN dotnet restore

# Copy all source files
COPY . ./

# Publish the app
RUN dotnet publish -c Release -o /app/publish /p:UseAppHost=false

# Run stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080
ENTRYPOINT ["dotnet","StudentApi.dll"]

