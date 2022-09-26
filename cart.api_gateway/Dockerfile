#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["cart.api_gateway/cart.api_gateway.csproj", "cart.api_gateway/"]
RUN dotnet restore "cart.api_gateway/cart.api_gateway.csproj"
COPY . .
WORKDIR "/src/cart.api_gateway"
RUN dotnet build "cart.api_gateway.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "cart.api_gateway.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "cart.api_gateway.dll"]