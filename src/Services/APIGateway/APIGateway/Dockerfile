#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src


#COPY ["APIGateway/APIGateway.csproj", "APIGateway/"]
COPY "Services/APIGateway/APIGateway/APIGateway.csproj" "Services/APIGateway/APIGateway/APIGateway.csproj"
#COPY ["APIGateway/APIGateway/APIGateway/APIGateway.csproj", "APIGateway/"]
RUN dotnet restore "Services/APIGateway/APIGateway/APIGateway.csproj"

COPY . .

WORKDIR "/src/Services/APIGateway/APIGateway"
RUN dotnet build -c Release -o /app/build
#RUN dotnet build "APIGateway.csproj" -c Release -o /app/build
#RUN dotnet build "APIGateway/APIGateway/APIGateway.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish -c Release -o /app/publish
#RUN dotnet publish "APIGateway.csproj" -c Release -o /app/publish
#RUN dotnet publish "APIGateway/APIGateway/APIGateway.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "APIGateway.dll"]