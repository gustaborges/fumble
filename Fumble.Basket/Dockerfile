FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
COPY . /src
WORKDIR "/src/Fumble.Basket.Api"
RUN dotnet build "./Fumble.Basket.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Fumble.Basket.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Fumble.Basket.Api.dll"]