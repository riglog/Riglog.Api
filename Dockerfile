FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Riglog.Api/Riglog.Api.csproj", "Riglog.Api/"]
RUN dotnet restore "Riglog.Api/Riglog.Api.csproj"
COPY . .
WORKDIR "/src/Riglog.Api"
RUN dotnet build "Riglog.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Riglog.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Riglog.Api.dll"]