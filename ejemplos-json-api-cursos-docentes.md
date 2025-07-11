# Ejemplos JSON para API de Cursos y Docentes

## Base URL
```
http://localhost:5000
```

---

## DOCENTES

### 1. GET - Obtener todos los docentes
**Endpoint:** `GET /api/Docentes`

**Headers:**
```json
{
  "Accept": "application/json"
}
```

**Response esperado:**
```json
[
  {
    "id": 1,
    "apellidos": "García López",
    "nombres": "Juan Carlos",
    "profesion": "Ingeniero de Sistemas",
    "fechaNacimiento": "1985-05-15T00:00:00",
    "correo": "juan.garcia@universidad.edu",
    "cursos": [
      {
        "id": 1,
        "nombreCurso": "Programación Web",
        "creditos": 4,
        "horasSemanal": 6,
        "ciclo": 5
      }
    ]
  }
]
```

### 2. GET - Obtener docente por ID
**Endpoint:** `GET /api/Docentes/1`

**Headers:**
```json
{
  "Accept": "application/json"
}
```

**Response esperado:**
```json
{
  "id": 1,
  "apellidos": "García López",
  "nombres": "Juan Carlos",
  "profesion": "Ingeniero de Sistemas",
  "fechaNacimiento": "1985-05-15T00:00:00",
  "correo": "juan.garcia@universidad.edu",
  "cursos": [
    {
      "id": 1,
      "nombreCurso": "Programación Web",
      "creditos": 4,
      "horasSemanal": 6,
      "ciclo": 5
    }
  ]
}
```

### 3. POST - Crear nuevo docente
**Endpoint:** `POST /api/Docentes`

**Headers:**
```json
{
  "Content-Type": "application/json",
  "Accept": "application/json"
}
```

**Body:**
```json
{
  "apellidos": "Martínez Rodríguez",
  "nombres": "María Elena",
  "profesion": "Ingeniera de Software",
  "fechaNacimiento": "1990-08-22",
  "correo": "maria.martinez@universidad.edu"
}
```

**Response esperado (201 Created):**
```json
{
  "id": 2,
  "apellidos": "Martínez Rodríguez",
  "nombres": "María Elena",
  "profesion": "Ingeniera de Software",
  "fechaNacimiento": "1990-08-22T00:00:00",
  "correo": "maria.martinez@universidad.edu",
  "cursos": []
}
```

### 4. PUT - Actualizar docente
**Endpoint:** `PUT /api/Docentes/1`

**Headers:**
```json
{
  "Content-Type": "application/json",
  "Accept": "application/json"
}
```

**Body:**
```json
{
  "apellidos": "García López",
  "nombres": "Juan Carlos",
  "profesion": "Ingeniero de Software Senior",
  "fechaNacimiento": "1985-05-15",
  "correo": "juan.garcia.senior@universidad.edu"
}
```

**Response esperado (204 No Content)**

### 5. DELETE - Eliminar docente
**Endpoint:** `DELETE /api/Docentes/1`

**Headers:**
```json
{
  "Accept": "application/json"
}
```

**Response esperado (204 No Content)**

**Nota:** Si el docente tiene cursos asignados, retornará error 400:
```json
{
  "error": "No se puede eliminar el docente porque tiene cursos asignados"
}
```

---

## CURSOS

### 1. GET - Obtener todos los cursos
**Endpoint:** `GET /api/Cursos`

**Headers:**
```json
{
  "Accept": "application/json"
}
```

**Response esperado:**
```json
[
  {
    "id": 1,
    "nombreCurso": "Programación Web",
    "creditos": 4,
    "horasSemanal": 6,
    "ciclo": 5,
    "idDocente": 1,
    "docente": {
      "id": 1,
      "apellidos": "García López",
      "nombres": "Juan Carlos",
      "profesion": "Ingeniero de Sistemas"
    }
  }
]
```

### 2. GET - Obtener curso por ID
**Endpoint:** `GET /api/Cursos/1`

**Headers:**
```json
{
  "Accept": "application/json"
}
```

**Response esperado:**
```json
{
  "id": 1,
  "nombreCurso": "Programación Web",
  "creditos": 4,
  "horasSemanal": 6,
  "ciclo": 5,
  "idDocente": 1,
  "docente": {
    "id": 1,
    "apellidos": "García López",
    "nombres": "Juan Carlos",
    "profesion": "Ingeniero de Sistemas"
  }
}
```

### 3. GET - Obtener cursos por docente
**Endpoint:** `GET /api/Cursos/PorDocente/1`

**Headers:**
```json
{
  "Accept": "application/json"
}
```

