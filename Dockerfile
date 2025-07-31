# 1. Берём образ SDK для сборки
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# 2. Копируем всё (как при git clone)
COPY . .

# 3. Собираем проект
RUN dotnet publish "WebApi/WebApi.csproj" -c Release -o /app/publish

# 4. Берём образ только для запуска (он легче)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# 5. Копируем собранное из прошлого образа
COPY --from=build /app/publish .
 
# 6. Команда для миграций:
RUN dotnet ef database update
# Или через runtime (если EF Core установлен в основном проекте):
# ENTRYPOINT ["dotnet", "ef", "database", "update", "--no-build", "&&", "dotnet", "YourProject.dll"]

# 6. Запускаем приложение
ENTRYPOINT ["dotnet", "WebApi.dll"]
