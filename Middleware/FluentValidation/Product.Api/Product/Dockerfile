#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base

RUN apt-get update && \
    apt-get install -y -q curl gnupg2
RUN curl http://nginx.org/keys/nginx_signing.key | apt-key add -

RUN apt-get install -y -q nginx
RUN apt-get install -y -q dnsutils


COPY ["consul/","consul/"]
COPY ["consul/consul", "/usr/local/bin/"]
COPY ["nginx/default","etc/nginx/sites-enabled/"]

RUN chmod 777 /usr/local/bin/consul

WORKDIR /app
EXPOSE 80 9018

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Api/Product.Api.csproj", "Api/"]
COPY ["Services/Services.csproj", "Services/"]
COPY ["Domain/Domain.csproj", "Domain/"]
COPY ["Infrastructure/Infrastructure.csproj", "Infrastructure/"]
COPY ["nuget.config", "Domain/"]
COPY ["nuget.config", "Infrastructure/"]
COPY ["nuget.config", "Services/"]
COPY ["nuget.config", "Api/"]


RUN dotnet restore "Api/Product.Api.csproj"
COPY . .
WORKDIR "/src/Api"
RUN dotnet build "Product.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Product.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENV DOTNET_USE_POLLING_FILE_WATCHER=true  
ENV ASPNETCORE_URLS=http://+:9018

ENTRYPOINT ["dotnet", "Product.Api.dll"]