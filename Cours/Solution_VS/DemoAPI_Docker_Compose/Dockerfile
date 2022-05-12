#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["DemoAPI_Docker_Compose/DemoAPI_Docker_Compose.csproj", "DemoAPI_Docker_Compose/"]
RUN dotnet restore "DemoAPI_Docker_Compose/DemoAPI_Docker_Compose.csproj"
COPY . .
WORKDIR "/src/DemoAPI_Docker_Compose"
RUN dotnet build "DemoAPI_Docker_Compose.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DemoAPI_Docker_Compose.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DemoAPI_Docker_Compose.dll"]
