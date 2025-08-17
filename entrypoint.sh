#!/bin/bash
set -e

# Применяем миграции через ef (теперь он доступен)
dotnet ef database update --no-build --startup-project /app/WebApi.dll --project /app/WebApi.dll

# Запускаем приложение
exec dotnet WebApi.dll
