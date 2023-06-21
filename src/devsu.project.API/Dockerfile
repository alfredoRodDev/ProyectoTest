#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/devsu.project.API/devsu.project.API.csproj", "src/devsu.project.API/"]
COPY ["src/devsu.project.Application/devsu.project.Application.csproj", "src/devsu.project.Application/"]
COPY ["src/devsu.project.Domain/devsu.project.Domain.csproj", "src/devsu.project.Domain/"]
COPY ["src/devsu.project.Infrastructure/devsu.project.Infrastructure.csproj", "src/devsu.project.Infrastructure/"]
RUN dotnet restore "src/devsu.project.API/devsu.project.API.csproj"
COPY . .
WORKDIR "/src/src/devsu.project.API"
RUN dotnet build "devsu.project.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "devsu.project.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "devsu.project.API.dll"]