**Response esperado:**
```json
[
  {
    "id": 1,
    "nombreCurso": "Programación Web",
    "creditos": 4,
    "horasSemanal": 6,
    "ciclo": 5,
    "idDocente": 1,
    "docente": {
      "id": 1,
      "apellidos": "García López",
      "nombres": "Juan Carlos",
      "profesion": "Ingeniero de Sistemas"
    }
  },
  {
    "id": 3,
    "nombreCurso": "Base de Datos",
    "creditos": 5,
    "horasSemanal": 8,
    "ciclo": 4,
    "idDocente": 1,
    "docente": {
      "id": 1,
      "apellidos": "García López",
      "nombres": "Juan Carlos",
      "profesion": "Ingeniero de Sistemas"
    }
  }
]
```

### 4. GET - Obtener cursos por ciclo
**Endpoint:** `GET /api/Cursos/PorCiclo/5`

**Headers:**
```json
{
  "Accept": "application/json"
}
```

**Response esperado:**
```json
[
  {
    "id": 1,
    "nombreCurso": "Programación Web",
    "creditos": 4,
    "horasSemanal": 6,
    "ciclo": 5,
    "idDocente": 1,
    "docente": {
      "id": 1,
      "apellidos": "García López",
      "nombres": "Juan Carlos",
      "profesion": "Ingeniero de Sistemas"
    }
  }
]
```

### 5. POST - Crear nuevo curso
**Endpoint:** `POST /api/Cursos`

**Headers:**
```json
{
  "Content-Type": "application/json",
  "Accept": "application/json"
}
```

**Body - Ejemplo 1 (con docente):**
```json
{
  "nombreCurso": "Inteligencia Artificial",
  "creditos": 5,
  "horasSemanal": 8,
  "ciclo": 7,
  "idDocente": 1
}
```

**Body - Ejemplo 2 (sin docente):**
```json
{
  "nombreCurso": "Cálculo Diferencial",
  "creditos": 4,
  "horasSemanal": 6,
  "ciclo": 2,
  "idDocente": null
}
```

**Response esperado (201 Created):**
```json
{
  "id": 2,
  "nombreCurso": "Inteligencia Artificial",
  "creditos": 5,
  "horasSemanal": 8,
  "ciclo": 7,
  "idDocente": 1,
  "docente": {
    "id": 1,
    "apellidos": "García López",
    "nombres": "Juan Carlos",
    "profesion": "Ingeniero de Sistemas"
  }
}
```

### 6. PUT - Actualizar curso
**Endpoint:** `PUT /api/Cursos/1`

**Headers:**
```json
{
  "Content-Type": "application/json",
  "Accept": "application/json"
}
```

**Body - Ejemplo 1 (cambiar docente):**
```json
{
  "nombreCurso": "Programación Web Avanzada",
  "creditos": 5,
  "horasSemanal": 8,
  "ciclo": 6,
  "idDocente": 2
}
```

**Body - Ejemplo 2 (quitar docente):**
```json
{
  "nombreCurso": "Programación Web",
  "creditos": 4,
  "horasSemanal": 6,
  "ciclo": 5,
  "idDocente": null
}
```

**Response esperado (204 No Content)**

### 7. DELETE - Eliminar curso
**Endpoint:** `DELETE /api/Cursos/1`

**Headers:**
```json
{
  "Accept": "application/json"
}
```

**Response esperado (204 No Content)**

---

## VALIDACIONES Y RESTRICCIONES

### Docentes
- **apellidos**: Requerido, máximo 100 caracteres
- **nombres**: Requerido, máximo 100 caracteres
- **profesion**: Opcional, máximo 100 caracteres
- **fechaNacimiento**: Opcional, formato ISO 8601
- **correo**: Opcional, debe ser email válido, máximo 100 caracteres, debe ser único

### Cursos
- **nombreCurso**: Requerido, máximo 200 caracteres
- **creditos**: Requerido, rango 1-10
- **horasSemanal**: Requerido, rango 1-40
- **ciclo**: Requerido, rango 1-10
- **idDocente**: Opcional, debe existir el docente si se especifica

---

## CÓDIGOS DE RESPUESTA HTTP

- **200 OK**: Operación exitosa (GET)
- **201 Created**: Recurso creado exitosamente (POST)
- **204 No Content**: Operación exitosa sin contenido (PUT, DELETE)
- **400 Bad Request**: Datos inválidos o reglas de negocio violadas
- **404 Not Found**: Recurso no encontrado

---

## EJEMPLOS DE ERRORES

### Error 400 - Validación de modelo
```json
{
  "type": "https://tools.ietf.org/html/rfc7231#section-6.5.1",
  "title": "One or more validation errors occurred.",
  "status": 400,
  "errors": {
    "Creditos": [
      "The field Creditos must be between 1 and 10."
    ]
  }
}
```

### Error 400 - Regla de negocio
```json
{
  "error": "Ya existe un docente con este correo electrónico"
}
```

### Error 404 - Recurso no encontrado
```json
{
  "error": "Curso con ID 999 no encontrado"
}
```

---

## NOTA SOBRE PATCH

La API actual no implementa el método PATCH. Si necesitas actualización parcial, debes usar PUT con todos los campos del objeto.
