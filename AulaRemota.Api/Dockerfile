#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

ENV ASPNETCORE_URLS=https://+:8000;htpps://+:80;
ENV ASPNETCORE_ENVIRONMENT=Production

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["AulaRemota.Api/AulaRemota.Api.csproj", "AulaRemota.Api/"]
COPY ["AulaRemota.Infra/AulaRemota.Infra.csproj", "AulaRemota.Infra/"]
COPY ["AulaRemota.Shared/AulaRemota.Shared.csproj", "AulaRemota.Shared/"]
COPY ["AulaRemota.Core/AulaRemota.Core.csproj", "AulaRemota.Core/"]
RUN dotnet restore "AulaRemota.Api/AulaRemota.Api.csproj"
COPY . .
WORKDIR "/src/AulaRemota.Api"
RUN dotnet build "AulaRemota.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AulaRemota.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AulaRemota.Api.dll"]

#docker build -f Dockerfile -t image-aula-remota-api ..
#docker run --name aula-remota-web-api -p 8000:80 image-aula-remota-api ..