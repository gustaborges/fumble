FROM mcr.microsoft.com/dotnet/sdk:6.0 as build
LABEL version="1.0.0" description="Fumble Catalog Api"
COPY . /app/source
WORKDIR /app/source
RUN ["dotnet", "publish", "-o", "/app/bin", "-c", "Release"]

FROM mcr.microsoft.com/dotnet/aspnet:6.0 as final
COPY --from=build /app/bin /app
WORKDIR /app
EXPOSE 80/tcp
ENTRYPOINT ["dotnet"]
CMD ["Fumble.Catalog.Api.dll"]