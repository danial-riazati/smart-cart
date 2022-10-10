#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["cart.services.payment_service/cart.services.payment_service.csproj", "cart.services.payment_service/"]
RUN dotnet restore "cart.services.payment_service/cart.services.payment_service.csproj"
COPY . .
WORKDIR "/src/cart.services.payment_service"
RUN dotnet build "cart.services.payment_service.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "cart.services.payment_service.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "cart.services.payment_service.dll"]