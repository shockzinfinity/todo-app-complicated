FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
# FROM microsoft/aspnetcore-build:2.0.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
# FROM microsoft/aspnetcore:2.0.0 AS build
WORKDIR /src
COPY ["todoCore3.Api.csproj", "./"]
# RUN ulimit -n 8192
# COPY ["nuget.config", "./"]
RUN dotnet restore "./todoCore3.Api.csproj"
# RUN dotnet restore -v d "./todoCore3.Api.csproj"
# RUN dotnet restore --disable-parallel "./todoCore3.Api.csproj"
# RUN dotnet restore --configfile ./nuget.config "./todoCore3.Api.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "todoCore3.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "todoCore3.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV ASPNETCORE_URLS http://*:5000
ENTRYPOINT ["dotnet", "todoCore3.Api.dll"]