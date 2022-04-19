# docker build . -t blchilds55/store:0.0.1
# docker run -d -p 5000:80 blchilds55/store:0.0.1
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

WORKDIR /app

COPY . .

RUN dotnet clean StoreBackEnd/StoreFront.sln
RUN dotnet publish StoreBackEnd/WebAPI --configuration Release -o ./StoreBackEnd/publish

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS run

WORKDIR /app

COPY --from=build /app/StoreBackEnd/publish .

CMD ["dotnet", "WebAPI.dll"]

# docker run --rm -it -p 8000:80 -e Logging__Console__FormatterName="" mcr.microsoft.com/dotnet/samples:aspnetapp