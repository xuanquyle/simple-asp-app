# ----------- Build Stage -----------
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy solution and project files for layer caching
COPY DockerComposeApi.sln ./
COPY DockerComposeApi/*.csproj ./app/
RUN dotnet restore ./app/DockerComposeApi.csproj

# Copy the full source code and build
COPY DockerComposeApi/ ./app/
WORKDIR /src/app
RUN dotnet publish DockerComposeApi.csproj -r linux-x64 -o /app/publish -c Release

# ----------- Runtime Stage -----------
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

# Expose port your app listens on
ENV ASPNETCORE_URLS=http://+:8000

# Run your app
ENTRYPOINT ["dotnet", "DockerComposeApi.dll"]
