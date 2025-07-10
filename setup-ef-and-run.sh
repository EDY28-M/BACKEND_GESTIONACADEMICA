#!/bin/bash

# Script para configurar Entity Framework y la base de datos

echo "🔧 Configurando Entity Framework..."

# Agregar herramientas de .NET al PATH
export PATH="$PATH:/home/cater/.dotnet/tools"

echo "🧹 Limpiando proyecto anterior..."
dotnet clean

echo "🗑️ Eliminando migraciones anteriores..."
rm -rf Migrations/

echo "🗑️ Eliminando binarios anteriores..."
rm -rf bin/ obj/

echo "🔨 Restaurando paquetes NuGet..."
dotnet restore

echo "📝 Creando migraciones iniciales para PostgreSQL..."
dotnet ef migrations add InitialCreate Script para configurar Entity Framework y la base de datos

echo "🔧 Configurando Entity Framework..."

# Agregar herramientas de .NET al PATH
export PATH="$PATH:/home/cater/.dotnet/tools"

echo "� Limpiando migraciones anteriores..."
rm -rf Migrations/

echo "�📝 Creando migraciones iniciales..."
dotnet ef migrations add InitialCreate

echo "🗃️ Aplicando migraciones a PostgreSQL..."
dotnet ef database update

echo "🔨 Compilando proyecto final..."
dotnet build -c Release

echo "✅ Configuración de base de datos completada!"

echo "🚀 Iniciando aplicación..."
echo "La aplicación estará disponible en: http://IP_EXTERNA:8080"
dotnet run --configuration Release --urls "http://0.0.0.0:8080"
