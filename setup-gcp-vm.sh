#!/bin/bash

# Script para configurar la VM de Google Cloud para la API de Cursos Académicos

echo "🚀 Configurando VM para API de Cursos Académicos..."

# Actualizar sistema
echo "📦 Actualizando sistema..."
sudo apt update && sudo apt upgrade -y

# Instalar dependencias básicas
echo "🔧 Instalando dependencias..."
sudo apt install -y curl wget gnupg2 software-properties-common apt-transport-https ca-certificates lsb-release

# Instalar Docker
echo "🐳 Instalando Docker..."
curl -fsSL https://get.docker.com -o get-docker.sh
sudo sh get-docker.sh
sudo usermod -aG docker $USER

# Instalar .NET 8
echo "🔷 Instalando .NET 8..."
wget https://packages.microsoft.com/config/debian/12/packages-microsoft-prod.deb -O packages-microsoft-prod.deb
sudo dpkg -i packages-microsoft-prod.deb
sudo apt update
sudo apt install -y dotnet-sdk-8.0

# Instalar PostgreSQL
echo "🗃️ Instalando PostgreSQL..."
sudo apt install -y postgresql postgresql-contrib
sudo systemctl start postgresql
sudo systemctl enable postgresql

# Configurar PostgreSQL
echo "⚙️ Configurando base de datos..."
sudo -u postgres psql << EOF
CREATE USER gestionacademica WITH PASSWORD 'Junior.28';
CREATE DATABASE GestionAcademica OWNER gestionacademica;
GRANT ALL PRIVILEGES ON DATABASE GestionAcademica TO gestionacademica;
ALTER USER gestionacademica CREATEDB;
\q
EOF

# Instalar Git
echo "📁 Instalando Git..."
sudo apt install -y git

# Configurar firewall para el puerto 8080
echo "🔥 Configurando firewall..."
sudo ufw allow 8080
sudo ufw --force enable

# Crear directorio para la aplicación
echo "📂 Creando estructura de directorios..."
mkdir -p ~/api-cursos
cd ~/api-cursos

echo "✅ ¡Configuración completada!"
echo "🌐 La VM está lista para recibir tu aplicación .NET"
echo "📊 Base de datos PostgreSQL configurada"
echo "🔌 Puerto 8080 abierto para la aplicación"
echo ""
echo "📋 Próximos pasos:"
echo "1. Subir tu código a esta VM"
echo "2. Modificar connection string para PostgreSQL"
echo "3. Ejecutar migraciones de Entity Framework"
echo "4. Compilar y ejecutar la aplicación"
