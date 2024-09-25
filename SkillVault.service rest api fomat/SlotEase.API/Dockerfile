FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Levr.API/Levr.API.csproj", "Levr.API/"]
COPY ["Levr.Application/Levr.Application.csproj", "Levr.Application/"]
COPY ["Levr.Domain/Levr.Domain.csproj", "Levr.Domain/"]
COPY ["Levr.Infrastructure/Levr.Infrastructure.csproj", "Levr.Infrastructure/"]
RUN pwd && ls -la
RUN dotnet restore "Levr.API/Levr.API.csproj"
COPY . .
WORKDIR "/src/Levr.API"
RUN dotnet build "Levr.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Levr.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Levr.API.dll"]