FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /CareerSharp

COPY . ./

RUN dotnet restore

RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /CareerSharp
COPY --from=build-env /CareerSharp/out .
ENTRYPOINT ["dotnet", "CareerSharp.dll"]