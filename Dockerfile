# BUILD STAGE
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /source
COPY . .
RUN dotnet publish "ClockifyCloneAPI/ClockifyCloneAPI.csproj" -c Release -o /app/publish

# RUNTIME STAGE
FROM mcr.microsoft.com/dotnet/aspnet:8.0

ARG DEBIAN_FRONTEND=noninteractive

ENV ASPNETCORE_ENVIRONMENT=Development
ENV ASPNETCORE_URLS=http://+:8045

EXPOSE 8045

WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "ClockifyCloneAPI.dll"]

