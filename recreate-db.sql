-- Script para recrear la base de datos PostgreSQL

-- Terminar conexiones existentes a la base de datos
SELECT pg_terminate_backend(pid) FROM pg_stat_activity WHERE datname = 'gestionacademica';

-- Eliminar base de datos y usuario si existen
DROP DATABASE IF EXISTS gestionacademica;
DROP ROLE IF EXISTS gestionacademica;

-- Crear nuevo usuario
CREATE ROLE gestionacademica WITH LOGIN PASSWORD 'Junior.28';
ALTER ROLE gestionacademica CREATEDB;

-- Crear nueva base de datos
CREATE DATABASE gestionacademica OWNER gestionacademica;

-- Otorgar permisos
GRANT ALL PRIVILEGES ON DATABASE gestionacademica TO gestionacademica;

-- Verificar que se creó correctamente
\l
\du
