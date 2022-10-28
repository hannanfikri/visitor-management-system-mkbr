#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Visitor.Migrator/Visitor.Migrator.csproj", "src/Visitor.Migrator/"]
COPY ["src/Visitor.EntityFrameworkCore/Visitor.EntityFrameworkCore.csproj", "src/Visitor.EntityFrameworkCore/"]
COPY ["src/Visitor.Core/Visitor.Core.csproj", "src/Visitor.Core/"]
COPY ["src/Visitor.Core.Shared/Visitor.Core.Shared.csproj", "src/Visitor.Core.Shared/"]
RUN dotnet restore "src/Visitor.Migrator/Visitor.Migrator.csproj"
COPY . .
WORKDIR "/src/src/Visitor.Migrator"
RUN dotnet build "Visitor.Migrator.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Visitor.Migrator.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Visitor.Migrator.dll", "-s"]
