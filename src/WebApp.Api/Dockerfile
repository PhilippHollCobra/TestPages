#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 9001

ENV ASPNETCORE_URLS=http://+:9001

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["WebApp.Api/WebApp.Api.csproj", "WebApp.Api/"]
RUN dotnet restore "WebApp.Api/WebApp.Api.csproj"
COPY . .
WORKDIR "/src/WebApp.Api"
RUN dotnet build "WebApp.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WebApp.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebApp.Api.dll"]