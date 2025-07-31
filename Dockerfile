# Используем SDK для сборки + миграций
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY . .

# Устанавливаем dotnet-ef
RUN dotnet tool install --global dotnet-ef

# Собираем и применяем миграции
RUN dotnet publish "WebApi/WebApi.csproj" -c Release -o /app/publish
RUN dotnet ef database update --project "WebApi/WebApi.csproj" --startup-project "WebApi/WebApi.csproj"

# Итоговый образ для запуска
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "WebApi.dll"]
