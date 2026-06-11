FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
FROM mcr.microsoft.com/dotnet/core/sdk:3.1

COPY /bin/Release/netcoreapp3.1/publish/ SportsStore/

ENV ASPNETCORE_ENVIRONMENT Production

EXPOSE 5001
WORKDIR /SportsStore
ENTRYPOINT ["dotnet", "SportsStore.dll",  "--urls=http://sportsstore.preprod.edenred.net:5001"]
