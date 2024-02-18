

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Fumble.Discount.Grpc/Fumble.Discount.Grpc.csproj", "Fumble.Discount.Grpc/"]
COPY ["Fumble.Discount.Database/Fumble.Discount.Database.csproj", "Fumble.Discount.Database/"]
COPY ["Fumble.Discount.Domain/Fumble.Discount.Domain.csproj", "Fumble.Discount.Domain/"]
RUN dotnet restore "./Fumble.Discount.Grpc/./Fumble.Discount.Grpc.csproj"
COPY . .
WORKDIR "/src/Fumble.Discount.Grpc"
RUN dotnet build "./Fumble.Discount.Grpc.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Fumble.Discount.Grpc.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
USER app
WORKDIR /app
EXPOSE 8080
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Fumble.Discount.Grpc.dll"]