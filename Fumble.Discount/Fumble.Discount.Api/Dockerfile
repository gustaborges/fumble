FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Fumble.Discount.Api/Fumble.Discount.Api.csproj", "Fumble.Discount.Api/"]
RUN dotnet restore "./Fumble.Discount.Api/./Fumble.Discount.Api.csproj"
COPY . .
WORKDIR "/src/Fumble.Discount.Api"
RUN dotnet build "./Fumble.Discount.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Fumble.Discount.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
EXPOSE 8080
EXPOSE 8081
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Fumble.Discount.Api.dll"]