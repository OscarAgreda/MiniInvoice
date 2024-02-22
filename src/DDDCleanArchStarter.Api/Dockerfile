#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.
# From solution root:
# docker build -t front-desk --progress=plain --no-cache -f .\DDDCleanArchStarter\src\DDDCleanArchStarter.Api\Dockerfile .
FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443
FROM mcr.microsoft.com/dotnet/sdk:5.0.201-buster-slim AS build
WORKDIR /app
# run this from repository root
COPY ./ ./ 
#RUN ls -lha .
RUN echo 'Building DDDCleanArchStarter container'
WORKDIR /app/DDDCleanArchStarter/src/DDDCleanArchStarter.Api
#RUN ls -lha .
RUN dotnet restore
RUN dotnet build "DDDCleanArchStarter.Api.csproj" -c Release -o /app/build
FROM build AS publish
RUN dotnet publish "DDDCleanArchStarter.Api.csproj" -c Release -o /app/publish
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DDDCleanArchStarter.Api.dll"]
