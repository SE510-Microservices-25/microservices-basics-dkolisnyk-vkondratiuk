FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["Subscriptions.Api/Subscriptions.Api.csproj", "Subscriptions.Api/"]
COPY ["Subscriptions.Business/Subscriptions.Business.csproj", "Subscriptions.Business/"]
COPY ["Subscriptions.Data/Subscriptions.Data.csproj", "Subscriptions.Data/"]

RUN dotnet restore "Subscriptions.Api/Subscriptions.Api.csproj"

COPY . .

WORKDIR "/src/Subscriptions.Api"
RUN dotnet build "Subscriptions.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Subscriptions.Api.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .

EXPOSE 5177

ENTRYPOINT ["dotnet", "Subscriptions.Api.dll"]
