FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
USER $APP_UID
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["QueryConsoleApp/QueryConsoleApp.csproj", "QueryConsoleApp/"]
RUN dotnet restore "QueryConsoleApp/QueryConsoleApp.csproj"
COPY . .
WORKDIR "/src/QueryConsoleApp"
RUN dotnet build "QueryConsoleApp.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "QueryConsoleApp.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "QueryConsoleApp.dll"]
