# Используем SDK для сборки + миграций
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY . .

# Устанавливаем dotnet-ef и добавляем в PATH
RUN dotnet tool install --global dotnet-ef
ENV PATH="$PATH:/root/.dotnet/tools"

# Восстанавливаем зависимости
RUN dotnet restore "WebApi/WebApi.csproj"

# Применяем миграции (теперь dotnet-ef доступен)
RUN dotnet ef database update --project "WebApi/WebApi.csproj" --startup-project "WebApi/WebApi.csproj"

# Собираем приложение
RUN dotnet publish "WebApi/WebApi.csproj" -c Release -o /app/publish

# Итоговый образ для запуска
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "WebApi.dll"]
