FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["Notifications.Api/Notifications.Api.csproj", "Notifications.Api/"]
COPY ["Notifications.Business/Notifications.Business.csproj", "Notifications.Business/"]
COPY ["Notifications.Data/Notifications.Data.csproj", "Notifications.Data/"]

RUN dotnet restore "Notifications.Api/Notifications.Api.csproj"

COPY . .
WORKDIR "/src/Notifications.Api"
RUN dotnet build "Notifications.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Notifications.Api.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .

EXPOSE 5179
ENTRYPOINT ["dotnet", "Notifications.Api.dll"]
