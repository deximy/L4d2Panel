#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM cm2network/steamcmd AS get_l4d2_server
RUN mkdir /home/steam/l4d2_server \
    && ./steamcmd.sh +force_install_dir /home/steam/l4d2_server +login anonymous +app_update 222860 validate +quit

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /l4d2_server
COPY --from=get_l4d2_server ["/home/steam/l4d2_server", "."]
RUN dpkg --add-architecture i386 \
    && apt-get update -y \
    && apt-get install -y --no-install-recommends lib32gcc-s1 expect \
    && apt-get autoremove -y \
    && apt-get clean -y \
    && rm -rf /var/lib/apt/lists/*
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["L4d2PanelBackend.API/L4d2PanelBackend.API.csproj", "L4d2PanelBackend.API/"]
RUN dotnet restore "L4d2PanelBackend.API/L4d2PanelBackend.API.csproj"
COPY . .
WORKDIR "/src/L4d2PanelBackend.API"
RUN dotnet build "L4d2PanelBackend.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "L4d2PanelBackend.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "L4d2PanelBackend.API.dll"]