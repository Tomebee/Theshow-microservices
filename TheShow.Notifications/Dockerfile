#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["TheShow.Notifications/TheShow.Notifications.csproj", "TheShow.Notifications/"]
RUN dotnet restore "TheShow.Notifications/TheShow.Notifications.csproj"
COPY . .
WORKDIR "/src/TheShow.Notifications"
RUN dotnet build "TheShow.Notifications.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TheShow.Notifications.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TheShow.Notifications.dll"]