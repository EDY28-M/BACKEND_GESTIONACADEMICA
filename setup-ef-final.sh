#!/bin/bash

# Script mejorado para configurar Entity Framework y la base de datos PostgreSQL

echo "🚀 Iniciando configuración de la API con PostgreSQL..."

# Agregar herramientas de .NET al PATH
export PATH="$PATH:/home/cater/.dotnet/tools"

# Verificar que estamos en el directorio correcto
if [ ! -f "Program.cs" ]; then
    echo "❌ Error: No se encontró Program.cs. Asegúrate de estar en el directorio correcto."
    exit 1
fi

echo "🧹 Limpieza completa del proyecto..."
dotnet clean
rm -rf Migrations/
rm -rf bin/ obj/

echo "📦 Restaurando paquetes NuGet..."
dotnet restore

if [ $? -ne 0 ]; then
    echo "❌ Error al restaurar paquetes NuGet"
    exit 1
fi

echo "🔨 Compilando proyecto..."
dotnet build -c Release

if [ $? -ne 0 ]; then
    echo "❌ Error al compilar el proyecto"
    exit 1
fi

echo "🔍 Verificando conexión a PostgreSQL..."
# Probar conexión con psql (si está disponible)
if command -v psql &> /dev/null; then
    echo "Verificando conexión a la base de datos..."
    PGPASSWORD=Junior.28 psql -h localhost -U gestionacademica -d GestionAcademica -c "SELECT version();" 2>/dev/null
    if [ $? -eq 0 ]; then
        echo "✅ Conexión a PostgreSQL exitosa"
    else
        echo "⚠️ No se pudo verificar la conexión a PostgreSQL, pero continuamos..."
    fi
else
    echo "⚠️ psql no disponible, saltando verificación de conexión"
fi

echo "📝 Creando migraciones iniciales para PostgreSQL..."
dotnet ef migrations add InitialCreate

if [ $? -ne 0 ]; then
    echo "❌ Error al crear migraciones"
    exit 1
fi

echo "🗃️ Aplicando migraciones a PostgreSQL..."
dotnet ef database update

if [ $? -ne 0 ]; then
    echo "❌ Error al aplicar migraciones"
    echo "Verifica que:"
    echo "1. PostgreSQL esté ejecutándose"
    echo "2. La base de datos 'GestionAcademica' exista"
    echo "3. El usuario 'gestionacademica' tenga permisos"
    echo "4. La cadena de conexión en appsettings.json sea correcta"
    exit 1
fi

echo "✅ Configuración de base de datos completada exitosamente!"

echo "🚀 Iniciando aplicación en modo Release..."
echo "La aplicación estará disponible en: http://$(curl -s ifconfig.me):8080"
echo "Swagger UI estará disponible en la raíz del sitio"
echo ""
echo "Presiona Ctrl+C para detener la aplicación"

# Usar el archivo de configuración de producción
export ASPNETCORE_ENVIRONMENT=Production

dotnet run --configuration Release --urls "http://0.0.0.0:8080"
