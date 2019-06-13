FROM mcr.microsoft.com/dotnet/core/aspnet:2.2 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:2.2 AS build
WORKDIR /src
COPY ["Domogeek.Net.Api.csproj", "Domogeek.Net.Api/"]
RUN dotnet restore "Domogeek.Net.Api/Domogeek.Net.Api.csproj"
COPY . .
RUN dotnet build "Domogeek.Net.Api.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Domogeek.Net.Api.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Domogeek.Net.Api.dll"]