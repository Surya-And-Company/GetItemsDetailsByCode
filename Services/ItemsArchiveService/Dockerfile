FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /GetItemsDetailsByCode

COPY ["Services/ItemsArchiveService/ItemsArchiveService.csproj", "Services/ItemsArchiveService/"]
RUN dotnet restore "Services/ItemsArchiveService/ItemsArchiveService.csproj"
COPY . .
WORKDIR "/GetItemsDetailsByCode/Services/ItemsArchiveService"
RUN dotnet build "ItemsArchiveService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ItemsArchiveService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ItemsArchiveService.dll"]
