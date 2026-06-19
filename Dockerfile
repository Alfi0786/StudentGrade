# Build stage - MySQL compatible
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

# Copy solution and project files
COPY ["StudentGrade.sln", "./"]
COPY ["StudentGrade/StudentGrade.csproj", "StudentGrade/"]

# Restore dependencies
RUN dotnet restore "StudentGrade/StudentGrade.csproj"

# Copy everything else
COPY . .

# Build and publish
WORKDIR "/src/StudentGrade"
RUN dotnet build "StudentGrade.csproj" -c Release -o /app/build
RUN dotnet publish "StudentGrade.csproj" -c Release -o /app/publish

# Final stage
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
COPY --from=build /app/publish .

# Use shell form to read Railway's PORT variable
CMD dotnet StudentGrade.dll --urls "http://0.0.0.0:${PORT:-8080}"