#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["TheShow.Admin/TheShow.Admin.csproj", "TheShow.Admin/"]
RUN dotnet restore "TheShow.Admin/TheShow.Admin.csproj"
COPY . .
WORKDIR "/src/TheShow.Admin"
RUN dotnet build "TheShow.Admin.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TheShow.Admin.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TheShow.Admin.dll"]