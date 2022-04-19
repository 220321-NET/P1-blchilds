# docker-compose up -d
# docker-compose down
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

WORKDIR /app

COPY . .

RUN dotnet clean StoreBackEnd/StoreFront.sln
RUN dotnet publish StoreBackEnd/WebAPI --configuration Release -o ./StoreBackEnd/publish

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS run

WORKDIR /app

COPY --from=build /app/StoreBackEnd/publish .

CMD ["dotnet", "WebAPI.dll"]