FROM mcr.microsoft.com/dotnet/sdk:8.0 AS sdk
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS aspnet

FROM sdk AS build
COPY . ./src
RUN dotnet restore src
RUN dotnet publish -c release -o app --no-restore src

FROM  aspnet AS final
WORKDIR /app
COPY --from=build ./app .
ENTRYPOINT [ "dotnet", "Product.FrontServer.dll" ]