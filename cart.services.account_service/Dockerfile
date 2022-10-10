#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["cart.services.account_service/cart.services.account_service.csproj", "cart.services.account_service/"]
RUN dotnet restore "cart.services.account_service/cart.services.account_service.csproj"
COPY . .
WORKDIR "/src/cart.services.account_service"
RUN dotnet build "cart.services.account_service.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "cart.services.account_service.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "cart.services.account_service.dll"]