FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["Notiffly.Api/Notiffly.Api.csproj", "Notiffly.Api/"]
COPY ["Notiffly.Business/Notiffly.Business.csproj", "Notiffly.Business/"]
COPY ["Notiffly.Data/Notiffly.Data.csproj", "Notiffly.Data/"]

RUN dotnet restore "Notiffly.Api/Notiffly.Api.csproj"

COPY . .

WORKDIR "/src/Notiffly.Api"
RUN dotnet build "Notiffly.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Notiffly.Api.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

COPY --from=publish /app/publish .

EXPOSE 5177

ENTRYPOINT ["dotnet", "Notiffly.Api.dll"]
