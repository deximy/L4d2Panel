#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["L4d2PanelBackend.Market.API/L4d2PanelBackend.Market.API.csproj", "L4d2PanelBackend.Market.API/"]
RUN dotnet restore "L4d2PanelBackend.Market.API/L4d2PanelBackend.Market.API.csproj"
COPY . .
WORKDIR "/src/L4d2PanelBackend.Market.API"
RUN dotnet build "L4d2PanelBackend.Market.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "L4d2PanelBackend.Market.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "L4d2PanelBackend.Market.API.dll"]