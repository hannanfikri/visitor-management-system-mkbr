#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Visitor.Web.Host/Visitor.Web.Host.csproj", "src/Visitor.Web.Host/"]
COPY ["src/Visitor.Web.Core/Visitor.Web.Core.csproj", "src/Visitor.Web.Core/"]
COPY ["src/Visitor.Application/Visitor.Application.csproj", "src/Visitor.Application/"]
COPY ["src/Visitor.Application.Shared/Visitor.Application.Shared.csproj", "src/Visitor.Application.Shared/"]
COPY ["src/Visitor.Core.Shared/Visitor.Core.Shared.csproj", "src/Visitor.Core.Shared/"]
COPY ["src/Visitor.Core/Visitor.Core.csproj", "src/Visitor.Core/"]
COPY ["src/Visitor.EntityFrameworkCore/Visitor.EntityFrameworkCore.csproj", "src/Visitor.EntityFrameworkCore/"]
COPY ["src/Visitor.GraphQL/Visitor.GraphQL.csproj", "src/Visitor.GraphQL/"]
RUN dotnet restore "src/Visitor.Web.Host/Visitor.Web.Host.csproj"
COPY . .
WORKDIR "/src/src/Visitor.Web.Host"
RUN dotnet build "Visitor.Web.Host.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Visitor.Web.Host.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Visitor.Web.Host.dll"]
