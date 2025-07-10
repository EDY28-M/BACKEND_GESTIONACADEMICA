#!/bin/bash

echo "🚀 Configurando entorno completo en VM para API Cursos Académicos..."

# Actualizar sistema
echo "📦 Actualizando sistema..."
sudo apt update && sudo apt upgrade -y

# Instalar dependencias básicas
echo "🔧 Instalando dependencias..."
sudo apt install -y curl wget gnupg2 software-properties-common apt-transport-https ca-certificates lsb-release

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
sudo -u postgres psql << 'EOF'
DROP DATABASE IF EXISTS GestionAcademica;
DROP USER IF EXISTS gestionacademica;
CREATE USER gestionacademica WITH PASSWORD 'Junior.28';
CREATE DATABASE GestionAcademica OWNER gestionacademica;
GRANT ALL PRIVILEGES ON DATABASE GestionAcademica TO gestionacademica;
ALTER USER gestionacademica CREATEDB;
\q
EOF

# Configurar firewall para el puerto 8080
echo "🔥 Configurando firewall..."
sudo ufw allow 8080
sudo ufw allow 22
sudo ufw --force enable

# Ir al directorio del proyecto
cd ~/api-cursos

# Restaurar dependencias
echo "📦 Restaurando dependencias del proyecto..."
dotnet restore

# Crear migraciones para PostgreSQL
echo "🔄 Creando migraciones para PostgreSQL..."
dotnet ef migrations add InitialPostgreSQL --context GestionAcademicaContext

# Aplicar migraciones
echo "📊 Aplicando migraciones a la base de datos..."
dotnet ef database update --context GestionAcademicaContext

# Compilar el proyecto
echo "🏗️ Compilando proyecto..."
dotnet build -c Release

# Crear un servicio systemd para la aplicación
echo "⚙️ Creando servicio systemd..."
sudo tee /etc/systemd/system/api-cursos.service > /dev/null << 'EOF'
[Unit]
Description=API Cursos Académicos
After=network.target

[Service]
Type=notify
ExecStart=/usr/bin/dotnet /home/cater/api-cursos/bin/Release/net8.0/API_REST_CURSOSACADEMICOS.dll
WorkingDirectory=/home/cater/api-cursos
Restart=always
RestartSec=5
SyslogIdentifier=api-cursos
User=cater
Environment=ASPNETCORE_ENVIRONMENT=Production
Environment=ASPNETCORE_URLS=http://*:8080

[Install]
WantedBy=multi-user.target
EOF

# Habilitar y iniciar el servicio
echo "🚀 Iniciando servicio..."
sudo systemctl daemon-reload
sudo systemctl enable api-cursos
sudo systemctl start api-cursos

echo ""
echo "✅ ¡Configuración completada!"
echo "🌐 La API está ejecutándose en: http://$(curl -s http://metadata.google.internal/computeMetadata/v1/instance/network-interfaces/0/access-configs/0/external-ip -H "Metadata-Flavor: Google"):8080"
echo "📊 Swagger UI disponible en: http://$(curl -s http://metadata.google.internal/computeMetadata/v1/instance/network-interfaces/0/access-configs/0/external-ip -H "Metadata-Flavor: Google"):8080"
echo ""
echo "📋 Comandos útiles:"
echo "  sudo systemctl status api-cursos    # Ver estado del servicio"
echo "  sudo systemctl restart api-cursos   # Reiniciar servicio"
echo "  sudo journalctl -u api-cursos -f    # Ver logs en tiempo real"
echo ""
echo "🧪 Prueba la API:"
echo "  curl http://localhost:8080/api/Docentes"
