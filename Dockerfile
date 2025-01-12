# Build Stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Set the working directory for the build stage
WORKDIR /src

# Copy the .csproj files for all required projects
COPY ["src/Web/ApiServer/ApiServer.csproj", "src/Web/ApiServer/"]
COPY ["src/Application/Application.csproj", "src/Application/"]
COPY ["src/Domain/Domain.csproj", "src/Domain/"]
COPY ["src/Infrastructure/Infrastructure.csproj", "src/Infrastructure/"]

# Restore the dependencies for the ApiServer project
RUN dotnet restore "src/Web/ApiServer/ApiServer.csproj"

# Copy the entire project source code
COPY . ../

# Set the working directory to the ApiServer and build the project
WORKDIR /src/Web/ApiServer
RUN dotnet build "ApiServer.csproj" -c Release -o /app/build

# Publish Stage
FROM build AS publish

# Publish the application without restoring dependencies (since they are already restored)
RUN dotnet publish --no-restore -c Release -o /app/publish

# Runtime Stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0

# Set environment variable for ASP.NET Core HTTP port
ENV ASPNETCORE_HTTP_PORTS=5001

# Expose the application port
EXPOSE 5001

# Set the working directory for the runtime container
WORKDIR /app

# Copy the published files from the publish stage into the runtime container
COPY --from=publish /app/publish .

# Set the entrypoint to run the ApiServer application
ENTRYPOINT ["dotnet", "ApiServer.dll"]

