FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Aldaris.API/Aldaris.API.csproj", "Aldaris.API/"]
RUN dotnet restore "Aldaris.API/Aldaris.API.csproj"
COPY . .
WORKDIR "/src/Aldaris.API"
RUN dotnet build "Aldaris.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Aldaris.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Aldaris.API.dll"]
