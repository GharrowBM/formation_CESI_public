#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["06_Librarie/06_Librarie.csproj", "06_Librarie/"]
RUN dotnet restore "06_Librarie/06_Librarie.csproj"
COPY . .
WORKDIR "/src/06_Librarie"
RUN dotnet build "06_Librarie.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "06_Librarie.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "06_Librarie.dll"]