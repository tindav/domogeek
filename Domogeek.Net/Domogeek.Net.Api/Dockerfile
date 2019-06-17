FROM mcr.microsoft.com/dotnet/core/sdk:2.2 AS publish
WORKDIR /src
COPY ["Domogeek.Net.Api.csproj", "Domogeek.Net.Api/"]
RUN dotnet restore "Domogeek.Net.Api/Domogeek.Net.Api.csproj"
COPY . .
RUN dotnet publish "Domogeek.Net.Api.csproj" -c Release -o /app

FROM mcr.microsoft.com/dotnet/core/aspnet:2.2 AS final
WORKDIR /app
EXPOSE 80
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Domogeek.Net.Api.dll"]