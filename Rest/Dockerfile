FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base

ENV ASPNETCORE_ENVIRONMENT=Development

WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Rest/Rest.csproj", "Rest/"]
RUN dotnet restore "Rest/Rest.csproj"
COPY . .
WORKDIR "/src/Rest"
RUN dotnet build "Rest.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Rest.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Rest.dll"]