FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Hackathon.Api/Hackathon.Api.csproj", "Hackathon.Api/"]
COPY ["Hackathon.Service/Hackathon.Service.csproj", "Hackathon.Service/"]
COPY ["Hackathon.Repository/Hackathon.Repository.csproj", "Hackathon.Repository/"]
RUN dotnet restore "Hackathon.Api/Hackathon.Api.csproj"
COPY . .
WORKDIR "/src/Hackathon.Api"
RUN dotnet build "./Hackathon.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Hackathon.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Hackathon.Api.dll"]
