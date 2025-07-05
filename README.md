# API REST para Gestión Académica

Esta es una API REST desarrollada en .NET 9 para la gestión de un sistema académico que permite administrar docentes y cursos.

## 🚀 Características

- **Gestión de Docentes**: CRUD completo para administrar información de docentes
- **Gestión de Cursos**: CRUD completo para administrar cursos académicos
- **Relaciones**: Los cursos pueden estar asignados a docentes
- **Validaciones**: Validaciones de datos de entrada y reglas de negocio
- **Documentación**: Swagger/OpenAPI integrado
- **Base de Datos**: SQL Server con Entity Framework Core

## 🏗️ Arquitectura

- **Framework**: .NET 9
- **ORM**: Entity Framework Core 9.0.6
- **Base de Datos**: SQL Server
- **Documentación**: Swagger/OpenAPI
- **Patrón**: API REST con controladores

## 📊 Modelo de Datos

### Docente
- **Id**: Identificador único (autoincremental)
- **Apellidos**: Apellidos del docente (requerido, máx. 100 caracteres)
- **Nombres**: Nombres del docente (requerido, máx. 100 caracteres)
- **Profesión**: Profesión del docente (opcional, máx. 100 caracteres)
- **FechaNacimiento**: Fecha de nacimiento (opcional)
- **Correo**: Correo electrónico único (opcional, máx. 100 caracteres)

### Curso
- **Id**: Identificador único (autoincremental)
- **NombreCurso**: Nombre del curso (requerido, máx. 200 caracteres)
- **Créditos**: Número de créditos (requerido, 1-10)
- **HorasSemanal**: Horas semanales (requerido, 1-40)
- **Ciclo**: Ciclo académico (opcional, máx. 50 caracteres)
- **IdDocente**: ID del docente asignado (opcional)

## 🔗 Endpoints de la API

### Docentes (`/api/docentes`)

| Método | Endpoint | Descripción |
|--------|----------|-------------|
| GET | `/api/docentes` | Obtener todos los docentes |
| GET | `/api/docentes/{id}` | Obtener un docente por ID |
| POST | `/api/docentes` | Crear un nuevo docente |
| PUT | `/api/docentes/{id}` | Actualizar un docente |
| DELETE | `/api/docentes/{id}` | Eliminar un docente |

### Cursos (`/api/cursos`)

| Método | Endpoint | Descripción |
|--------|----------|-------------|
| GET | `/api/cursos` | Obtener todos los cursos |
| GET | `/api/cursos/{id}` | Obtener un curso por ID |
| GET | `/api/cursos/PorDocente/{docenteId}` | Obtener cursos de un docente |
| GET | `/api/cursos/PorCiclo/{ciclo}` | Obtener cursos por ciclo |
| POST | `/api/cursos` | Crear un nuevo curso |
| PUT | `/api/cursos/{id}` | Actualizar un curso |
| DELETE | `/api/cursos/{id}` | Eliminar un curso |

## ⚙️ Configuración

### Requisitos Previos
- .NET 9 SDK
- SQL Server
- Visual Studio Code o Visual Studio

### Configuración de Base de Datos

1. **Crear la base de datos** ejecutando el script SQL proporcionado en SQL Server:

```sql
-- Crear la base de datos
CREATE DATABASE GestionAcademica;
GO

-- Usar la base de datos
USE GestionAcademica;
GO

-- Crear las tablas y datos de ejemplo...
```

2. **Configurar la cadena de conexión** en `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=MATHOS;Database=GestionAcademica;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=true"
  }
}
```

### Ejecutar la Aplicación

1. **Clonar o descargar** el proyecto
2. **Navegar** al directorio del proyecto:
   ```bash
   cd API_REST_CURSOSACADEMICOS
   ```
3. **Restaurar dependencias**:
   ```bash
   dotnet restore
   ```
4. **Compilar** el proyecto:
   ```bash
   dotnet build
   ```
5. **Ejecutar** la aplicación:
   ```bash
   dotnet run
   ```

La aplicación estará disponible en:
- **API**: `http://localhost:5251`
- **Swagger**: `http://localhost:5251/swagger`

## 🧪 Pruebas

El archivo `test-api.http` contiene ejemplos de todas las peticiones HTTP para probar la API. Puedes usar:
- **REST Client** (extensión de VS Code)
- **Postman**
- **Swagger UI**

### Ejemplos de Uso

**Crear un docente:**
```json
POST /api/docentes
{
  "apellidos": "García",
  "nombres": "María Elena",
  "profesion": "Profesora de Química",
  "fechaNacimiento": "1982-03-15",
  "correo": "maria.garcia@instituto.edu"
}
```

**Crear un curso:**
```json
POST /api/cursos
{
  "nombreCurso": "Álgebra Lineal",
  "creditos": 4,
  "horasSemanal": 6,
  "ciclo": "2do Ciclo",
  "idDocente": 1
}
```

## 🔒 Validaciones y Reglas de Negocio

### Docentes
- Los apellidos y nombres son obligatorios
- El correo debe ser único en el sistema
- No se puede eliminar un docente que tiene cursos asignados

### Cursos
- El nombre del curso es obligatorio
- Los créditos deben estar entre 1 y 10
- Las horas semanales deben estar entre 1 y 40
- El docente asignado debe existir en el sistema

## 🛠️ Tecnologías Utilizadas

- **.NET 9**: Framework principal
- **ASP.NET Core**: Para la API REST
- **Entity Framework Core 9**: ORM para base de datos
- **SQL Server**: Base de datos
- **Swagger/OpenAPI**: Documentación de la API
- **DTOs**: Objetos de transferencia de datos para separar modelos

## 📝 Notas Adicionales

- La API incluye **CORS** configurado para permitir todas las conexiones
- Se utilizan **DTOs** para separar los modelos de base de datos de las respuestas de la API
- Incluye **validaciones** tanto en el cliente como en el servidor
- **Manejo de errores** con mensajes descriptivos
- **Relaciones** entre entidades manejadas por Entity Framework

---

Desarrollado con ❤️ usando .NET 9
