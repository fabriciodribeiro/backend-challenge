FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
#ENV ASPNETCORE_URLS=https://+:5001;http://+:5000
#ENV ASPNETCORE_URLS=http://*:5000
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["src/Challenge.Core/Challenge.Core.csproj", "src/Challenge.Core/"]
COPY ["src/Challenge.Application/Challenge.Application.csproj", "src/Challenge.Application/"]
COPY ["src/Challenge.Infrastructure/Challenge.Infrastructure.csproj", "src/Challenge.Infrastructure/"]
COPY ["src/Challenge.API/Challenge.API.csproj", "src/Challenge.API/"]
RUN dotnet restore "src/Challenge.API/Challenge.API.csproj"
COPY . .
WORKDIR "/src/src/Challenge.API"
RUN dotnet build "Challenge.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Challenge.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Challenge.API.dll"]