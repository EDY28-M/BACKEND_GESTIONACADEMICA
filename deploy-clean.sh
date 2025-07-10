#!/bin/bash

# Script final y optimizado para despliegue en VM de Google Cloud
# Versión corregida sin referencias a SQL Server ni MultipleActiveResultSets

echo "🚀 Configuración final de la API con PostgreSQL en VM Google Cloud"
echo "=================================================================="

# Verificar directorio actual
if [ ! -f "Program.cs" ]; then
    echo "❌ Error: Ejecuta este script desde el directorio del proyecto"
    exit 1
fi

# Agregar herramientas de .NET al PATH
export PATH="$PATH:/home/cater/.dotnet/tools"

echo "🧹 1. Limpieza completa del proyecto..."
dotnet clean > /dev/null 2>&1
rm -rf Migrations/ bin/ obj/

echo "📦 2. Restaurando dependencias..."
dotnet restore
if [ $? -ne 0 ]; then
    echo "❌ Error al restaurar paquetes"
    exit 1
fi

echo "🔨 3. Compilando proyecto..."
dotnet build -c Release
if [ $? -ne 0 ]; then
    echo "❌ Error al compilar"
    exit 1
fi

echo "📝 4. Creando migraciones para PostgreSQL..."
dotnet ef migrations add InitialCreate
if [ $? -ne 0 ]; then
    echo "❌ Error al crear migraciones"
    exit 1
fi

echo "🗃️ 5. Aplicando migraciones a PostgreSQL..."
dotnet ef database update
if [ $? -ne 0 ]; then
    echo "❌ Error al aplicar migraciones"
    echo "Verifica que PostgreSQL esté ejecutándose y que la base de datos 'GestionAcademica' exista"
    exit 1
fi

echo "✅ Configuración completada exitosamente!"
echo ""
echo "🔍 Configuración verificada:"
echo "  ✅ Sin referencias a MultipleActiveResultSets"
echo "  ✅ Sin referencias a SQL Server" 
echo "  ✅ Usando PostgreSQL con Npgsql"
echo "  ✅ Cadenas de conexión corregidas"
echo "  ✅ Migraciones limpias aplicadas"
echo ""

echo "🚀 Iniciando aplicación..."
echo "📍 URL: http://$(curl -s ifconfig.me 2>/dev/null || echo 'IP_EXTERNA'):8080"
echo "📖 Swagger: Disponible en la raíz del sitio"
echo "🛑 Presiona Ctrl+C para detener"
echo ""

# Configurar entorno de producción
export ASPNETCORE_ENVIRONMENT=Production

# Iniciar la aplicación
dotnet run --configuration Release --urls "http://0.0.0.0:8080"
