#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src

#COPY ["Data/DataService/DataService/DataService.csproj", "DataService/"]
COPY "Services/Device/Device/Device.csproj" "Services/Device/Device/Device.csproj"
#COPY ["Data/DataService/DataService/DataService.csproj", "DataService/"]
RUN dotnet restore "Services/Device/Device/Device.csproj"

COPY . .

WORKDIR "/src/Services/Device/Device"
RUN dotnet build -c Release -o /app/build

FROM build AS publish
RUN dotnet publish -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Device.dll"]