FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 5050

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Gamba.WebAPI/Gamba.WebAPI.csproj", "Gamba.WebAPI/"]
COPY ["Gamba.Application/Gamba.Application.csproj", "Gamba.Application/"]
COPY ["Gamba.Infrastructure/Gamba.Infrastructure.csproj", "Gamba.Infrastructure/"]
COPY ["Gamba.DataAccess/Gamba.DataAccess.csproj", "Gamba.DataAccess/"]
RUN dotnet restore "Gamba.WebAPI/Gamba.WebAPI.csproj"
COPY . .
WORKDIR "/src/Gamba.WebAPI"
RUN dotnet build "Gamba.WebAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Gamba.WebAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Gamba.WebAPI.dll"]
