#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["TheShow.Core/TheShow.Core.csproj", "TheShow.Core/"]
RUN dotnet restore "TheShow.Core/TheShow.Core.csproj"
COPY . .
WORKDIR "/src/TheShow.Core"
RUN dotnet build "TheShow.Core.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TheShow.Core.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TheShow.Core.dll"]