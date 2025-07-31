#!/bin/bash
set -e

# Применяем миграции (Render сам подставит переменные окружения)
dotnet ef database update --no-build

# Запускаем приложение
exec dotnet WebApi.dll
