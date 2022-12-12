FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/ProductActor.Host/ProductActor.Host.csproj", "ProductActor.Host/"]
RUN dotnet restore "src/ProductActor.Host/ProductActor.Host.csproj"
COPY . .
WORKDIR "/src/ProductActor.Host"
RUN dotnet build "ProductActor.Host.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ProductActor.Host.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ProductActor.Host.dll"]
