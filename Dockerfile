FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["src/AgroApp.API/AgroApp.API.csproj", "src/AgroApp.API/"]
COPY ["src/AgroApp.Application/AgroApp.Application.csproj", "src/AgroApp.Application/"]
COPY ["src/AgroApp.Domain/AgroApp.Domain.csproj", "src/AgroApp.Domain/"]
COPY ["src/AgroApp.Infrastructure/AgroApp.Infrastructure.csproj", "src/AgroApp.Infrastructure/"]

RUN dotnet restore "src/AgroApp.API/AgroApp.API.csproj"

COPY . .
WORKDIR "/src/src/AgroApp.API"
RUN dotnet build "AgroApp.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AgroApp.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AgroApp.API.dll"]