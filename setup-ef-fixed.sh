#!/bin/bash

# Script para configurar Entity Framework y la base de datos

echo "🔧 Configurando Entity Framework..."

# Agregar herramientas de .NET al PATH
export PATH="$PATH:/home/cater/.dotnet/tools"

echo "🧹 Limpiando proyecto..."
dotnet clean

echo "🗑️ Eliminando migraciones anteriores..."
rm -rf Migrations/

echo "🔨 Compilando proyecto limpio..."
dotnet build -c Release

echo "📝 Creando migraciones iniciales para PostgreSQL..."
dotnet ef migrations add InitialCreate

echo "🗃️ Aplicando migraciones a PostgreSQL..."
dotnet ef database update

echo "✅ Configuración de base de datos completada!"

echo "🚀 Iniciando aplicación..."
echo "🌐 La aplicación estará disponible en: http://34.60.233.211:8080"
echo "📋 Swagger UI: http://34.60.233.211:8080"
dotnet run --configuration Release --urls "http://0.0.0.0:8080"
