#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["src/ProvidorBackendTest.Api/ProvidorBackendTest.Api.csproj", "src/ProvidorBackendTest.Api/"]
COPY ["src/ProvidorBackendTest.Application/ProvidorBackendTest.Application.csproj", "src/ProvidorBackendTest.Application/"]
COPY ["src/ProvidorBackendTest.Domain/ProvidorBackendTest.Domain.csproj", "src/ProvidorBackendTest.Domain/"]
COPY ["src/ProvidorBackendTest.Persistance/ProvidorBackendTest.Persistance.csproj", "src/ProvidorBackendTest.Persistance/"]
RUN dotnet restore "src/ProvidorBackendTest.Api/ProvidorBackendTest.Api.csproj"
COPY . .
WORKDIR "/src/src/ProvidorBackendTest.Api"
RUN dotnet build "ProvidorBackendTest.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ProvidorBackendTest.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ProvidorBackendTest.Api.dll"]
