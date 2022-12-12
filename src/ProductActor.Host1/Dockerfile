FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/ProductActor.Host1/ProductActor.Host1.csproj", "ProductActor.Host1/"]
RUN dotnet restore "src/ProductActor.Host1/ProductActor.Host1.csproj"
COPY . .
WORKDIR "/src/ProductActor.Host1"
RUN dotnet build "ProductActor.Host1.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ProductActor.Host1.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ProductActor.Host1.dll"]
