FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /app

# Copy solution file and project file
COPY StudentGrade.sln ./
COPY StudentGrade/*.csproj ./StudentGrade/

# Restore dependencies
RUN dotnet restore

# Copy everything else and build
COPY . .
WORKDIR /app/StudentGrade
RUN dotnet publish -c Release -o /app/out

FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
COPY --from=build /app/out .

ENTRYPOINT ["dotnet", "StudentGrade.dll"]