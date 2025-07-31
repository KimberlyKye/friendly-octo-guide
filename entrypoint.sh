#!/bin/bash
set -e

# Ждем доступности БД (теперь с таймаутом)
for i in {1..30}; do
  if </dev/tcp/$DB_HOST/$DB_PORT; then
    echo "Database is ready!"
    break
  fi
  echo "Waiting for database... ($i/30)"
  sleep 2
done

# Применяем миграции
dotnet ef database update --no-build

# Запускаем приложение
exec dotnet WebApi.dll
