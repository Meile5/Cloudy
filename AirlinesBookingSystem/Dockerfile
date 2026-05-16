FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["AirlinesBookingSystem/AirlinesBookingSystem.csproj", "AirlinesBookingSystem/"]
COPY ["Orchestrator/Orchestrator.csproj", "Orchestrator/"]
RUN dotnet restore "AirlinesBookingSystem/AirlinesBookingSystem.csproj"
COPY . .
WORKDIR "/src/AirlinesBookingSystem"
RUN dotnet build "./AirlinesBookingSystem.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./AirlinesBookingSystem.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false /p:ErrorOnDuplicatePublishOutputFiles=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AirlinesBookingSystem.dll"]