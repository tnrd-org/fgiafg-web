FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["FGIAFG.Web/FGIAFG.Web.csproj", "FGIAFG.Web/"]
RUN dotnet restore "FGIAFG.Web/FGIAFG.Web.csproj"
COPY . .
WORKDIR "/src/FGIAFG.Web"
RUN dotnet build "FGIAFG.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FGIAFG.Web.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FGIAFG.Web.dll"]
