#!/bin/bash
set -e

# Ждём доступности БД (актуально для Docker Compose)
while ! nc -z $DB_HOST $DB_PORT; do
  sleep 1
done

# Применяем миграции
dotnet ef database update --no-build

# Запускаем приложение
exec dotnet WebApi.dll
