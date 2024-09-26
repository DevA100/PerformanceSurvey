# Use the official .NET SDK image as the base image for building
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy the project file and restore dependencies
COPY ["PerformanceSurvey.csproj", "./"]
RUN dotnet restore "PerformanceSurvey.csproj"

# Copy the rest of the source code
COPY . .

# Build the application
RUN dotnet build "PerformanceSurvey.csproj" -c Release -o /app/build

# Publish the application
FROM build AS publish
RUN dotnet publish "PerformanceSurvey.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Build the runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
EXPOSE 8080
ENTRYPOINT ["dotnet", "PerformanceSurvey.dll"]