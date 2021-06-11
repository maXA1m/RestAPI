#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["NetCoreApi/NetCoreApi.csproj", "NetCoreApi/"]
COPY ["NetCoreApi.Queries/NetCoreApi.Queries.csproj", "NetCoreApi.Queries/"]
COPY ["NetCoreApi.Data.Model/NetCoreApi.Data.Model.csproj", "NetCoreApi.Data.Model/"]
COPY ["NetCoreApi.Data.Access/NetCoreApi.Data.Access.csproj", "NetCoreApi.Data.Access/"]
COPY ["NetCoreApi.Model/NetCoreApi.Model.csproj", "NetCoreApi.Model/"]
COPY ["NetCoreApi.Common/NetCoreApi.Common.csproj", "NetCoreApi.Common/"]
COPY ["NetCoreApi.Security/NetCoreApi.Security.csproj", "NetCoreApi.Security/"]
COPY ["NetCoreApi.Cqrs/NetCoreApi.Cqrs.csproj", "NetCoreApi.Cqrs/"]
RUN dotnet restore "NetCoreApi/NetCoreApi.csproj"
COPY . .
WORKDIR "/src/NetCoreApi"
RUN dotnet build "NetCoreApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "NetCoreApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NetCoreApi.dll"]
